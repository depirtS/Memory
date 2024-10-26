using Plugin.Maui.Audio;
using System.Collections;
using System.Linq.Expressions;

namespace Memory;

public partial class PlayPage : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
        return true;
    }

    private IAudioManager _audioManager;
    private IAudioPlayer _errorSound;
    private Dictionary<string, Field> Fields { get; set; }
    private bool Play { get; set; }
    private bool Continue { get; set; }
    private bool NormalMode { get; set; }
    private Random Random { get; set; }
    private List<string> FieldSequence { get; set; }
    private int SequenceClickCount { get; set; }

    public PlayPage(bool colors)
    {
        NormalMode = colors;
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        InitializeValues();
    }

    private async void InitializeValues()
    {
        NormalRecordLabel.Text = "Record in normal mode: " + Preferences.Get("normalRecord", 0);
        RandomColorsRecordLabel.Text = "Record in random colors mode: " + Preferences.Get("randomColorsRecord", 0);
        Play = false;
        Continue = false;
        Random = new Random();
        Fields = new Dictionary<string, Field>();
        Fields.Add("1", new Field(First, "first.mp3"));
        Fields.Add("2", new Field(Second, "second.mp3"));
        Fields.Add("3", new Field(Three, "three.mp3"));
        Fields.Add("4", new Field(Four, "four.mp3"));

        _audioManager = AudioManager.Current;
        _errorSound = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("incorrect.mp3"));
    }

    private void setButtonColors()
    {
        List<Color> colorsList = new List<Color>();
        foreach (Color color in GlobalManager.colorsList.Values)
        {
            colorsList.Add(color);
        }
        First.BackgroundColor = Color.FromArgb(colorsList[Random.Next(colorsList.Count() - 1) + 1].ToHex());
        Second.BackgroundColor = Color.FromArgb(colorsList[Random.Next(colorsList.Count() - 1) + 1].ToHex());
        Three.BackgroundColor = Color.FromArgb(colorsList[Random.Next(colorsList.Count() - 1) + 1].ToHex());
        Four.BackgroundColor = Color.FromArgb(colorsList[Random.Next(colorsList.Count() - 1) + 1].ToHex());
    }

    private void Play_Clicked(object sender, EventArgs e)
    {
        if (!NormalMode)
            setButtonColors();
        StopOverlay.IsVisible = false;
        Play = true;
        Continue = true;
        SequenceClickCount = 0;
        FieldSequence = new List<string>();
        AddNextField();
        AnimateFieldSequence();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (Play && Continue)
        {
            var btn = (Button)sender;
            if (FieldSequence[SequenceClickCount].Equals(btn.Text))
                Fields[btn.Text].AnimateButtonClick();
            else
                MistakeField_Clicked(btn);

            if (SequenceClickCount == FieldSequence.Count() - 1 && Continue)
            {
                SequenceClickCount = -1;
                AddNextField();
                AnimateFieldSequence();
            }
            SequenceClickCount++;
        }
    }

    private void AddNextField()
    {
        int idField = Random.Next(Fields.Count) + 1;
        FieldSequence.Add(idField.ToString());
    }
    private async void AnimateFieldSequence()
    {
        Play = false;
        await Task.Delay(1000);
        foreach (var field in FieldSequence)
        {
            Fields[field].AnimateButtonClick();
            await Task.Delay(501);
        }
        Play = true;
    }
    private async void MistakeField_Clicked(Button btn)
    {

        Continue = false;
        _errorSound?.Play();
        await btn.FadeTo(0.1, 250, Easing.CubicOut);
        await btn.FadeTo(0.5, 250, Easing.CubicIn);

        SaveCurrentRecord();
        if (NormalMode)
            NormalRecordLabel.Text = "Record in normal mode: " + Preferences.Get("normalRecord", 0);
        else
            RandomColorsRecordLabel.Text = "Record in random colors mode: " + Preferences.Get("randomColorsRecord", 0);
        StopOverlay.IsVisible = true;
    }

    private void SaveCurrentRecord()
    {
        CurrentScore.Text = "Score: " + (FieldSequence.Count() - 1);
        if (NormalMode)
        {
            if (Preferences.Default.ContainsKey("normalRecord"))
            {
                int currentRecord = Preferences.Get("normalRecord", 0);
                if (currentRecord < FieldSequence.Count() - 1)
                    Preferences.Set("normalRecord", FieldSequence.Count() - 1);
            }
            else
            {
                Preferences.Set("normalRecord", FieldSequence.Count() - 1);
            }
        }
        else
        {
            if (Preferences.Default.ContainsKey("randomColorsRecord"))
            {
                int currentRecord = Preferences.Get("randomColorsRecord", 0);
                if (currentRecord < FieldSequence.Count() - 1)
                    Preferences.Set("randomColorsRecord", FieldSequence.Count() - 1);
            }
            else
            {
                Preferences.Set("randomColorsRecord", FieldSequence.Count() - 1);
            }
        }

    }
}
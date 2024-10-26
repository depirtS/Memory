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

    private Dictionary<string, Field> Fields { get; set; }
    private bool Play { get; set; }
    private bool Continue { get; set; }
    private Random Random { get; set; }
    private List<string> FieldSequence { get; set; }
    private int SequenceClickCount { get; set; }

    public PlayPage(bool colors)
	{
		NavigationPage.SetHasNavigationBar(this, false);
		InitializeComponent();
        InitializeValues();
        setButtonColors(colors);
    }

    private void InitializeValues()
    {
        RecordLabel.Text = "Record: " + Preferences.Get("record", 0);
        Play = false;
        Continue = false;
        Random = new Random();
        Fields = new Dictionary<string, Field>();
        Fields.Add("1", new Field(First));
        Fields.Add("2", new Field(Second));
        Fields.Add("3", new Field(Three));
        Fields.Add("4", new Field(Four));
    }

    private void setButtonColors(bool colors)
    {
        if(!colors)
        {
            //random colors
        }
    }

    private void Play_Clicked(object sender, EventArgs e)
    {
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

            if(SequenceClickCount == FieldSequence.Count() - 1 && Continue)
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
        int idField = Random.Next(Fields.Count)+ 1;
        FieldSequence.Add(idField.ToString());
    }
    private async void AnimateFieldSequence()
    {
        Play = false;
        await Task.Delay(1000);
        foreach(var field in FieldSequence)
        {
            Fields[field].AnimateButtonClick();
            await Task.Delay(501);
        }
        Play = true;
    }
    private async void MistakeField_Clicked(Button btn)
    {
        Continue = false;
        await btn.FadeTo(0.1, 250, Easing.CubicOut);
        await btn.FadeTo(0.5, 250, Easing.CubicIn);

        SaveCurrentRecord();
        RecordLabel.Text = "Record: " + Preferences.Get("record", 0);
        StopOverlay.IsVisible = true;
    }

    private void SaveCurrentRecord()
    {
        if (Preferences.Default.ContainsKey("record"))
        {
            int currentRecord = Preferences.Get("record", 0);
            if(currentRecord < FieldSequence.Count()-1)
                Preferences.Set("record", FieldSequence.Count() - 1);
        }
        else
        {
            Preferences.Set("record", 0);
        }
    }
}
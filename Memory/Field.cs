using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class Field
    {
        private Button Button {  get; set; }
        private IAudioPlayer _audioPlayer;
        //add sound to animate

        public Field(Button button, string soundFilePath)
        {
            Button = button;
            InitializeAudioPlayer(soundFilePath);
        }

        private async void InitializeAudioPlayer(string soundFilePath)
        {
            var audioManager = AudioManager.Current;
            _audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(soundFilePath));
        }

        public async void AnimateButtonClick()
        {
            await Button.FadeTo(1.0, 250, Easing.CubicOut);
            await Button.FadeTo(0.5, 250, Easing.CubicIn);
            _audioPlayer?.Play();
        }
    }
}

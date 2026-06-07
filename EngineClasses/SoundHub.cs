using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace LITOURGIYA___OBLATION
{
    public class SoundHub
    {
        private AudioFileReader audioFile;
        private WaveOutEvent outputDevice;
        public void PlayMusic(string audio)
        {
            switch (audio)
            {
                case "CombatOST":
                    audio = "OBLATION-InCombatOST (Drive Injector - CRY.NN).mp3";
                    break;
                case "MainMenuOST":
                    audio = "OBLATION-MainMenuOST (Decay.fla - SentryTurbo x CRY.NN).mp3";
                    break;
            }
            string file = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
            file = Path.Combine(file, audio);

            outputDevice?.Stop();
            outputDevice?.Dispose();
            audioFile?.Dispose();

            audioFile = new AudioFileReader(file);
            outputDevice = new WaveOutEvent();

            outputDevice.Init(audioFile);
            outputDevice.Volume = 0.5f;
            outputDevice.Play();
        }
        public void StopMusic()
        {
            outputDevice?.Stop();
        }
    }
}

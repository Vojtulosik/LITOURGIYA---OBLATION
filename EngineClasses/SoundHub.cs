using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace LITOURGIYA___OBLATION
{
    internal class SoundHub
    {
        public void PlaySound()
        {
            Task.Run(() => Console.Beep(37, 300));
        }
        public void PlayFromFile()
        {
            string file = Path.Combine("Assets", "OBLATION-InCombatOST (Drive Injector - CRY.NN)", ".mp3");
            var audioFile = new AudioFileReader(file);
            var outputDevice = new WaveOutEvent();

            outputDevice.Init(audioFile);
            outputDevice.Volume = 0.5f;
            outputDevice.Play();
            Console.ReadKey();
        }
    }
}

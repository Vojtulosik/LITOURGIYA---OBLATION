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
        private LoopStream loopStream;
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
                case "InHideoutOST":
                    audio = "OBLATION-InHideoutOST (Epilogue - CRY.NN).mp3";
                    break;
                case "ExploringOST":
                    audio = "OBLATION-ExploringOST (Million ways to show the awful - CRY.NN).mp3";
                    break;
            }
            string file = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
            file = Path.Combine(file, audio);

            outputDevice?.Stop();
            outputDevice?.Dispose();
            audioFile?.Dispose();

            audioFile = new AudioFileReader(file);
            loopStream = new LoopStream(audioFile);
            outputDevice = new WaveOutEvent();

            outputDevice.Init(loopStream);
            outputDevice.Volume = 1f;
            outputDevice.Play();
        }
        public void StopMusic()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();

            loopStream?.Dispose();
            audioFile?.Dispose();
        }
    }
    public class LoopStream : WaveStream
    {
        private readonly WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
        }

        public override WaveFormat WaveFormat => sourceStream.WaveFormat;

        public override long Length => sourceStream.Length;

        public override long Position
        {
            get => sourceStream.Position;
            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(
                    buffer,
                    offset + totalBytesRead,
                    count - totalBytesRead);

                if (bytesRead == 0)
                {
                    sourceStream.Position = 0;
                }
                else
                {
                    totalBytesRead += bytesRead;
                }
            }

            return totalBytesRead;
        }
    }
}

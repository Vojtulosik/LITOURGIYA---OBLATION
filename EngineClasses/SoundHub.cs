using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LITOURGIYA___OBLATION
{
    internal class SoundHub
    {
        public void PlaySound()
        {
            Task.Run(() => Console.Beep(37, 300));
        }
    }
}

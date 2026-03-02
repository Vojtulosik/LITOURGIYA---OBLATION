using System;
using System.Collections.Generic;
using System.Text;

namespace LITOURGIYA___OBLATION
{
    internal class LoadNotes
    {
        public int HighlightChosenColor = 6;
        public int TextChosenColor = 9;
        public LoadNotes(int highlightChosenColor, int textChosenColor)
        {
            HighlightChosenColor = highlightChosenColor;
            TextChosenColor = textChosenColor;
        }

        public void Render(int ID)
        {
            ConsoleOutput ConsoleOutput = new ConsoleOutput(null, null, null, null, null, TextChosenColor, HighlightChosenColor, null, null, null);
            FileManagement FileManager = new FileManagement();
            switch (ID)
            {
                case 0:
                    string[] prompt = FileManager.LoadAsset("JOURNAL Entry 01 - Settlement");
                    ConsoleOutput.RenderNote(prompt, true);
                    break;
            }
        }
    }
}

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
            string notename = "";
            switch (ID)
            {
                case 0:
                    notename = "JOURNAL Entry 01 - Settlement";
                    break;
            }
            string[] prompt = FileManager.LoadAsset(notename);
            ConsoleOutput.RenderNote(prompt, true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace LITOURGIYA___OBLATION
{
    internal class InGameUI
    {
        private string SoulFilePlacement;
        private string[] Data;
        private int IngameDay;
        private int TextChosenColor;
        private int HighlightChosenColor;
        public InGameUI(string soulfileplacement, int textchosencolor, int highlightchosencolor) 
        {
            SoulFilePlacement = soulfileplacement;
            TextChosenColor = textchosencolor;
            HighlightChosenColor = highlightchosencolor;
        }
        FileManagement FileManager = new FileManagement();
        SoundHub SoundHub = new SoundHub();
        public void Initialisation()
        {
            Data = FileManager.LoadData(SoulFilePlacement);
            string DataCut = Data[0].Substring(Data[0].IndexOf(":"));
            int.TryParse(DataCut, out int IngameDay);
            if (IngameDay == 0)
            {
                Intro();
                Console.Clear();
                LoadNotes NoteUI = new LoadNotes(HighlightChosenColor, HighlightChosenColor);
                NoteUI.Render(0);
                string[] options = { "Close" };
                int[] specialsymbols = { 0, 1 };
                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, null, null, null, null, TextChosenColor, HighlightChosenColor, specialsymbols, null, null);
                ConsoleOutput.RenderOptions(options, specialsymbols);
                ConsoleOutput.Run();
                Console.Clear();
                CombatInput Combat = new CombatInput();
                bool test = Combat.PrecisionBar(4, 50, 4);
                if (test == true)
                {
                    Console.WriteLine("You hit the target type shit");
                }
                else
                {
                    Console.WriteLine("Innacurate FOOL!!!!!");
                }
                Thread.Sleep(2000);
                test = Combat.PrecisionBar(4, 50, 4);
                if (test == true)
                {
                    Console.WriteLine("You hit the target type shit");
                }
                else
                {
                    Console.WriteLine("Innacurate FOOL!!!!!");
                }
                Console.ReadKey();
            }
            EmilyBaseUI BaseUI = new EmilyBaseUI(TextChosenColor, HighlightChosenColor);
            BaseUI.Hideout();
        }
        private void Intro()
        {
            Console.Clear();
            string[] prompts =
            {
               "Text-based survival horror RPG, based on :"
            };
            int[] textcolors =
            {
                8
            };
            int padding = (Console.WindowWidth - prompts[0].Length) / 2;
            string store = new string(' ', padding) + prompts[0];
            prompts[0] = store;
            ConsoleOutput ConsoleOutput = new ConsoleOutput(null, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, null, null, null);
            Thread.Sleep(700);
            ConsoleOutput.RenderText(prompts, textcolors);
            textcolors[0] = 7;
            Thread.Sleep(1000);
            ConsoleOutput.RenderText(prompts, textcolors);
            textcolors[0] = 6;
            Thread.Sleep(1000);
            ConsoleOutput.RenderText(prompts, textcolors);
            Thread.Sleep(3000);
            Console.Clear();
            prompts[0] = "LITOURGIYA, \"TOUR\"";
            padding = (Console.WindowWidth - prompts[0].Length) / 2;
            store = new string(' ', padding) + prompts[0];
            prompts[0] = store;
            textcolors[0] = 10;
            ConsoleOutput.RenderText(prompts, textcolors);
            Thread.Sleep(3000);
            Console.Clear();
            prompts = new string[] { "", "Created by ", "VOJTULOSIK", " aka ", "FUNI" };
            store = "Created by VOJTULOSIK aka FUNI";
            padding = (Console.WindowWidth - store.Length) / 2;
            store = new string(' ', padding);
            prompts[0] = store;
            textcolors = new int[] { 7, 7, 14, 7, 14 };
            ConsoleOutput.RenderText(prompts, textcolors);
            Thread.Sleep(4000);
            Console.Clear();
            Thread.Sleep(1000);
            prompts = new string[] { "OBLATION" };
            padding = (Console.WindowWidth - prompts[0].Length) / 2;
            store = new string(' ', padding) + prompts[0];
            prompts[0] = store;
            textcolors = new int[] { 8 };
            ConsoleOutput.RenderText(prompts, textcolors);
            textcolors[0] = 7;
            Thread.Sleep(700);
            ConsoleOutput.RenderText(prompts, textcolors);
            textcolors[0] = 6;
            Thread.Sleep(700);
            ConsoleOutput.RenderText(prompts, textcolors);
            Thread.Sleep(1500);
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                textcolors[0] = 9;
                ConsoleOutput.RenderText(prompts, textcolors);
                Thread.Sleep(random.Next(10, 100));
                textcolors[0] = 6;
                ConsoleOutput.RenderText(prompts, textcolors);
                Thread.Sleep(random.Next(10, 100));
            }
            Console.Clear();
            Thread.Sleep(2000);
            prompts = new string[] { "NATURE IS ", "DEAD\n"};
            textcolors = new int[] { 7, 0 };
            ConsoleOutput.RenderText(prompts, textcolors, 30);
            Thread.Sleep(1000);
            prompts = new string[] { "STEEL IS ", "BRED\n" };
            textcolors = new int[] { 7, 0 };
            ConsoleOutput.RenderText(prompts, textcolors, 30);
            Thread.Sleep(1000);
            prompts = new string[] { "FLESH IS ", "MISREAD.\n" };
            textcolors = new int[] { 6, 10 };
            ConsoleOutput.RenderText(prompts, textcolors, 30);
            Thread.Sleep(6000);
            Console.Clear();
        }
    }
}

/*
 * ConsoleColor.Red, //0
            ConsoleColor.Yellow, //1
            ConsoleColor.Green, //2
            ConsoleColor.Blue, //3
            ConsoleColor.Magenta, //4
            ConsoleColor.Cyan, //5
            ConsoleColor.White, //6
            ConsoleColor.Gray, //7
            ConsoleColor.DarkGray, //8
            ConsoleColor.Black, //9
            ConsoleColor.DarkRed, //10
            ConsoleColor.DarkYellow, //11
            ConsoleColor.DarkGreen, //12
            ConsoleColor.DarkBlue, //13
            ConsoleColor.DarkMagenta, //14
            ConsoleColor.DarkCyan, //15
*/

//Night moon cycle : ◯ ☽ ◑ ⬤ ☾◯

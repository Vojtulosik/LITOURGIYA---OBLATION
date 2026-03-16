using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security;

namespace LITOURGIYA___OBLATION
{
    internal class FileManagement
    {
        public int AmountOfSouls;

        public void SaveProgress(string path, string[] data)
        {
            File.WriteAllLines(path, data);
            File.Move(path, Path.GetFileNameWithoutExtension(path).Substring(0, Path.GetFileNameWithoutExtension(path).IndexOf("-") + 2) + "Day " + data[0].Substring(data[0].IndexOf(":") + 1) + ".txt");
        }
        public string[] SearchFiles()
        {
            string folder = Directory.GetCurrentDirectory();
            string[] souls = Directory.GetFiles(folder, "*.txt");
            return souls;
        }
        public string[] ReadFileNames(string[] paths)
        {
            string[] FileNames = new string[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                FileNames[i] = Path.GetFileNameWithoutExtension(paths[i]);
            }
            return FileNames;
        }
        public void CreateSoul(int highlightchosencolor, int textchosencolor)
        {
            string[] prompts =
            {
               "Type in your Soul's ", "name", ". ", "Press ", "ENTER", " to continue.\n", "\n"
            };
            string[] options =
            {
               "##" //symbol for text input options
            };
            int[] textcolors =
            {
                7, 1, 7, 7, 1, 7, 1, 7
            };
            int[] specialsymbol =
            {
                0, 1
            };
            string UserInput = "";
            ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, textchosencolor, highlightchosencolor, specialsymbol, null, null);
            Console.Clear();
            ConsoleOutput.RenderText(prompts, textcolors);
            UserInput = ConsoleOutput.RenderInputOption();
            prompts = new string[] { "Do you confirm your typed-in name? : ", UserInput + "\n"};
            textcolors = new int[] { 7, 5 };
            options = new string[] { "Confirm", "Decline" };
            specialsymbol = new int[] { 0, 1, 0, 1 };
            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
            ConsoleOutput.RenderText(prompts, textcolors);
            ConsoleOutput.RenderOptions(options, specialsymbol);
            int SelectedIndex = ConsoleOutput.Run();
            switch (SelectedIndex)
            {
                case 0:
                    DateTime now = DateTime.Now;
                    string Time;
                    if (now.Hour < 10)
                    {
                        Time = "0" + now.Hour + "∶";
                    }
                    else
                    {
                        Time = "" + now.Hour + "∶";
                    }
                    if (now.Minute < 10)
                    {
                        Time = Time + "0" + now.Minute;
                    }
                    else
                    {
                        Time = Time + now.Minute;
                    }
                    string Filename = "[" + Time + ", " + now.Day + "." + now.Month + "." + now.Year + "]" + " “" + UserInput + "” - Day 0";
                    string[] datastructure =
                    {
                        //First branch == Progression status data
                        "InGameday: 0",
                        "Time: 9:00AM",
                        "",
                        //Second branch == Emily status/config data
                        "BulletsInMag: 5",
                        "",
                        //Third branch == Loot data
                    };
                    File.WriteAllLines(Filename + ".txt", datastructure);
                    break;
            }
        }
        public void DeleteSoul(int highlightchosencolor, int textchosencolor, string[] soulfiles, string[] soulfilenames)
        {
            string[] prompts = new string[11 + soulfiles.Length];
            prompts[0] = "Type in your Soul's ";
            prompts[1] = "name";
            prompts[2] = ". ";
            prompts[3] = "Press ";
            prompts[4] = "ENTER";
            prompts[5] = " to continue.\n";
            prompts[6] = "Currently ";
            prompts[7] = "available ";
            prompts[8] = "Souls";
            prompts[9] = " :\n";
            prompts[prompts.Length - 1] = "\n";
            int soulfileIndex = 0;
            for (int i = 10; i < soulfiles.Length + 10; i++)
            {
                prompts[i] = soulfilenames[soulfileIndex] + "\n";
                soulfileIndex++;
            }
            string[] options =
            {
               "##" //symbol for text input options
            };
            int[] textcolors = new int[11 + soulfiles.Length];
            textcolors[0] = 7;
            textcolors[1] = 1;
            textcolors[2] = 7;
            textcolors[3] = 7;
            textcolors[4] = 1;
            textcolors[5] = 7;
            textcolors[6] = 7;
            textcolors[7] = 1;
            textcolors[8] = 5;
            textcolors[9] = 7;
            textcolors[textcolors.Length - 1] = 7;
            for (int i = 10; i < soulfiles.Length + 10; i++)
            {
                textcolors[i] = 8;
            }
            int[] specialsymbol =
            {
                0, 1
            };
            string UserInput = "";
            ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, textchosencolor, highlightchosencolor, specialsymbol, null, null);
            Console.Clear();
            ConsoleOutput.RenderText(prompts, textcolors);
            UserInput = ConsoleOutput.RenderInputOption();
            prompts = new string[] { "Do you confirm the typed-in name? : ", UserInput + "\n" };
            textcolors = new int[] { 7, 5 };
            options = new string[] { "Confirm", "Decline" };
            specialsymbol = new int[] { 0, 1, 0, 1 };
            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
            ConsoleOutput.RenderText(prompts, textcolors);
            ConsoleOutput.RenderOptions(options, specialsymbol);
            int SelectedIndex = ConsoleOutput.Run();
            switch (SelectedIndex)
            {
                case 0:
                    prompts = new string[] { "Understand that ", "this action is IRREVERSIBLE", ". Are you sure?\n" };
                    textcolors = new int[] { 7, 0, 7 };
                    options = new string[] { "Confirm", "Decline" };
                    specialsymbol = new int[] { 0, 1, 0, 1 };
                    ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                    ConsoleOutput.RenderText(prompts, textcolors);
                    ConsoleOutput.RenderOptions(options, specialsymbol);
                    SelectedIndex = ConsoleOutput.Run();
                    switch (SelectedIndex)
                    {
                        case 0:
                            bool foundIndexBool = false;
                            for (int i = 0; i < soulfiles.Length; i++)
                            {
                                string fileName = soulfilenames[i];
                                int start = fileName.IndexOf('“');
                                int end = fileName.IndexOf('”');
                                if (start != -1 && end != -1 && end > start)
                                {
                                    fileName = fileName.Substring(start + 1, end - start - 1);
                                }

                                if (string.Equals(fileName, UserInput, StringComparison.OrdinalIgnoreCase))
                                {
                                    File.Delete(soulfiles[i]);
                                    foundIndexBool = true;
                                    break;
                                }
                            }
                            if (foundIndexBool == false)
                            {
                                prompts = new string[] { "No Soul with the name : \"", UserInput, "\" has been ", "located", ".\n" };
                                textcolors = new int[] { 7, 5, 7, 1, 7 };
                                options = new string[] { "Back" };
                                specialsymbol = new int[] { 0, 1 };
                                ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                                ConsoleOutput.RenderText(prompts, textcolors);
                                ConsoleOutput.RenderOptions(options, specialsymbol);
                                SelectedIndex = ConsoleOutput.Run();
                            }
                            break;
                    }
                    break;  
            }
        }
        public string[] LoadData(string path)
        {
            string[] data = File.ReadAllLines(path);
            return data;
        }
        public string[] LoadAsset(string name)
        {
            Directory.SetCurrentDirectory("..");
            string dir = Path.Combine("Assets", name + ".txt");
            string[] data = File.ReadAllLines(dir);
            Directory.SetCurrentDirectory("Savefiles");
            return data;
        }
    }
}
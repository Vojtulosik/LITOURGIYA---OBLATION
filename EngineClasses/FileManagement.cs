using LITOURGIYA___OBLATION.EngineClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LITOURGIYA___OBLATION
{
    internal class FileManagement
    {
        public int AmountOfSouls;
        public const int CurrentGameVersion = 1;
        public void SaveProgress(string path, DataStructure data)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Savefiles");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory("Savefiles");
            }
            string FileName = "[" + data.FileCreationHour + "∶" + data.FileCreationMinutes + ", " + data.FileCreationDate + "] “" + data.FileCreationName + "” - " + "Day " + data.InGameDay + ".json";
            folder = Path.Combine(folder, FileName);
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(folder, json);
            if (!string.IsNullOrEmpty(path) && File.Exists(path) && path != folder)
            {
                File.Delete(path);
            }
        }
        public DataStructure LoadData(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<DataStructure>(json);
        }
        public string[] SearchFiles()
        {
            string folder = Directory.GetCurrentDirectory();
            string[] souls = Directory.GetFiles(Path.Combine(folder, "Savefiles"), "*.json");
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
                    string hour;
                    string minute;
                    if (now.Hour < 10)
                    {
                        Time = "0" + now.Hour + "∶";
                        hour = "0" + now.Hour;
                    }
                    else
                    {
                        Time = "" + now.Hour + "∶";
                        hour = "" + now.Hour;
                    }
                    if (now.Minute < 10)
                    {
                        Time = Time + "0" + now.Minute;
                        minute = "0" + now.Minute;
                    }
                    else
                    {
                        Time = Time + now.Minute;
                        minute = "" + now.Minute;
                    }
                    string Filename = "[" + Time + ", " + now.Day + "." + now.Month + "." + now.Year + "]" + " “" + UserInput + "” - Day 0";
                    string[] Files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Savefiles"));
                    bool FileExists = false;
                    for (int i = 0; i < Files.Length; i++)
                    {
                        string filePath = Path.GetFullPath(Files[i]);
                        DataStructure Data = LoadData(filePath);
                        if (Data.FileCreationName == UserInput) FileExists = true;
                    }
                    int AmountOfFiles = Files.Length;
                    if (FileExists == false)
                    {
                        ConfigFileData ConfigData = LoadConfigFile();
                        if (AmountOfFiles >= ConfigData.MaxSaveFileCount)
                        {
                            prompts = new string[] { "Cannot create Soul : ", "Too many ", "souls ", "exist.", " Kill ", "one and then create a new one.\n\n" };
                            textcolors = new int[] { 0, 7, 5, 7, 1, 7 };
                            options = new string[] { "Close" };
                            specialsymbol = new int[] { 0, 1 };
                            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                            ConsoleOutput.RenderText(prompts, textcolors);
                            ConsoleOutput.RenderOptions(options, specialsymbol);
                            ConsoleOutput.Run();
                        }
                        else
                        {
                            DataStructure DataStructure = new DataStructure()
                            {
                                InGameDay = 0,
                                Time = "9:00AM",
                                FirstAimPractise = false,
                                BulletsInMag = 5,
                                FileCreationHour = hour,
                                FileCreationMinutes = minute,
                                FileCreationDate = now.Day + "." + now.Month + "." + now.Year,
                                FileCreationName = UserInput
                            };
                            string json = JsonSerializer.Serialize(DataStructure, new JsonSerializerOptions
                            {
                                WriteIndented = true
                            });
                            string filedir = Path.Combine(Directory.GetCurrentDirectory(), "Savefiles");
                            File.WriteAllText(Path.Combine(filedir, Filename) + ".json", json);
                        }
                    }
                    else
                    {
                        prompts = new string[] { "Cannot create Soul : ", "Soul with the same name \"", UserInput, "\" already exists.\n\n" };
                        textcolors = new int[] { 0, 7, 1, 7 };
                        options = new string[] { "Close" };
                        specialsymbol = new int[] { 0, 1 };
                        ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                        ConsoleOutput.RenderText(prompts, textcolors);
                        ConsoleOutput.RenderOptions(options, specialsymbol);
                        ConsoleOutput.Run();
                    }
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
        public string[] LoadAsset(string name)
        {
            string dir = Path.Combine("Assets", name + ".txt");
            string[] data = File.ReadAllLines(dir);
            return data;
        }
        public void CreateConfigFile()
        {
            ConfigFileData ConfigData = new ConfigFileData();
            ConfigData.TextHighlightColor = 6;
            ConfigData.TextColor = 9;
            ConfigData.MaxSaveFileCount = 10;
            ConfigData.SelectionSymbol = '*';
            string json = JsonSerializer.Serialize(ConfigData, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Config.json");
            if (!File.Exists(path)) File.WriteAllText(path, json);
        }
        public void UpdateConfigFile(ConfigFileData ConfigData)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Config.json");
            string json = JsonSerializer.Serialize(ConfigData, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(path, json);
        }
        public ConfigFileData LoadConfigFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Config.json");
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ConfigFileData>(json);
        }
        public DataStructure CheckSafeFileVersion(DataStructure data, string path)
        {
            if (data.Version != CurrentGameVersion) data = RepairSaveFile(data);
            return data;
        }
        private DataStructure RepairSaveFile(DataStructure data)
        {
            if (data.Inventory == null)
                data.Inventory = new List<string>();

            if (data.InventoryValues == null)
                data.InventoryValues = new List<int>();

            if (data.EnvironmentalLootData == null)
                data.EnvironmentalLootData = new Dictionary<string, int[]>();

            if (data.EnvironmentalLootDataNames == null)
                data.EnvironmentalLootDataNames = new Dictionary<string, string[]>();

            if (data.EnvironmentalStatusData == null)
                data.EnvironmentalStatusData = new Dictionary<string, bool>();

            if (!data.EnvironmentalLootData.ContainsKey("TinyStockroomLeftRackLoot"))
            {
                data.EnvironmentalLootData["TinyStockroomLeftRackLoot"] = new int[] { 5, 4, 2, 1 };
            }

            if (!data.EnvironmentalLootDataNames.ContainsKey("TinyStockroomLeftRackLoot"))
            {
                data.EnvironmentalLootDataNames["TinyStockroomLeftRackLoot"] = new string[] { "Scrap polymers", "Wood scrap", "Bottle of acid", "Glue" };
            }

            if (!data.EnvironmentalStatusData.ContainsKey("TinyStockroomExplored"))
            {
                data.EnvironmentalStatusData.TryAdd("TinyStockroomExplored", false);
            }
            data.Version = CurrentGameVersion;
            return data;
        }
        public char LoadIndexChar()
        {
            ConfigFileData ConfigData = LoadConfigFile();
            return ConfigData.SelectionSymbol;
        }
    }
}
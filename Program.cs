
namespace LITOURGIYA___OBLATION
{
    internal class Program
    {
        public static void Main(string[] args)

        {
            Console.Title = "LITOURGIYA - Oblation";
            Console.ResetColor();
            MainMenu MainMenu = new MainMenu();
            FileManagement FileManager = new FileManagement();
            FileManager.CreateConfigFile();
            MainMenu.StartupMenu();
            //Console.WriteLine("☼");//
        }

    }
}
//My source code is a total tragedy, I'm very sorry for anyone who did the self-harming decision to learn this mess
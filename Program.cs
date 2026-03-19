
namespace LITOURGIYA___OBLATION
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.ResetColor();
            Directory.CreateDirectory("Savefiles");
            MainMenu MainMenu = new MainMenu();
            MainMenu.StartupMenu();
        }

    }
}
//My source code is a total tragedy, I'm very sorry for anyone who did the self-harming decision to learn this mess
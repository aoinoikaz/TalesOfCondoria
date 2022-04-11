using System;

namespace Tales_of_Condoria
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new GameLoop();
            game.Run();
        }
    }
}

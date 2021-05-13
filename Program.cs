using System;

namespace Roar_Flight2
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GestorDePantallas())
                game.Run();
        }
    }
}

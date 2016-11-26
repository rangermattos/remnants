using System;
using System.Windows;
namespace Remnants
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (Game1 game = new Game1())
                    game.Run();
            }

            catch (Exception e)
            {
				Console.Write(e.ToString());
                //System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }
    }
}

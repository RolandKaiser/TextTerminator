using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace TextTerminator
{
    class Program
    {

        static string input;                    

        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        // static = Klassenmethode, Typ muss angegeben werden, Main required
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "test");
            
            // Eventhandler
            window.Closed += OnClose;
            window.KeyPressed += OnKeyPressed;

            while (window.IsOpen)
            {
                // Verarbeite Eingaben
                window.DispatchEvents();
                
                // Clear Display
                window.Clear();
                // Render
                window.Display();
            }
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code.ToString().
            { 
                return;
            }

            if (e.Shift)
            {
                input += e.Code.ToString().ToUpper();
            }
            else
            {
                input += e.Code.ToString().ToLower();
            }

            Console.WriteLine(input);
        }
    }
}

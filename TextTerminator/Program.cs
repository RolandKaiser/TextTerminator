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
            window.TextEntered += OnTextEntered;

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

        private static void OnTextEntered(object sender, TextEventArgs e)
        {
            input += e.Unicode;
            Console.WriteLine(input);
        }
        
    }
}

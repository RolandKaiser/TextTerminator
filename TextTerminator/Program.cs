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
            Font font = new Font("arial.ttf");
            
            // Eventhandler
            window.Closed += OnClose;
            window.TextEntered += OnTextEntered;
            window.KeyPressed += OnKeyPressed;

            while (window.IsOpen)
            {
                // Verarbeite Eingaben
                window.DispatchEvents();

                // Clear Display
                window.Clear();

                // Show Text
                Text text = new Text(input, font, 50);
                window.Draw(text);

                // Render
                window.Display();
            }
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.BackSpace)
            {
                input = input.Substring(1, input.Length - 1);
                Console.WriteLine(input);
            }
        }

        private static void OnTextEntered(object sender, TextEventArgs e)
        {
            input += e.Unicode;
            Console.WriteLine(input);
        
        }

    }
}

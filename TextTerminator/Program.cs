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

        static List<Text> BuildTextCollection(string[] words, Font font)
        {

            List<Text> textobjects = new List<Text>();
            Random rnd = new Random();

            foreach (string word in words)
            {
                Text text = new Text(word, font, 50);
                int x = rnd.Next(300);
                int y = rnd.Next(400);
                text.Position = new Vector2f(x, y);
                textobjects.Add(text);
            }

            return textobjects;

        }

        // static = Klassenmethode, Typ muss angegeben werden, Main required
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "test");
            Font font = new Font("arial.ttf");

            string[] words = { "Lorem", "Ipsum", "Ego", "Caprum", "Non", "Iam", "Habeo", "TextTerminator", "ContactDude", "Validation" };

            // Unten stehender Code ist nicht funktionstüchtig und nur für Erklärungszwecke verwendet.
            // Idee!    mit 2. Collection arbeiten - solange das betrachtete Element aus 1. Cl mit allen Elementen aus 2. Collection vergleichen
            //          Prüfung bestanden (schneiden sich nicht), dann push into Collection 2
            //          Prüfung nicht bestanden, neu Berechnung und wieder mit allen Elementen aus C2 vergleichen

            
            List<Text> textobjects2 = new List<Text>();
            List<Text> textobjects = BuildTextCollection(words, font);
            Random rnd = new Random();

            foreach (Text textobject in textobjects)
            {

                FloatRect textrect = textobject.GetGlobalBounds();

                bool intersects = true;

                while (intersects)
                {
                    foreach (Text textobject2 in textobjects2)
                    {
                        if (!(textrect.Intersects(textobject2.GetGlobalBounds())))
                        {
                            intersects = false;
                        }
                        else
                        {
                            int x = rnd.Next(300);
                            int y = rnd.Next(400);
                            textobject.Position = new Vector2f(x, y);
                            textrect = textobject.GetGlobalBounds();
                            intersects = true;
                            break;

                        }
                    }

                    if ((!intersects) || (textobjects2.Count == 0))
                    {
                        textobjects2.Add(textobject);
                        intersects = false;
                    }
                                         
                }
            }

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

                foreach (Text item in textobjects2)
                {
                    window.Draw(item);
                }

                // Render
                window.Display();
            }
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                RenderWindow window = (RenderWindow)sender;
                window.Close();
            }
        }

        private static void OnTextEntered(object sender, TextEventArgs e)
        {
            if (e.Unicode == "\b")
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                }
            }
            else
            { 
                input += e.Unicode;
            }
            Console.WriteLine(input);
        }

    }
}

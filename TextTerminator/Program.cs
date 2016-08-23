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

            string[] words = { "Lorem", "Ipsum", "Ego", "Caprum", "Non", "Iam", "Habeo", "TextTerminator", "ContactDude", "Validation" };

            // Unten stehender Code ist nicht funktionstüchtig und nur für Erklärungszwecke verwendet.
            // Idee!    mit 2. Array arbeiten - immer 1. Element mit allen Elementen aus 2. Array vergleichen
            //          Prüfung bestanden, dann Remove aus Array 1 und push into Array 2
            //          Prüfung nicht bestanden, neu Berechnung und wieder mit allen Elementen aus A2 vergleichen

            //Text[] textobjects = new Text[words.Length];
            //Text[] textobjects2 = new Text[words.Length];
            //Random rnd = new Random();

            //for (int i = 0; i < words.Length -1 ; i++)
            //{
            //    Text text = new Text(words[i], font, 50);
            //    int x = rnd.Next(100);
            //    int y = rnd.Next(100);
            //    text.Position = new Vector2f(x, y);
            //    FloatRect textrect = text.GetGlobalBounds();
            //    textobjects[i] = text;


            //    for (int z = 1; z < textobjects2.Length -1; z++)
            //    {
            //        // Exception for null
            //        if (!(textrect.Intersects(textobjects2[z].GetGlobalBounds())))
            //        {
            //            textobjects2[z] = text;
            //            textobjects.ToList().RemoveAt(0);
            //            textobjects.ToArray();
            //        }
            //        else
            //        {
            //            x = rnd.Next(100);
            //            y = rnd.Next(100);
            //            text.Position = new Vector2f(x, y);
            //            textrect = text.GetGlobalBounds();
            //        }
            //    }

            //}


            List<Text> textobjects = new List<Text>();
            Random rnd = new Random();

            foreach (string word in words)
            {
                Text text = new Text(word, font, 50);
                int x = rnd.Next(100);
                int y = rnd.Next(100);
                text.Position = new Vector2f(x, y);
                FloatRect textrect = text.GetGlobalBounds();

                foreach (Text textobject in textobjects)
                {

                    while (textrect.Intersects(textobject.GetGlobalBounds()))
                    {
                        x = rnd.Next(100);
                        y = rnd.Next(100);
                        text.Position = new Vector2f(x, y);
                        textrect = text.GetGlobalBounds();
                    }

                }

                textobjects.Add(text);
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

                foreach (Text item in textobjects)
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

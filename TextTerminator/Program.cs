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
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;


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
                setNewItemPosition(rnd, text);
                textobjects.Add(text);
            }

            return textobjects;

        }

        static bool checkWindowBorderCollision(Random rnd, Text item, RenderWindow window)
        {
            bool WindowBorderCollision = false;
                     
            if ((item.GetGlobalBounds().Left + item.GetGlobalBounds().Width) > WINDOW_WIDTH) { WindowBorderCollision = true; }
            if ((item.GetGlobalBounds().Top + item.GetGlobalBounds().Height) > WINDOW_HEIGHT) { WindowBorderCollision = true; }

            return WindowBorderCollision;
        }

        static void setNewItemPosition(Random rnd, Text item)
        {
            int x = rnd.Next(WINDOW_WIDTH);
            int y = rnd.Next(WINDOW_HEIGHT);
            item.Position = new Vector2f(x, y);
        }

        // static = Klassenmethode, Typ muss angegeben werden, Main required
        static void Main(string[] args)
        {

            RenderWindow window = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "test");
            Font font = new Font("arial.ttf");

            string[] words = { "Lorem", "Ipsum", "Ego", "Caprum", "Non", "Iam", "Habeo", "TextTerminator", "ContactDude", "Validation" };

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
                        if (!(textrect.Intersects(textobject2.GetGlobalBounds())) && (!(checkWindowBorderCollision(rnd, textobject, window))))
                        {
                            intersects = false;
                        }
                        else
                        {
                            setNewItemPosition(rnd, textobject);
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

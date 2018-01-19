using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace Block_sorting_system
{
    class Input
    {
        private static List<Key> keysDown;
        private static List<Key> keysDownLast;

        private static List<MouseButton> buttonsDown;
        private static List<MouseButton> buttonsDownLast;

        public static void Initialize(GameWindow game)
        {
            keysDown = new List<Key>();
            keysDownLast = new List<Key>();
            buttonsDown = new List<MouseButton>();
            buttonsDownLast = new List<MouseButton>();

            game.KeyDown += game_KeyDown;
            game.KeyUp += game_KeyUp;
            game.MouseDown += game_MouseDown;
            game.MouseUp += game_MouseUp;
        }

        static void game_MouseUp(object sender, MouseButtonEventArgs e)
        {
            while (buttonsDown.Contains(e.Button))
            {
                buttonsDown.Remove(e.Button);
            }
        }

        static void game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonsDown.Add(e.Button);
        }

        static void game_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            while (keysDown.Contains(e.Key))
            {
                keysDown.Remove(e.Key);
            }
        }

        static void game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
        }


        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            buttonsDownLast = new List<MouseButton>(buttonsDown);
        }

        public static bool KeyPress(Key key)
        {
            return(keysDown.Contains(key) && (keysDownLast.Contains(key)));
        }

        public static bool KeyRelease(Key key)
        {
            return(keysDown.Contains(key) && (keysDownLast.Contains(key)));
        }

        public static bool KeyDown(Key key)
        {
            return(keysDown.Contains(key));
        }

        public static bool MousePress(MouseButton button)
        {
            return(buttonsDown.Contains(button) && (buttonsDownLast.Contains(button)));
        }

        public static bool MouseRelease(MouseButton button)
        {
            return(buttonsDown.Contains(button) && (buttonsDownLast.Contains(button)));
        }

        public static bool MouseDown(MouseButton button)
        {
            return(buttonsDown.Contains(button));
        }


    }
}

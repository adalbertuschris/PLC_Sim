using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Drawing;

namespace Block_sorting_system
{
    class CustomButton
    {
        private Texture2D texButton;
        private Vector2 position;
        private RectangleF size;
        

        public bool buttonHover = false;

        public CustomButton(Vector2 pos, RectangleF size)
        {
            position = pos;
            this.size = size;
            
            texButton = ContentPipe.LoadTexture("button.png");
        }       

        public void Draw()
        {
            if (buttonHover)
            {
                Spritebatch.Draw(texButton, position, new Vector2(1f, 1f), Color.White, new Vector2(0, 0), size);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Block_sorting_system
{
    public class Piston : IPosition
    {
        public enum Direction
        {
            A0,
            A1,
        }

        public static Vector2 startPosition = new Vector2(684, 345);
        public static Vector2 endPosition = new Vector2(642, 345);        

        public Vector2 newPosition;

        public Vector2 Position { get; set; }
        public Vector2 velocity;
        private Vector2 size;
        public Direction direction;
        public Direction prevDirection;

        private Texture2D playerSprite;

        public static bool butonik = false;

        public Piston()
        {
            Position = startPosition;
            newPosition = Position;
            direction = Direction.A0;
            velocity = Vector2.Zero;

            size = new Vector2(40, 40);
            playerSprite = ContentPipe.LoadTexture("piston.png");
        }
        static bool possibleMove = false;
        public void Update(Block block)
        {
            if (Position == this.newPosition)
            {
                possibleMove = false;
                butonik = false;
                prevDirection = direction;
                return;
            }
            else
            {
                switch (direction)
                {
                    case Direction.A0:
                        this.velocity = new Vector2(1f, 0);
                        Position += velocity;
                        break;
                    case Direction.A1:

                        this.velocity = new Vector2(1f, 0);
                        
                        if (Block.platformIsBusy)
                        {
                            possibleMove = true;
                        }
                        else if (block != null)
                        {
                            if (!(block.Position.X == Block.startPosition.X && block.Position.Y <= Block.startPosition.Y))
                            {
                                possibleMove = true;
                            }
                        }
                        else
                        {
                            possibleMove = true;
                        }
                        if (!Block.pistonLocked && possibleMove)
                        {
                            Position -= velocity;
                        }
                        break;
                }
            }
        }

        



        public void Draw(Texture2D texture2)
        {
            Spritebatch.Draw(texture2, new Vector2(Position.X - 1, Position.Y), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(Position.X - 1, Position.Y, 64 + 2, 32));
            Spritebatch.Draw(playerSprite, Position, new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(0, 0, 64, 32));
            Spritebatch.Draw(texture2, new Vector2(694, 345), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(694, 345, 56, 36));
            
        }
    }
}

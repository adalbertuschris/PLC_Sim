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
    public class Player : IPosition
    {
        public enum Direction
        {
            A0,
            A1,
            A2,
            B0,
            B1
        }

        public static Vector2 startPosition = new Vector2(365, 34);
        public static float lockManipulatorRight = 355f;
        public static float lockManipulatorLeft = 368f;
        public Vector2 newPosition;
        public static Vector2 downPosition = new Vector2(365, 90);
        public static Vector2 firstContainerPosition = new Vector2(243, 34);
        public static Vector2 secondContainerPosition = new Vector2(121, 34);
        public Vector2 Position { get; set; }
        public Vector2 velocity;
        private Vector2 size;
        public Direction direction;
        public Direction prevDirection;
        private bool endArmA = false;
        public static bool ManipulatorLocked = false;
        private Texture2D sprite;
        private Texture2D spriteForOpen;
        private Texture2D spriteForClose;



        public static bool butonik = false;

        public Player()
        {
            Position = startPosition;
            newPosition = Position;
            direction = Direction.A0;
            velocity = Vector2.Zero;

            size = new Vector2(40, 40);
            spriteForOpen = ContentPipe.LoadTexture("chwytak4.png");
            spriteForClose = ContentPipe.LoadTexture("chwytak5.png");
            sprite = spriteForOpen;
        }

        public void Update(Gripper gripper, List<Block> blocks, Texture2D texture)
        {
            if (gripper.state == Gripper.StateList.Open)
            {
                sprite = spriteForOpen;
            }
            else if (gripper.state == Gripper.StateList.Close)
            {
                sprite = spriteForClose;
            }

            if (Position == this.newPosition)
            {
                butonik = false;
                this.prevDirection = this.direction;
                return;

            }
            else
            {
                if (!ManipulatorLocked)
                {
                    switch (this.direction)
                    {
                        case Direction.A0:
                            if (!ColisionRight(gripper))
                            {
                                this.velocity = new Vector2(1f, 0);
                                Position += velocity;
                            }
                            endArmA = false;
                            break;
                        case Direction.A1:
                            if (!ColisionLeft(gripper))
                            {
                                if ((prevDirection == Direction.A0 || prevDirection == Direction.B0 || prevDirection == Direction.B1) && !endArmA)
                                {
                                    this.velocity = new Vector2(1f, 0);
                                    Position -= velocity;
                                }
                                else if ((prevDirection == Direction.A2 || prevDirection == Direction.B0 || prevDirection == Direction.B1) && endArmA)
                                {
                                    this.velocity = new Vector2(1f, 0);
                                    Position += velocity;
                                }
                            }
                            else
                            {
                                butonik = false;
                            }
                            break;
                        case Direction.A2:
                            if (!ColisionLeft(gripper))
                            {
                                this.velocity = new Vector2(1f, 0);
                                Position -= velocity;
                                endArmA = true;
                            }
                            else
                            {
                                butonik = false;
                            }
                            break;
                        case Direction.B0:
                            this.velocity = new Vector2(0, 1f);
                            Position -= velocity;
                            break;
                        case Direction.B1:
                            if (!ColisionDown(blocks))
                            {
                                this.velocity = new Vector2(0, 1f);
                                Position += velocity;
                            }
                            break;
                    }
                }
            }
            //this.Draw(texture);
        }

        public bool ColisionDown(List<Block> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                double x = Position.X;
                double y = Position.Y + 280;
                if ((blocks[i].Position.X < Position.X + 90) && (blocks[i].Position.X > Position.X + 26))
                {
                    if (y > blocks[i].Position.Y)
                    {
                        //Console.Write(blocks[i].Position.X.ToString() + "z");
                        return true;
                    }

                }
            }
            return false;
        }

        public bool ColisionRight(Gripper gripper)
        {
            if (gripper.state == Gripper.StateList.Close)
            {
                double x = Position.X + 80;
                double y = Position.Y;
                if ((lockManipulatorRight < x))
                {
                    if (y == downPosition.Y)
                    {
                        //Console.Write("jest ok");
                        Gripper.isPossibleToChangeState = false;
                        return true;
                    }

                }
            }
            else if (gripper.state == Gripper.StateList.Open)
            {
                double x = Position.X + 84;
                double y = Position.Y;
                if ((lockManipulatorRight < x))
                {
                    if (y == downPosition.Y)
                    {
                        //Console.Write("jest ok");
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ColisionLeft(Gripper gripper)
        {
            if (gripper.state == Gripper.StateList.Close)
            {
                double x = Position.X + 18;
                double y = Position.Y;
                if ((lockManipulatorLeft < x))
                {
                    if (y == downPosition.Y)
                    {
                        //Console.Write("jest ok");
                        //Gripper.isPossibleToChangeState = false;
                        return true;
                    }

                }
            }
            else if (gripper.state == Gripper.StateList.Open)
            {
                double x = Position.X + 18;
                double y = Position.Y;
                if ((lockManipulatorLeft < x))
                {
                    if (y == downPosition.Y)
                    {
                        //Console.Write("jest ok");
                        return true;
                    }
                }
            }
            return false;
        }




        public void Draw(Texture2D texture2)
        {
            Spritebatch.Draw(texture2, new Vector2(Position.X - 2, Position.Y - 2), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(Position.X - 2, Position.Y - 2, 93, 284));//72,171
            Spritebatch.Draw(sprite, Position, new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(0, 0, 90, 280));//68,167

            Spritebatch.Draw(sprite, new Vector2(Position.X, 88), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(0, 300, 90, 110));//68,167
           
        }
    }
}

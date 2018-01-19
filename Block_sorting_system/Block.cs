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
    public class Block : IPosition
    {
        public static int Count
        {
            get;
            set;
        }

        static Random rand = new Random();
        public static bool platformIsBusy = false;
        public bool set = false;
        public bool colision = false;
        public bool abandon = false;
        public bool locked=false;
        public static bool pistonLocked = false;


        public enum Direction
        {
            X0,
            X1,
            X2,
            X3,
            X4,
            Y0,
            Y1
        }

        public enum Type
        {
            Black,
            Silver,
            None
        }


        public static Vector2 startPosition = new Vector2(643, 345);
        public static Vector2 onConveyor = new Vector2(601, 345);
        public static Vector2 endPosition = new Vector2(389, 345);
        private static Vector2 possibleCollisionWithOpenGripper = new Vector2(449,345);
        private static float removeBlockY = 404;

        public Vector2 manipulatorPosition = Player.downPosition;
        bool grab = false;

        public Vector2 newPosition;
        public Type type;
        public Vector2 Position { get; set; }
        public Vector2 velocity;
        public static Vector2 prevPosition;
        private Vector2 size;
        public Direction direction;

        private Texture2D sprite;

        public static bool butonik = false;

        public Block()
        {
            type = ChooseTypeBlock();
            if (type == Type.Black)
            {
                sprite = ContentPipe.LoadTexture("klocek1.png");
            }
            else if (type == Type.Silver)
            {
                sprite = ContentPipe.LoadTexture("klocek2.png");
            }


            if (Count == 0)
            {
                Position = startPosition;
                prevPosition = Position;
                platformIsBusy = true;
                direction = Direction.X0;
            }
            else
            {
                //ustawianie bloczków w podajniku
                Position = new Vector2(prevPosition.X, (prevPosition.Y - sprite.Height - 1));
                prevPosition = Position;
                direction = Direction.Y1;
            }
            newPosition = Position;

            velocity = Vector2.Zero;
            size = new Vector2(40, 40);

            Count++;
        }

        public Type ChooseTypeBlock()
        {
            Type typeSelected = Type.None;

            int selectedType = rand.Next(0, 2);
            switch (selectedType)
            {
                case 0:
                    typeSelected = Type.Black;
                    break;
                case 1:
                    typeSelected = Type.Silver;
                    break;
            }
            return typeSelected;
        }

        public void Update(Player player, Conveyor conveyor, Gripper gripper, Piston piston, List<Block> blocks)
        {
            if (Position == Block.startPosition)
            {
                direction = Direction.X1;
                platformIsBusy = true;
            }

            else if (Position == onConveyor)
            {
                direction = Direction.X2;
                platformIsBusy = false;
            }

            else if (Position == endPosition)
            {
                direction = Direction.Y0;
            }


            Block prevBlock = null;
            switch (this.direction)
            {
                case Direction.X1:                       
                    prevBlock = null;
                    for (int i = (blocks.Count - 1); i >= 0; i--)
                    {
                        if (this == blocks[0])
                        {
                            Vector2 distance2 = piston.Position - Position + new Vector2(-41, 0);
                            Position += distance2;
                        }
                        else if ((this == blocks[i]))
                        {
                            double x = this.Position.X;
                            double y = this.Position.Y;
                            if (!(x < (blocks[i - 1].Position.X + 41)) && y == blocks[i - 1].Position.Y)
                            {
                                Vector2 distance2 = piston.Position - Position + new Vector2(-41, 0);
                                Position += distance2;
                            }
                            else
                            {
                                if (!blocks[i - 1].locked)
                                {
                                    Vector2 distance2 = piston.Position - Position + new Vector2(-41, 0);
                                    Position += distance2;
                                }
                                else
                                {
                                    //Console.WriteLine("Piston locked");
                                    pistonLocked = true;
                                }
                            }                            
                        }
                    }
                    
                    
                    for (int i = (blocks.Count - 1); i >= 0; i--)
                    {
                        if (prevBlock != null)
                        {
                            double x = prevBlock.Position.X;
                            double y = prevBlock.Position.Y;
                            if ((x < (blocks[i].Position.X + 41)) && y == blocks[i].Position.Y)
                            {
                                if (!blocks[i].locked)
                                {
                                    blocks[i].Position = new Vector2(blocks[i].Position.X - 1, blocks[i].Position.Y);
                                }
                            }
                        }
                        prevBlock = blocks[i];
                    }
                    break;
                case Direction.X2:
                    if (conveyor.state == Conveyor.StateList.Run)
                    {
                        if (!this.locked)
                        {
                            this.velocity = new Vector2(1f, 0);
                            this.Position -= velocity;
                        }
                    }

                        if (CheckCollisionWithGripper(player))
                        {
                            locked = true;
                        }
                        else
                        {
                            locked = false;
                            pistonLocked = false;
                        }

                        for (int i = (blocks.Count - 1); i >= 0; i--)
                        {
                            if ((this == blocks[i]) && (i != 0))
                            {
                                double x = this.Position.X;
                                double y = this.Position.Y;
                                if ((x < (blocks[i-1].Position.X + 42)) && y == blocks[i-1].Position.Y)
                                {
                                    if (blocks[i-1].direction == Direction.Y0 || blocks[i-1].locked)
                                    {
                                        blocks[i].locked = true;
                                    }
                                }
                            }
                        }
                    break;
                case Direction.Y0:
                    if (this.Position != endPosition && !set)
                    {
                        set = true;
                    }
                    else if (this.Position == endPosition && set)
                    {
                        set = false;
                    }
                    if (gripper.state == Gripper.StateList.Close && player.Position == manipulatorPosition)
                    {
                        grab = true;
                        //Console.WriteLine("grab = true");
                    }
                    else if (gripper.state == Gripper.StateList.Open && player.Position == manipulatorPosition)
                    {
                        grab = false;
                    }
                    if (gripper.state == Gripper.StateList.Close && grab)
                    {
                        if (!abandon)
                        {
                            Vector2 distance3 = player.Position - this.Position + new Vector2(24, 255);
                            
                            this.Position += distance3;
                        }
                        else
                        {
                            if (this.Position.Y < removeBlockY)
                            {
                                this.Position = new Vector2(Position.X, Position.Y + 1);
                            }
                            else if (this.Position.Y == removeBlockY)
                            {
                                blocks.Remove(this);
                            }
                        }
                    }
                    else if (gripper.state == Gripper.StateList.Open && grab)
                    {
                        abandon = true;
                        if (this.Position.Y < removeBlockY)
                        {
                            Vector2 distance3 = player.Position + this.Position + new Vector2(24, 255) - new Vector2(0, 578);                            
                           
                            if (distance3.Y < 30)
                            {
                                Player.ManipulatorLocked = true;
                            }
                            else
                            {
                                Player.ManipulatorLocked = false;
                            }

                            this.Position = new Vector2(Position.X, Position.Y + 1);
                        }
                        else if (this.Position.Y == removeBlockY)
                        {
                            blocks.Remove(this);
                        }
                    }
                    break;
                case Direction.Y1:
                    if (piston.Position == Piston.startPosition && !platformIsBusy & !pistonLocked)
                    {
                        this.velocity = new Vector2(0, 1f);
                        this.Position += velocity;
                    }
                    break;
            }


        }

        //public bool Colision(List<Block> blocks)
        //{
        //    for (int i = 0; i < blocks.Count; i++)
        //    {
        //        if ((Position != blocks[i].Position))
        //        {
        //            double x = this.Position.X;
        //            double y = this.Position.Y;
        //            if ((x < (blocks[i].Position.X + 41)) && y == blocks[i].Position.Y)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        public bool CheckCollisionWithGripper(Player player)
        {
            bool tmp = false;
            if (Position == possibleCollisionWithOpenGripper)
            {
                
                if ((player.Position.X == Player.startPosition.X) &&
                    (player.Position.Y <= Player.downPosition.Y && player.Position.Y >= Player.downPosition.Y - 25))
                {                    
                    tmp = true;
                    //Console.WriteLine("kolizja:"+this.Position.X+"|");
                }
                else
                {                   
                    tmp = false;
                    //Console.WriteLine("kolizja2:" + this.Position.X + "|");
                }
            }
            return tmp;
        }




        public void Draw(Texture2D texture2)
        {
            Spritebatch.Draw(texture2, new Vector2(Position.X - 1, Position.Y - 1), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(Position.X - 1, Position.Y - 1, 40 + 2, 32 + 2));
            Spritebatch.Draw(sprite, this.Position, new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(0, 0, 40, sprite.Height));
        }
    }
}

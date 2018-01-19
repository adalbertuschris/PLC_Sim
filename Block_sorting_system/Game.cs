using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using DataTransfer;
using System.Windows.Forms;


namespace Block_sorting_system
{
    
    class Game: GameWindow
    {
        
        Texture2D texture2, texture;
        private static Game _gameWindow;

        public Player player;
        public List<Block> blocks = new List<Block>();
        public Piston piston;
        public Conveyor conveyor;
        public Gripper gripper;
        public Sensor[] sensors = new Sensor[15];
        public CustomButton button1;
        public Game(int width, int height):base(width,height)
        {
            _gameWindow = this;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            
            Input.Initialize(this);
            //MouseDown += Mouse_ButtonDown;
        }
        public Game(int width, int height, OpenTK.Graphics.GraphicsMode mode)
            : base(width, height, mode)
        {
            _gameWindow = this;
            GL.Enable(EnableCap.Texture2D);
            //GL.Enable(EnableCap.
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Input.Initialize(this);
            //MouseDown += Mouse_ButtonDown;
        }
        private static Vector2 GetWindowLocation()
        {
            return new Vector2(_gameWindow.X, _gameWindow.Y);
        }
       
        public void InitializeSensors()
        {
            sensors[0] = new Sensor(new Vector2(727, 320), new RectangleF(0, 0, 22, 22), Piston.startPosition, "I0.0");
            sensors[1] = new Sensor(new Vector2(694, 320), new RectangleF(0, 0, 22, 22), Piston.endPosition, "I0.1");
            sensors[2] = new Sensor(new Vector2(653, 393), new RectangleF(0, 0, 22, 22), Block.startPosition, "I0.2");
            sensors[3] = new Sensor(new Vector2(606, 393), new RectangleF(0, 0, 22, 22), Block.onConveyor, "I0.3");
            sensors[4] = new Sensor(new Vector2(399, 393), new RectangleF(0, 0, 22, 22), Block.endPosition, "I0.5");
            sensors[5] = new InductiveSensor(new Vector2(399, 414), new RectangleF(0, 0, 22, 22), Block.endPosition, "I0.4");
            sensors[6] = new Sensor(new Vector2(397, 7), new RectangleF(0, 0, 22, 22), Player.startPosition, "I0.6");
            sensors[7] = new Sensor(new Vector2(276, 7), new RectangleF(0, 0, 22, 22), Player.firstContainerPosition, "I0.7");
            sensors[8] = new Sensor(new Vector2(150, 7), new RectangleF(0, 0, 22, 22), Player.secondContainerPosition, "I1.0");
            sensors[9] = new Sensor(new Vector2(477, 32), new RectangleF(0, 0, 22, 22), Player.startPosition, "I1.1");
            sensors[10] = new Sensor(new Vector2(477, 309), new RectangleF(0, 0, 22, 22), Player.downPosition, "I1.2");

            sensors[11] = new Sensor(new Vector2(815, 40), new RectangleF(0, 0, 22, 22), "I1.3");
            sensors[12] = new Sensor(new Vector2(863, 40), new RectangleF(0, 0, 22, 22), "I1.4");
            sensors[13] = new Sensor(new Vector2(815, 130), new RectangleF(0, 0, 22, 22), "I1.5");
            sensors[14] = new Sensor(new Vector2(863, 130), new RectangleF(0, 0, 22, 22), "I1.6");
        }

        private Form keyboardControlForm = null;

        void okno_FormClosed(object sender, FormClosedEventArgs e)
        {
            keyboardControlForm = null;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            if (OpenTK.Input.Mouse.GetState()[OpenTK.Input.MouseButton.Left])
            {
                var mouse = OpenTK.Input.Mouse.GetCursorState();                

                int x = mouse.X - (int)GetWindowLocation().X - 7;
                int y = mouse.Y - (int)GetWindowLocation().Y - 30;
                if ((x >= 820 && x <= 883) && (y >= 440 && y <= 475))
                {
                    try
                    {
                        
                        if (System.IO.File.Exists("Content/" + "sterowanie.png"))
                        {
                            if (keyboardControlForm != null) return;
                            keyboardControlForm = new Form();
                            Bitmap bmp = new Bitmap("Content/" + "sterowanie.png");
                            keyboardControlForm.BackgroundImage = (System.Drawing.Image)bmp;
                            keyboardControlForm.Size = new System.Drawing.Size(bmp.Width, bmp.Height + 28);
                            keyboardControlForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                            keyboardControlForm.MaximizeBox = false;
                            keyboardControlForm.Text = "Keyboard control";
                            keyboardControlForm.Show();

                            keyboardControlForm.FormClosed += new FormClosedEventHandler(okno_FormClosed);
                            //Console.WriteLine("File not found ad 'Content/sterowanie.png'");
                        }
                        else if (System.IO.File.Exists("Virtual System/Block sorting system/Content/" + "sterowanie.png"))
                        {
                            if (keyboardControlForm != null) return;
                            keyboardControlForm = new Form();
                            Bitmap bmp = new Bitmap("Virtual System/Block sorting system/Content/" + "sterowanie.png");
                            keyboardControlForm.BackgroundImage = (System.Drawing.Image)bmp;
                            keyboardControlForm.Size = new System.Drawing.Size(bmp.Width, bmp.Height+28);
                            keyboardControlForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                            keyboardControlForm.MaximizeBox = false;
                            keyboardControlForm.Text = "Keyboard control";
                            keyboardControlForm.Show();

                            keyboardControlForm.FormClosed += new FormClosedEventHandler(okno_FormClosed);
                        }                        
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            var mouse2 = OpenTK.Input.Mouse.GetCursorState();                

                int x2 = mouse2.X - (int)GetWindowLocation().X - 7;
                int y2 = mouse2.Y - (int)GetWindowLocation().Y - 30;
                if ((x2 >= 820 && x2 <= 883) && (y2 >= 440 && y2 <= 475))
                {
                    button1.buttonHover = true;
                }
                else
                {
                    button1.buttonHover = false;
                }
            if (Input.KeyDown(OpenTK.Input.Key.M))
            {
                ControlMode.Mode = ControlMode.ModeList.Manual;
            }
            if (Input.KeyDown(OpenTK.Input.Key.A))
            {
                ControlMode.Mode = ControlMode.ModeList.Auto;
            }

            if (ControlMode.Mode == ControlMode.ModeList.Manual)
            {
                Manual();
            }
            else if (ControlMode.Mode == ControlMode.ModeList.Auto)
            {
                ControlSystem.SetActuators(player,piston,gripper,conveyor);
            }

            player.Update(gripper, blocks, texture);
            try
            {
                if (blocks.Count != 0)
                {
                    piston.Update(blocks[blocks.Count - 1]);
                }
                else
                {
                    piston.Update(null);
                }
            }
            catch { }
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Update(player, conveyor, gripper, piston, blocks);
            }
            
            Input.Update();
        }

        public void Manual()
        {
            if (Input.KeyDown(OpenTK.Input.Key.Keypad1) || Input.KeyDown(OpenTK.Input.Key.Number1))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.A1;
                    player.newPosition.X = Player.firstContainerPosition.X;
                    player.newPosition.Y = player.Position.Y;
                    Player.butonik = true;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad2) || Input.KeyDown(OpenTK.Input.Key.Number2))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.A2;
                    player.newPosition.X = Player.secondContainerPosition.X;
                    player.newPosition.Y = player.Position.Y;
                    Player.butonik = true;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad0) || Input.KeyDown(OpenTK.Input.Key.Number0))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.A0;
                    player.newPosition.X = Player.startPosition.X;
                    player.newPosition.Y = player.Position.Y;
                    Player.butonik = true;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Down))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.B1;
                    player.newPosition.X = player.Position.X;
                    player.newPosition.Y = Player.downPosition.Y;
                    Player.butonik = true;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Up))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.B0;
                    player.newPosition.X = player.Position.X;
                    player.newPosition.Y = Player.startPosition.Y;
                    Player.butonik = true;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad5) || Input.KeyDown(OpenTK.Input.Key.Number5))
            {
                if (!Piston.butonik)
                {
                    piston.direction = Piston.Direction.A1;
                    piston.newPosition = Piston.endPosition;

                    Piston.butonik = true;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad6) || Input.KeyDown(OpenTK.Input.Key.Number6))
            {
                if (!Piston.butonik)
                {
                    piston.direction = Piston.Direction.A0;
                    piston.newPosition = Piston.startPosition;

                    Piston.butonik = true;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad8) || Input.KeyDown(OpenTK.Input.Key.Number8))
            {
                conveyor.state = Conveyor.StateList.Run;
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad9) || Input.KeyDown(OpenTK.Input.Key.Number9))
            {
                conveyor.state = Conveyor.StateList.Stop;
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad7) || Input.KeyDown(OpenTK.Input.Key.Number7))
            {
                if (Gripper.isPossibleToChangeState)
                {
                    gripper.state = Gripper.StateList.Open;
                }
            }

            if (Input.KeyDown(OpenTK.Input.Key.Keypad4) || Input.KeyDown(OpenTK.Input.Key.Number4))
            {
                gripper.state = Gripper.StateList.Close;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            button1 = new CustomButton(new Vector2(818, 440), new RectangleF(0, 0, 64, 36));
            //texture2 = ContentPipe.LoadTexture("Process.png");
            texture2 = ContentPipe.LoadTexture("procesik2.png");
            player = new Player();
            for (int i = 0; i < 6; i++)
            {
                blocks.Add(new Block());
            }
            piston = new Piston();
            gripper = new Gripper();
            conveyor = new Conveyor();
            InitializeSensors();
            conveyor = new Conveyor();
            ControlMode.Init();

           


            
        }


        static int redisplay_interval;
        Stopwatch sw = new Stopwatch();

        //void Application_Idle(object sender, EventArgs e)
        //{
        //    double milliseconds = ComputeTimeSlice();
        //    Accumulate(milliseconds);
        //    Animate(milliseconds);
        //}

        private double ComputeTimeSlice()
        {
            sw.Stop();
            double timeslice = sw.Elapsed.TotalMilliseconds;
            sw.Reset();
            sw.Start();
            return timeslice;
        }


        //private void Animate(double milliseconds)
        //{
        //    float deltaRotation = (float)milliseconds / 20.0f;
        //    rotation += deltaRotation;
        //    UpdateFrame();
        //    glControl1.Invalidate();
        //}

        double timeGenerateFrame = 1000 / 80;
        private void SetFPS(double milliseconds)
        {
            if (milliseconds < timeGenerateFrame)
            {
                int sleepTime = (int)(timeGenerateFrame - milliseconds);
                System.Threading.Thread.Sleep(sleepTime);
            }            
        }


        double accumulator = 0;
        int idleCounter = 0;
        private void Accumulate(double milliseconds)
        {
            idleCounter++;
            accumulator += milliseconds;
            if (accumulator > 1000)
            {
                //Console.WriteLine(idleCounter.ToString());
                accumulator -= 1000;
                idleCounter = 0; // don't forget to reset the counter!
            }
        }
        int counter;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(System.Drawing.Color.Aquamarine);
            Spritebatch.Begin(this.Width, this.Height);
            Spritebatch.Draw(texture2, new Vector2(0, 0), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(0, 0, 450, 480));
            Spritebatch.Draw(texture2, new Vector2(450, 0), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(450, 0, 450, 480));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            sw.Start();
            //Animate(milliseconds);
            button1.Draw();
            Spritebatch.Draw(texture2, new Vector2(0, 0), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(0, 0, 70, 50));
            player.Draw(texture2);
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                blocks[i].Draw(texture2);
            }

            //Spritebatch.Draw(texture, new Vector2(642, 170), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(0, 0, 42, 166));
            piston.Draw(texture2);

            bool tmp = false;
            tmp = sensors[0].CheckPosition(piston);
            sensors[0].State = tmp;
            PipeClient.SetInput(sensors[0].Name, sensors[0].State);
            sensors[0].Draw();

            tmp = false;
            tmp = sensors[1].CheckPosition(piston);
            sensors[1].State = tmp;
            PipeClient.SetInput(sensors[1].Name,sensors[1].State);
            sensors[1].Draw();

            tmp = false;
            for (int i = 0; i < blocks.Count; i++)
            {
                tmp = sensors[2].CheckPosition(blocks[i]);
                if (tmp)
                {
                    break;
                }
            }
            sensors[2].State = tmp;
            PipeClient.SetInput(sensors[2].Name, sensors[2].State);
            sensors[2].Draw();

            tmp = false;
            for (int i = 0; i < blocks.Count; i++)
            {
                tmp = sensors[3].CheckPosition(blocks[i]);
                if (tmp)
                {
                    break;
                }
            }
            sensors[3].State = tmp;
            PipeClient.SetInput(sensors[3].Name, sensors[3].State);
            sensors[3].Draw();

            tmp = false;
            for (int i = 0; i < blocks.Count; i++)
            {
                tmp = sensors[4].CheckPosition(blocks[i]);
                if (tmp)
                {
                    break;
                }
            }
            sensors[4].State = tmp;
            PipeClient.SetInput(sensors[4].Name, sensors[4].State);
            sensors[4].Draw();

            tmp = false;
            for (int i = 0; i < blocks.Count; i++)
            {
                tmp = sensors[5].CheckPosition(blocks[i]);
                if (tmp)
                {
                    break;
                }
            }
            sensors[5].State = tmp;
            PipeClient.SetInput(sensors[5].Name, sensors[5].State);
            sensors[5].Draw();

            tmp = false;
            if (player.Position.X == Player.startPosition.X)
            {
                tmp = true;
            }
            sensors[6].State = tmp;
            PipeClient.SetInput(sensors[6].Name, sensors[6].State);
            sensors[6].Draw();

            tmp = false;
            if (player.Position.X == Player.firstContainerPosition.X)
            {
                tmp = true;
            }
            sensors[7].State = tmp;
            PipeClient.SetInput(sensors[7].Name, sensors[7].State);
            sensors[7].Draw();

            tmp = false;
            if (player.Position.X == Player.secondContainerPosition.X)
            {
                tmp = true;
            }
            sensors[8].State = tmp;
            PipeClient.SetInput(sensors[8].Name, sensors[8].State);
            sensors[8].Draw();

            tmp = false;
            if (player.Position.Y == Player.startPosition.Y)
            {
                tmp = true;
            }
            sensors[9].State = tmp;
            PipeClient.SetInput(sensors[9].Name, sensors[9].State);
            sensors[9].Draw();

            tmp = false;
            if (player.Position.Y == Player.downPosition.Y)
            {
                tmp = true;
            }
            sensors[10].State = tmp;
            PipeClient.SetInput(sensors[10].Name, sensors[10].State);
            sensors[10].Draw();
            
            tmp = false;
            tmp = Sensor.CheckState(conveyor);
            sensors[11].State = tmp;
            PipeClient.SetInput(sensors[11].Name, sensors[11].State);
            sensors[11].Draw();

            sensors[12].State = !tmp;
            PipeClient.SetInput(sensors[12].Name, sensors[12].State);
            sensors[12].Draw();


            tmp = false;
            tmp = Sensor.CheckState(gripper);            
            sensors[13].State = tmp;
            PipeClient.SetInput(sensors[13].Name, sensors[13].State);
            sensors[13].Draw();

            sensors[14].State = !tmp;
            PipeClient.SetInput(sensors[14].Name, sensors[14].State);
            sensors[14].Draw();
            ControlMode.UpdateControlsForMode();

            Spritebatch.Draw(texture2, new Vector2(116, 378), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(116, 378, 98, 64));
            Spritebatch.Draw(texture2, new Vector2(239, 378), new Vector2(1f, 1f), Color.White, new Vector2(0, 0), new RectangleF(239, 378, 98, 64));
            GL.Accum(AccumOp.Mult, 0.2f);

            // dodanie przeskalowanych wartości z bufora kolorów do bufora akumulacynego
            GL.Accum(AccumOp.Accum, 0.8f);

            //// kopiowanie wartości z bufora akumulacyjnego do bufora kolorów
            GL.Accum(AccumOp.Return, 1.0f);
            GL.Flush();
            this.SwapBuffers();
            double milliseconds = ComputeTimeSlice();
            SetFPS(milliseconds);
            Accumulate(milliseconds);
        }        
    }
}

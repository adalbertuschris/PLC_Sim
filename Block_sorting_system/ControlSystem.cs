using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using DataTransfer;

namespace Block_sorting_system
{
    class ControlSystem
    {
        public static void SetActuators(Player player, Piston piston, Gripper gripper, Conveyor conveyor)
        {
            if (PipeClient.GetOutput("Q0.7"))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.A1;
                    player.newPosition.X = Player.firstContainerPosition.X;
                    player.newPosition.Y = player.Position.Y;
                    Player.butonik = true;
                }
            }

            if (PipeClient.GetOutput("Q1.0"))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.A2;
                    player.newPosition.X = Player.secondContainerPosition.X;
                    player.newPosition.Y = player.Position.Y;
                    Player.butonik = true;
                }
            }

            if (PipeClient.GetOutput("Q0.6"))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.A0;
                    player.newPosition.X = Player.startPosition.X;
                    player.newPosition.Y = player.Position.Y;
                    Player.butonik = true;
                }
            }

            if (PipeClient.GetOutput("Q0.5"))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.B1;
                    player.newPosition.X = player.Position.X;
                    player.newPosition.Y = Player.downPosition.Y;
                    Player.butonik = true;
                }
            }

            if (PipeClient.GetOutput("Q0.4"))
            {
                if (!Player.butonik)
                {
                    player.direction = Player.Direction.B0;
                    player.newPosition.X = player.Position.X;
                    player.newPosition.Y = Player.startPosition.Y;
                    Player.butonik = true;
                }
            }

            if (PipeClient.GetOutput("Q0.0"))
            {
                if (!Piston.butonik)
                {
                    piston.direction = Piston.Direction.A1;
                    piston.newPosition = Piston.endPosition;

                    Piston.butonik = true;
                }
            }

            if (PipeClient.GetOutput("Q0.1"))
            {
                if (!Piston.butonik)
                {
                    piston.direction = Piston.Direction.A0;
                    piston.newPosition = Piston.startPosition;

                    Piston.butonik = true;
                }
            }

            if (PipeClient.GetOutput("Q0.2"))
            {
                conveyor.state = Conveyor.StateList.Run;
            }

            if (PipeClient.GetOutput("Q0.3"))
            {
                conveyor.state = Conveyor.StateList.Stop;
            }

            if (PipeClient.GetOutput("Q1.1"))
            {
                if (Gripper.isPossibleToChangeState)
                {
                    System.Diagnostics.Debug.WriteLine("opened");
                    gripper.state = Gripper.StateList.Open;
                }
            }

            if (PipeClient.GetOutput("Q1.2"))
            {
                System.Diagnostics.Debug.WriteLine("closed");
                gripper.state = Gripper.StateList.Close;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Hardware.CommunicationModule
{
    public delegate void DataReceivedHandler(object sender, DataReceivedEventArgs e);
    public class AsynchronousSocketListener
    {
        public event DataReceivedHandler DataReceived;
        
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        Socket listener;
        
        public void Disconnect(/*object name*/)
        {
            listener.Close();
        }
        public void StartListening(/*object name*/)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
            
            IPEndPoint _server = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 102);

            // Create a TCP/IP socket.
            listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(_server);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.                    
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();                    
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                // Signal the main thread to continue.
                allDone.Set();

                // Get the socket that handles the client request.
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = (Socket)listener.EndAccept(ar);
                StateObject state = new StateObject();

                state.workSocket = handler;


                // Create the state object.

                Receive(handler, state, ReadCallback);
                System.Diagnostics.Debug.WriteLine("socket");
            }
            catch { }
        }
        public void Receive(Socket handler, StateObject state, Action<IAsyncResult> ReadCallback)
        {
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void Send(Socket handler, byte[] byteData, bool isReceiveData, Action<IAsyncResult> SendCallback)
        {
            //if (isReceiveData)
            //{
                // Begin sending the data to the remote device.
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            //}
            //else
            //{
            //    handler.BeginSend(byteData, 0, byteData.Length, 0,
            //        new AsyncCallback(SendCallbackWithoutReceive), handler);
            //}
        }


        public void ReadCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
                int bytesRead = handler.EndReceive(ar);

                Feedback feedback;

                if (bytesRead > 0)
                {
                    byte[] dataTmp = new byte[1];
                    //string str = "";
                    //for (int i = 0; i < state.buffer.Length; i++)
                    //{
                    //    str += state.buffer[i] + ",";
                    //}
                    //System.Diagnostics.Debug.WriteLine("Odebrano: " + str);
                    
                    int packetLength = bytesRead;
                    int packetLengthIndex = 3;
                    while (true)
                    {
                        if (state.buffer[packetLengthIndex] == bytesRead)
                        {
                            dataTmp = new byte[bytesRead];
                            for (int i = 0; i < bytesRead; i++)
                            {
                                dataTmp[i] = state.buffer[(packetLengthIndex - 3) + i];
                            }
                            break;
                        }
                        else
                        {
                            packetLength = state.buffer[packetLengthIndex];
                            dataTmp = new byte[packetLength];
                            for (int i = 0; i < packetLength; i++)
                            {
                                dataTmp[i] = state.buffer[(packetLengthIndex - 3) + i];
                            }
                            packetLengthIndex += packetLength;
                            bytesRead -= packetLength;

                            if (DataReceived != null)
                            {
                                feedback = new Feedback();
                                //System.Diagnostics.Debug.WriteLine("działa");
                                DataReceived(handler, new DataReceivedEventArgs(new ReceiveInfo(dataTmp, packetLength), feedback));
                                while (!feedback.IsSet) ;
                                if (feedback.sendInfo.SomethingToSend)
                                {
                                    Send(handler, feedback.sendInfo.DataToSend, false, SendCallbackWithoutReceive);
                                }
                            }                            
                        }                        
                    }

                    if (DataReceived != null)
                    {
                        feedback = new Feedback();
                        //System.Diagnostics.Debug.WriteLine("działa");
                        DataReceived(handler, new DataReceivedEventArgs(new ReceiveInfo(dataTmp, bytesRead), feedback));
                        while (!feedback.IsSet) ;
                        if (feedback.sendInfo.SomethingToSend)
                        {
                            Send(handler, feedback.sendInfo.DataToSend, feedback.sendInfo.SomethingToReceive, SendCallback);
                        }
                        else
                        {
                            Receive(handler, state, ReadCallback);
                        }
                    }                    
                }
            }
            catch { }
        }

        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                if (handler != null)
                {
                    StateObject state = new StateObject();
                    state.workSocket = handler;
                    Receive(handler, state, ReadCallback);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendCallbackWithoutReceive(IAsyncResult ar)
        {
            System.Diagnostics.Debug.WriteLine("sendCallbackWithoutReceive");
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }        
    }
}

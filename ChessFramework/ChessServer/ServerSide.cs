using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer
{
    internal class ServerSide
    {
        public TcpListener server;
        public String dateServer;

        Thread t;
        bool workThread;
        NetworkStream streamServer;

        public ServerSide()
        {
            server = new TcpListener(System.Net.IPAddress.Any, 3000);
            server.Start();
            t = new Thread(new ThreadStart(Asculta_Server));
            workThread = true;

            t.Start();
        }

        public void Asculta_Server()
        {
            while (workThread)
            {
                Socket socketServer = server.AcceptSocket();
                try
                {
                    streamServer = new NetworkStream(socketServer);
                    StreamReader citireServer = new StreamReader(streamServer);

                    while (workThread)
                    {

                        string dateServer = citireServer.ReadLine();
                        //char temp;
                        //    do {
                        //    temp = (char)citireServer.Read();
                        //    dateServer += temp;
                        //} while (!citireServer.EndOfStream);


                        if (dateServer == null) break;//primesc nimic - clientul a plecat
                        if (dateServer == "#Gata") //ca sa pot sa inchid serverul
                            workThread = false;
                        MethodInvoker m = new MethodInvoker(() => serverForm.textBox1.Text += (socketServer.LocalEndPoint + ": " + dateServer + Environment.NewLine));
                        serverForm.textBox1.Invoke(m);
                    }
                    streamServer.Close();
                }
                catch (Exception e)
                {
#if LOG
                    Console.WriteLine(e.Message);
#endif
                }
                socketServer.Close();
            }
        }
    }
}

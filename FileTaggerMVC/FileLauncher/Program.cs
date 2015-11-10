using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace FileLauncher
{
    class Program
    {
        //public event DelegateMessage PipeMessage;
        private const string MyPipeName = "MyPipeName";

        private static void Listen()
        {
            // Create the new async pipe 
            NamedPipeServerStream pipeServer = 
                new NamedPipeServerStream(MyPipeName,
                                          PipeDirection.In,
                                          1,
                                          PipeTransmissionMode.Message,
                                          PipeOptions.None);

            StreamReader sr = new StreamReader(pipeServer);

            do
            {
                pipeServer.WaitForConnection();
                string fileName = sr.ReadLine();

                Process proc = new Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();

                pipeServer.Disconnect();
            } while (true);
        }

        //private static void WaitForConnectionCallBack(IAsyncResult iar)
        //{
        //    // Get the pipe
        //    NamedPipeServerStream pipeServer = (NamedPipeServerStream)iar.AsyncState;
        //    // End waiting for the connection
        //    pipeServer.EndWaitForConnection(iar);

        //    byte[] buffer = new byte[255];

        //    // Read the incoming message
        //    pipeServer.Read(buffer, 0, 255);

        //    // Convert byte buffer to string
        //    string stringData = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

        //    Process proc = new Process();
        //    proc.StartInfo.FileName = stringData;
        //    proc.StartInfo.UseShellExecute = true;
        //    proc.Start();

        //    // Kill original sever and create new wait server
        //    pipeServer.Close();
        //    pipeServer = null;
        //    pipeServer = new NamedPipeServerStream(MyPipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

        //    // Recursively wait for the connection again and again....
        //    pipeServer.BeginWaitForConnection(new AsyncCallback(WaitForConnectionCallBack), pipeServer);
        //}

        public static void Main(string[] args)
        {
            Listen();
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace FileLauncher
{
    class Program
    {
        private const string MyPipeName = "MyPipeName";

        private static void Listen()
        {
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

                Console.WriteLine(fileName);

                Process proc = new Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();

                pipeServer.Disconnect();
            } while (true);
        }

        public static void Main(string[] args)
        {
            Listen();
        }
    }
}

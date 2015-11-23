using System.IO;
using System.IO.Pipes;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class ProcessController : ApiController
    {
        private const string MyPipeName = "MyPipeName";

        private static void Send(string fileName)
        {
            NamedPipeClientStream pipeStream = new NamedPipeClientStream(".",
                                                                         MyPipeName,
                                                                         PipeDirection.Out,
                                                                         PipeOptions.None);

            if (!pipeStream.IsConnected)
            {
                pipeStream.Connect(100);
            }

            StreamWriter sw = new StreamWriter(pipeStream);
            sw.WriteLine(fileName);
            sw.Flush();
            sw.Close();
        }

        // GET: api/Process?fileName=abc
        public bool Get([FromUri]string fileName)
        {
            try
            {
                Send(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

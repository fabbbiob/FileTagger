using FileTaggerModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTaggerMVC.RestSharp.Abstract
{
    interface IFileRest
    {
        void Post(File file);
        void Put(File file);
        File Get(string fileName);
    }
}

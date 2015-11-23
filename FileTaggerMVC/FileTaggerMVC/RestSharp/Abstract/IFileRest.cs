using FileTaggerMVC.Models.Base;

namespace FileTaggerMVC.RestSharp.Abstract
{
    public interface IFileRest
    {
        void Post(BaseFile file);
        void Put(BaseFile file);
        BaseFile Get(string fileName);
    }
}

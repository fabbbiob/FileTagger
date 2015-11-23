using FileTaggerMVC.Models.Base;
using System.Collections.Generic;

namespace FileTaggerMVC.RestSharp.Abstract
{
    public interface ITagRest
    {
        List<BaseTag> Get();
        BaseTag Get(int id);
        void Post(BaseTag tag);
        void Put(BaseTag tag);
        void Delete(int id);
    }
}

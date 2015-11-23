using FileTaggerMVC.Models.Base;
using System.Collections.Generic;

namespace FileTaggerMVC.RestSharp.Abstract
{
    public interface ITagTypeRest
    {
        List<BaseTagType> Get();
        BaseTagType Get(int id);
        void Post(BaseTagType tagType);
        void Put(BaseTagType tagType);
        void Delete(int id);
    }
}

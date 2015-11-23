using FileTaggerModel.Model;
using System.Collections.Generic;

namespace FileTaggerMVC.RestSharp.Abstract
{
    public interface ITagTypeRest
    {
        List<TagType> Get();
        TagType Get(int id);
        void Post(TagType tagType);
        void Put(TagType tagType);
        void Delete(int id);
    }
}

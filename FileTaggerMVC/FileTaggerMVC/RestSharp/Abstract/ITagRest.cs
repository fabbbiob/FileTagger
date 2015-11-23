using FileTaggerModel.Model;
using System.Collections.Generic;

namespace FileTaggerMVC.RestSharp.Abstract
{
    public interface ITagRest
    {
        List<Tag> Get();
        Tag Get(int id);
        void Post(Tag tag);
        void Put(Tag tag);
        void Delete(int id);
    }
}

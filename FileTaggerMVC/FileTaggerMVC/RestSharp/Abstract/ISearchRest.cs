using FileTaggerMVC.Models.Base;
using System.Collections.Generic;

namespace FileTaggerMVC.RestSharp.Abstract
{
    public interface ISearchRest
    {
        List<BaseFile> GetByTags(int[] tagIds);
        List<BaseFile> GetByTag(int tagId);
    }
}

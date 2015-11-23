using FileTaggerModel.Model;
using System.Collections.Generic;

namespace FileTaggerMVC.RestSharp.Abstract
{
    interface ISearchRest
    {
        List<File> GetByTags(int[] tagIds);
        List<File> GetByTag(int tagId);
    }
}

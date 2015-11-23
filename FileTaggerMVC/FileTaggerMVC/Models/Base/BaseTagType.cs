using System.Collections.Generic;

namespace FileTaggerMVC.Models.Base
{
    public class BaseTagType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<BaseTag> Tags;
    }
}
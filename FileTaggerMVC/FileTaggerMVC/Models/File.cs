﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace FileTaggerMVC.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        //public IEnumerable<TagViewModel> Tags { get; set; }

        //public IEnumerable<TagViewModel> AssociatedTags { get; set; }
        //public IEnumerable<TagViewModel> NonAssociatedTags { get; set; }

        public int[] TagIds { get; set; }
        public MultiSelectList Tags { get; set; }
    }
}
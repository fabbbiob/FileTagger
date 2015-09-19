using System.Collections.Generic;
using System.Web.Mvc;

namespace FileTaggerMVC.Models
{
    public class DropDownListViewModel
    {
        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
    }
}
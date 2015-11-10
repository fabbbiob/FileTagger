using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileTaggerMVC.Models
{
    public class TagViewModel : IComparable<TagViewModel>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Display(Name = "Tag Type")]
        public TagTypeViewModel TagType { get; set; }

        [NotMapped, Display(Name = "Tag Type")]
        public DropDownListViewModel TagTypeViewModel { get; set; }

        [NotMapped]
        public int TagTypeId { get; set; }

        public int CompareTo(TagViewModel other)
        {
            return Description.CompareTo(other.Description);
        }
    }
}
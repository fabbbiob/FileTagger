﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FileTaggerMVC.Models
{
    public class TagTypeViewModel : IComparable<TagTypeViewModel>
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public int CompareTo(TagTypeViewModel other)
        {
            return Description.CompareTo(other.Description);
        }
    }
}
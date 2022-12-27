﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteSharingAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

    }
}
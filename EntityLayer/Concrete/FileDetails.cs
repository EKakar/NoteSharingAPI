﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    [Table("FileDetails")]
    public class FileDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}

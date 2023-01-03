using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [MaxLength(50)]
        public string CategoryName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EF_Practise.Modals
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int? ProductId { get; set; }
        [Required]   
         public string? Name { get; set; }
         public int? Price { get; set; }
         public int? CategoryId { get; set; }
        
         [NotMapped]
         public string? CategoryName { get; set; }

    }
}

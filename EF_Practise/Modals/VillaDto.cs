using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Practise.Modals
{
    public class VillaDto
    {
        [Required]
        [MaxLength(30)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Ocupancy { get; set; }
        public int? Sqft { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EF_Practise.Modals
{
    public class Villa
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Ocupancy { get; set; }
        public int Sqft { get; set; }
    }
}

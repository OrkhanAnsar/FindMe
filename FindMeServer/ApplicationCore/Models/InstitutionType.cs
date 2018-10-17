using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class InstitutionType
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Type { get; set; }
    }
}
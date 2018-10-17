using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Models
{
    [Table("Losts")]
    public class Lost
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(200), Required]
        public string Clothes { get; set; }
        [Required]
        public string BodyType { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        [Required, MaxLength(300)]
        public string Signs { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public int AgeBegin { get; set; }
        [Required]
        public int AgeEnd { get; set; }
        [Required]
        public bool IsFound { get; set; }
        [Required]
        public int Height { get; set; }
        [MaxLength(228)]
        public string ImagePath { get; set; }
        [MaxLength(300)]
        public string Comment { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(300)]
        public string DetectionDescription { get; set; }
        [Required]
        public DateTime DetectionTime { get; set; }
        [Required]
        public string Gender { get; set; }
        public int InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
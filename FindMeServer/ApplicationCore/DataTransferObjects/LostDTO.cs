using System;

namespace ApplicationCore.DataTransferObjects
{
    public class LostDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Clothes { get; set; }
        public string BodyType { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string Signs { get; set; }
        public string LastName { get; set; }
        public int AgeBegin { get; set; }
        public int AgeEnd { get; set; }
        public bool IsFound { get; set; }
        public int Height { get; set; }
        public string ImagePath { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public string DetectionDescription { get; set; }
        public DateTime DetectionTime { get; set; }
        public string Gender { get; set; }
        public int InstitutionId { get; set; }
        public InstitutionDTO Institution { get; set; }
    }
}
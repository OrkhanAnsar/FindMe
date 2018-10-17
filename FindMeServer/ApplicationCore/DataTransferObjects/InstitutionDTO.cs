namespace ApplicationCore.DataTransferObjects
{
    public class InstitutionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Login { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Phone { get; set; }
        public string OpeningHours { get; set; }
        public string Website { get; set; }
        public int CityId { get; set; }
        public int InstitutionTypeId { get; set; }
        public bool IsAdmin { get; set; }
        public InstitutionTypeDTO InstitutionType { get; set; }
        public CityDTO City { get; set; }
    }
}
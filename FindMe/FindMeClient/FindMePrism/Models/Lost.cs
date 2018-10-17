using Newtonsoft.Json;
using Prism.Mvvm;
using System;

namespace FindMePrism.Models
{
    public class Lost : BindableBase, ICloneable
    {
        private int id;
        private string firstName;
        private string middleName;
        private string lastName;
        private string eyeColor;
        private string hairColor;
        private string clothes;
        private string bodyType;
        private string signs;
        private uint? ageBegin;
        private uint? ageEnd;
        private uint? height;
        private string imagePath;
        private string comment;
        private string description;
        private string detectionDescription;
        private DateTime detectionTime;
        private string gender;
        private Institution institution;
        private int institutionId;
        private bool isFound;

        public int Id { get => id; set { id = value; base.RaisePropertyChanged(); } }
        public string FirstName { get => firstName; set{ firstName = value; base.RaisePropertyChanged(); } }
        public string MiddleName { get => middleName; set { middleName = value; base.RaisePropertyChanged(); } }
        public string Clothes { get => clothes; set { clothes = value; base.RaisePropertyChanged(); } }
        public string BodyType { get => bodyType; set { bodyType = value; base.RaisePropertyChanged(); } }
        public string EyeColor { get => eyeColor; set { eyeColor = value; base.RaisePropertyChanged(); } }
        public string HairColor { get => hairColor; set { hairColor = value; base.RaisePropertyChanged(); } }
        public string Signs { get => signs; set { signs = value; base.RaisePropertyChanged(); } }
        public string LastName { get => lastName; set { lastName = value; base.RaisePropertyChanged(); } }
        public uint? AgeBegin { get => ageBegin; set { ageBegin = value; base.RaisePropertyChanged(); } }
        public uint? AgeEnd { get => ageEnd; set { ageEnd = value; base.RaisePropertyChanged(); } }
        public uint? Height { get => height; set { height = value; base.RaisePropertyChanged(); } }
        public string ImagePath { get => imagePath; set { imagePath = value; base.RaisePropertyChanged(); } }
        public string Comment { get => comment; set { comment = value; base.RaisePropertyChanged(); } }
        public string Description { get => description; set { description = value; base.RaisePropertyChanged(); } }
        public string DetectionDescription { get => detectionDescription; set { detectionDescription = value; base.RaisePropertyChanged(); } }
        public DateTime DetectionTime { get => detectionTime; set { detectionTime = value; base.RaisePropertyChanged(); } }
        public string Gender { get => gender; set { gender = value; base.RaisePropertyChanged(); } }
        public Institution Institution { get => institution; set { institution = value; base.RaisePropertyChanged(); } }
        public int InstitutionId { get => institutionId; set { institutionId = value; base.RaisePropertyChanged(); } }     
        public bool IsFound { get => isFound; set { isFound = value; base.RaisePropertyChanged(); } }

        [JsonIgnore]
        public string FullName => string.Join(" ", LastName, FirstName, MiddleName);
        //public string Age => string.Join(" - ", AgeBegin, AgeEnd);
        [JsonIgnore]
        public string Found
        {
            get
            {
                if (IsFound)
                    return "Yes";
                else
                    return "No";
            }
        }
        [JsonIgnore]
        public string Age
        {
            get
            {
                if (AgeBegin == AgeEnd)
                    return AgeBegin.ToString();
                else
                    return string.Join(" - ", AgeBegin, AgeEnd);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

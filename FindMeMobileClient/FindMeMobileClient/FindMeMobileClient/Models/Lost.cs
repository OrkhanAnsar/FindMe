using System;
using Xamarin.Forms;
using Prism.Mvvm;

namespace FindMeMobileClient.Models {

    public class Lost : BindableObject {
        public Lost(bool isVisible, int id, string firstName, string middleName, string lastName, string fullName, string eyeColor, string hairColor, string clothes, string bodyType, string signs, int ageBegin, int ageEnd, int height, string imagePath, string comment, string description, string detectionDescription, DateTime detectionTime, string gender, int institutionId, Institution institution) {
            this.IsVisible = isVisible;
            this.Id = id;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.FullName = $"{firstName} {middleName} {lastName}";
            this.EyeColor = eyeColor;
            this.HairColor = hairColor;
            this.Clothes = clothes;
            this.BodyType = bodyType;
            this.Signs = signs;
            this.Age = ageBegin == ageEnd ? ageBegin.ToString() : $"{ageBegin} - {ageEnd}";
            this.AgeBegin = ageBegin;
            this.AgeEnd = ageEnd;
            this.Height = height;
            this.ImagePath = imagePath;
            this.Comment = comment;
            this.Description = description;
            this.DetectionDescription = detectionDescription;
            this.DetectionTime = detectionTime;
            this.Gender = gender;
            this.InstitutionId = institutionId;
            this.Institution = institution;
        }

        private int id;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _fullName;
        private string _eyeColor;
        private string _hairColor;
        private string _clothes;
        private string _bodyType;
        private string _signs;
        private string _age;
        private int _height;
        private string _imagePath;
        private string _comment;
        private string _description;
        private string _detectionDescription;
        private DateTime _detectionTime;
        private string _gender;
        private int _institutionId;
        private Institution _institution;
        private bool isVisible;
        private int ageBegin;
        private int ageEnd;

        public int AgeEnd {
            get { return ageEnd; }
            set { ageEnd = value; base.OnPropertyChanged(); }
        }

        public int AgeBegin {
            get { return ageBegin; }
            set { ageBegin = value; base.OnPropertyChanged(); }
        }


        public bool IsVisible {
            get { return isVisible; }
            set { isVisible = value; base.OnPropertyChanged(); }
        }

        public int Id {
            get { return id; }
            set {
                id = value;
                base.OnPropertyChanged();
            }
        }



        public string FirstName { get => _firstName; set { _firstName = value; base.OnPropertyChanged(); } }
        public string MiddleName { get => _middleName; set { _middleName = value; base.OnPropertyChanged(); } }
        public string LastName { get => _lastName; set { _lastName = value; base.OnPropertyChanged(); } }
        public string FullName { get => _fullName; set { _fullName = value; base.OnPropertyChanged(); } }
        public string EyeColor { get => _eyeColor; set { _eyeColor = value; base.OnPropertyChanged(); } }
        public string HairColor { get => _hairColor; set { _hairColor = value; base.OnPropertyChanged(); } }
        public string Clothes { get => _clothes; set { _clothes = value; base.OnPropertyChanged(); } }
        public string BodyType { get => _bodyType; set { _bodyType = value; base.OnPropertyChanged(); } }
        public string Signs { get => _signs; set { _signs = value; base.OnPropertyChanged(); } }
        public string Age { get => _age; set { _age = value; base.OnPropertyChanged(); } }
        public int Height { get => _height; set { _height = value; base.OnPropertyChanged(); } }
        public string ImagePath { get => _imagePath; set { _imagePath = value; base.OnPropertyChanged(); } }
        public string Comment { get => _comment; set { _comment = value; base.OnPropertyChanged(); } }
        public string Description { get => _description; set { _description = value; base.OnPropertyChanged(); } }
        public string DetectionDescription { get => _detectionDescription; set { _detectionDescription = value; base.OnPropertyChanged(); } }
        public DateTime DetectionTime { get => _detectionTime; set { _detectionTime = value; base.OnPropertyChanged(); } }
        public string Gender { get => _gender; set { _gender = value; base.OnPropertyChanged(); } }
        public int InstitutionId { get => _institutionId; set { _institutionId = value; base.OnPropertyChanged(); } }
        public Institution Institution { get => _institution; set { _institution = value; base.OnPropertyChanged(); } }
    }
}
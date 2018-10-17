using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FindMeMobileClient.Models {
    public class Filter : BindableObject {
        private string firstName;
        private string middleName;
        private string lastName;
        private int ageBegin;
        private int ageEnd;
        private int height;
        private string hairColor;
        private string eyeColor;
        private string bodyType;
        private string gender;
        private DateTime filterDate;

        public DateTime FilterDate {
            get { return filterDate; }
            set { filterDate = value; base.OnPropertyChanged(); }
        }


        public string Gender {
            get { return gender; }
            set { gender = value; base.OnPropertyChanged(); }
        }


        public string BodyType {
            get { return bodyType; }
            set { bodyType = value; base.OnPropertyChanged(); }
        }


        public string EyeColor {
            get { return eyeColor; }
            set { eyeColor = value; base.OnPropertyChanged(); }
        }


        public string HairColor {
            get { return hairColor; }
            set { hairColor = value; base.OnPropertyChanged(); }
        }


        public int Height {
            get { return height; }
            set { height = value; base.OnPropertyChanged(); }
        }


        public int AgeEnd {
            get { return ageEnd; }
            set { ageEnd = value; base.OnPropertyChanged(); }
        }


        public int AgeBegin {
            get { return ageBegin; }
            set { ageBegin = value; base.OnPropertyChanged(); }
        }


        public string LastName {
            get { return lastName; }
            set { lastName = value; base.OnPropertyChanged(); }
        }


        public string FirstName {
            get { return firstName; }
            set { firstName = value; base.OnPropertyChanged(); }
        }

        public string MiddleName {
            get { return middleName; }
            set { middleName = value; base.OnPropertyChanged(); }
        }

    }
}

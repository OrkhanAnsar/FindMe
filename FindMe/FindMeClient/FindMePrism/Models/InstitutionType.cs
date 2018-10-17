using Prism.Mvvm;
using System;

namespace FindMePrism.Models
{
    public class InstitutionType : BindableBase, ICloneable
    {
        private int id;
        private string type { get; set; }

        public int Id { get => id; set { id = value; base.RaisePropertyChanged(); } }
        public string Type { get => type; set { type = value; base.RaisePropertyChanged(); } }

        public override string ToString()
        {
            return Type;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is InstitutionType))
                return false;

            return (((InstitutionType)obj).Id == this.Id);
        }
    }
}
using Prism.Mvvm;
using System;

namespace FindMePrism.Models
{
    public class City : BindableBase, ICloneable
    {
        private int id;
        private string name { get; set; }

        public int Id { get => id; set { id = value; base.RaisePropertyChanged(); } }
        public string Name { get => name; set { name = value; base.RaisePropertyChanged(); } }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
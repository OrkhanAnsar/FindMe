using Xamarin.Forms;

namespace FindMeMobileClient.Models
{
    public class City : BindableObject
    {
        public int Id { get; set; }
        private string name;

        public string Name {
            get { return name; }
            set { name = value; base.OnPropertyChanged(); }
        }

    }
}
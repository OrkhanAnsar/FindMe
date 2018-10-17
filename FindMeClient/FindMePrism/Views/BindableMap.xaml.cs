using FindMePrism.Events;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json.Linq;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using FindMePrism.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindMePrism.Views
{
    /// <summary>
    /// Interaction logic for BindableMap.xaml
    /// </summary>
    public partial class BindableMap : UserControl
    {
        public ObservableCollection<Pushpin> Pins
        {
            get { return (ObservableCollection<Pushpin>)GetValue(PinsProperty); }
            set { SetValue(PinsProperty, value); }
        }

        public IEventAggregator EventAggregator { get; }

        public static readonly DependencyProperty PinsProperty =
            DependencyProperty.Register("Pins", typeof(ObservableCollection<Pushpin>), typeof(BindableMap),
            new PropertyMetadata(null, new PropertyChangedCallback(OnPinsChanged)));

        private static void OnPinsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindableMap map = (BindableMap)d;
            map.ClearMapPoints();
            map.SubscribeToCollectionChanged();      
        }

        private void ClearMapPoints()
        {
            map.Children.Clear();
        }

        private void SubscribeToCollectionChanged()
        {
            if (Pins != null)
                Pins.CollectionChanged += Pins_CollectionChanged;
        }

        private void Pins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Remove the old pushpins
            if (e.OldItems != null)
                foreach (Pushpin pin in e.OldItems)
                    map.Children.Remove(pin);

            //Add the new pushpins
            if (e.NewItems != null)
                foreach (Pushpin pin in e.NewItems)
                    map.Children.Add(pin);
        }

        public BindableMap()
        {
            InitializeComponent();
            Pins = new ObservableCollection<Pushpin>();
            EventAggregator = App.Container.Resolve<IEventAggregator>();
            EventAggregator.GetEvent<InstToEdit>().Subscribe(SetPin);
            EventAggregator.GetEvent<ClearPinsEvent>().Subscribe(ClearPins);
        }

        private void ClearPins(bool clear)
        {
            if (clear)
                ClearMapPoints();
        }
       

        private void SetPin(Institution inst)
        {
            Pushpin pin = new Pushpin();
            pin.Location = new Location(inst.Latitude, inst.Longitude);          
            this.Pins.Clear();
            ClearMapPoints();
            this.Pins.Add(pin);
        }

        public List<string> GetSelectedAddress(Location location)
        {
            try
            {
                NumberFormatInfo nfi = new NumberFormatInfo
                {
                    NumberDecimalSeparator = "."
                };
                string latitude = location.Latitude.ToString(nfi);
                string longitude = location.Longitude.ToString(nfi);
                string query = $"http://dev.virtualearth.net/REST/v1/Locations/{latitude},{longitude}?o=json&key=eLuL2PZapgx4D2vS7Kqe~od5P4iK7p9ddyG9mmJ8nVg~ArF1qzf8-FieeGj-EVPFCiMRoLKPRu8i5p7pQWxZQIS1I2nOuavBsPLPVCl4o2TS";
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                var data = webClient.DownloadString(query);
                dynamic json = JObject.Parse(data);
                string city = "";
                string address = "";

                List<string> fullinfo = new List<string>();
                foreach (var set in json.resourceSets) {
                    foreach (var resource in set.resources) {
                        city = resource.address.adminDistrict.Value;
                        address = resource.address.addressLine;                       
                    }
                }
                fullinfo.Add(city);
                fullinfo.Add(address);
                fullinfo.Add(location.Latitude.ToString());
                fullinfo.Add(location.Longitude.ToString());
                return fullinfo;
            }
            catch (Exception)  { return null; }
        }

        private void Map_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var address = new List<string>();
                Point point = Mouse.GetPosition(map);
                Pushpin pin = new Pushpin();
                pin.Location = map.ViewportPointToLocation(point);
                address = GetSelectedAddress(pin.Location);               
                EventAggregator.GetEvent<AddressEvent>().Publish(address);
                this.Pins.Clear();
                ClearMapPoints();
                this.Pins.Add(pin);               
            }
            catch (Exception) {  }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Syntra.Data.Models;
using KlantenAppWPF.ViewModel;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows;
using KlantenAppWPF.Tools;
using DevExpress.Office;
using System.DirectoryServices;


namespace KlantenAppWPF.ViewModel
{
    class ViewMod : INotifyPropertyChanged
    {
        #region Fields 
        public event PropertyChangedEventHandler PropertyChanged;
        KlantenLijst _klantenLijst = new KlantenLijst();
        BookingLijst _bookingLijst = new BookingLijst();
        List<Klant> _klantenMetBoeking = new List<Klant>();
        List<int> _beschikbareTafels = new List<int>();
        List<int> _geboekteTafels = new List<int>();
        public ObservableCollection<Klant> _klantenColl = null;
        public ObservableCollection<Booking> _bookingColl = null;
        public ObservableCollection<Klant> _klantenMetBookingColl = null;
        public ObservableCollection<int> _beschikbareTafelsColl = null;
        public ObservableCollection<int> _geboekteTafelsColl = null;
        public Klant _currentKlant = null;
        public Booking _currentBooking = null;
        bool _isTafelBeschikbaar;
        bool _bestaatBechikbareTafel;
        public string LastError { get; protected set; } = "";

        #endregion Fields
        #region Properties
        public KlantenLijst KlantenLijst { get { _klantenLijst ??= new KlantenLijst(); return _klantenLijst; } set => _klantenLijst = value; }
        public BookingLijst BookingLijst { get { _bookingLijst ??= new BookingLijst(); return _bookingLijst; } set => _bookingLijst = value; }
        public List<Klant> KlantenMetBookingLijst { get; set; }

        public List<int> BeschikbareTafels { get; set; }
        public List<int> GeboekteTafels { get; set; }
        public Klant CurrentKlant { get =>_currentKlant; set => _currentKlant = value; } 
        public Booking CurrentBooking { get => _currentBooking; set => _currentBooking = value; }
    
   

    

    public ObservableCollection<Klant> KlantenColl
        { get
            {
                _klantenColl = new ObservableCollection<Klant>(KlantenLijst.Members);
                return _klantenColl;
            }
            set
            {
                _klantenColl = value;
            }
        }

        public ObservableCollection<Booking> BookingColl
        {
            get
            {
                _bookingColl = new ObservableCollection<Booking>(BookingLijst.Members);
                return _bookingColl;
            }
            set
            {
                _bookingColl = value;
            }
        }

        public ObservableCollection<Klant> KlantenMetBookingColl

        {
            get
            {
                _klantenMetBookingColl = new ObservableCollection<Klant>(KlantenMetBookingLijst);
                return _klantenMetBookingColl;
            }
            set
            {
                _klantenMetBookingColl = value;
            }
        }
        public ObservableCollection<int> BeschikbareTafelsColl
        {
            get
            {
                _beschikbareTafelsColl = new ObservableCollection<int>(BeschikbareTafels);
                return _beschikbareTafelsColl;
            }
            set
            {
                _beschikbareTafelsColl = value;
            }
        }
        public ObservableCollection<int> GeboekteTafelsColl
        {
            get
            {
                _geboekteTafelsColl = new ObservableCollection<int>(GeboekteTafels);
                return _geboekteTafelsColl;
            }
            set
            {
                _geboekteTafelsColl = value;
            }
        }

        #endregion Properties
        #region Methods
        internal void VulData()
        {


        }

        protected void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
       /* public bool Import(string json)
        {

        }
        public string Export()
        {

        }
        public bool ImportFromFile(string path)
        {
            if (File.Exists(path))
            {
                return Import(File.ReadAllText(path)) == true;
            }
            return false;
        }
        public bool ExportToFile(string path)
        {
            try
            {
                File.WriteAllText(path, Export());
                return File.Exists(path);
            }
            catch { }
            return false;
        }

        */
        #endregion Methods

    }
}
 


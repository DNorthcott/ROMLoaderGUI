using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ROMLoader.Annotations;
using ROMLoader.Models;
using ROMLoader.Src.Helpers;
using SQLite;

namespace ROMLoader.ViewModels
{
    /// <summary>
    /// Links the model to the view.  Variables in this class are bound to the
    /// GUI.  Provides the logic for events.
    /// </summary>
    class MainViewModel : INotifyPropertyChanged
    {
        // Variables not required for view.
        private readonly SQLiteAsyncConnection database;
        private List<Blend> listOfBlends;
        private List<RunOfMine> listOfROMS;
        private Models.ROMLoader loader;

        private RunOfMine primaryROM;

        // Variables for displaying data.
        private Blend primaryBlend;
        private ObservableCollection<string> blendCycle;
        private List<Stockpile> stockpiles;
        
        private int loadTime;
        private int maxWaitTime;

        /// <summary>
        /// Creates a new ROM.
        /// </summary>
        public  MainViewModel()
        {
            database = new SQLiteAsyncConnection("CoalMineDB.db");
            GetBlend();
            GetStockpiles();


            IncreaseLoadTimeCommand = new RelayCommand(IncreaseLoadTime);
        }

        public RelayCommand IncreaseLoadTimeCommand
        { get;
          set;  
        }

        void IncreaseLoadTime(object parameter)
        {
            LoadTime = LoadTime + 1;
        }

        public List<Stockpile> Stockpiles
        {
            get { return stockpiles; }
            set
            {
                stockpiles = value;
                OnPropertyChanged("Stockpiles");
            }
        }

        public ObservableCollection<string> BlendCycle
        {
            get { return blendCycle; }
            set
            {
                blendCycle = value;
                OnPropertyChanged("BlendCycle");
            }
        }

        public string LoadTime
        {
            get { return loadTime.ToString(); }
            set
            {
                loadTime = Int32.Parse(value);
                OnPropertyChanged("LoadTime");
            }
        }

        public string MaxWaitTime
        {
            get { return maxWaitTime.ToString(); }
            set
            {
                maxWaitTime = Int32.Parse(value);
                OnPropertyChanged("MaxWaitTime");
            }
        }

        public Blend PrimaryBlend
        {
            get { return primaryBlend; }
            set
            {
                primaryBlend = value;
            }
        }

        private async Task GetStockpiles()
        {
            listOfROMS = await DatabaseQueries.GetRunOfMineAsync(DateTime.Now, database);
            primaryROM = listOfROMS[0];
            Stockpiles = primaryROM.Stockpiles;
        }

        /// <summary>
        /// Calls blend data from the sqlite database. 
        /// Sets 
        /// </summary>
        /// <returns></returns>
        private async Task GetBlend()
        {
            listOfBlends = await DatabaseQueries.GetBlendsAsync(DateTime.Today, database);

            // Create list for blend cycle view.
            BlendCycle = new ObservableCollection<string>(listOfBlends[0].Cycle);
        }

        /// <summary>
        /// TODO: Research this more. I don't know how this works and its from resharper.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

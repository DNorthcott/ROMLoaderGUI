using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Documents;
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
        private Blend blend;
        private List<RunOfMine> listOfROMS;
        private Loader loader;

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
            DecreaseLoadTimeCommand = new RelayCommand(DecreaseLoadTime);

            IncreaseMaxWaitTimeCommand = new RelayCommand(IncreaseMaxWaitTime);
            DecreaseMaxWaitTimeCommand = new RelayCommand(DecreaseMaxWaitTime);
        }

        public RelayCommand IncreaseMaxWaitTimeCommand
        {
            get;
            set;
        }

        public RelayCommand DecreaseMaxWaitTimeCommand
        {
            get;
            set;
        }

        public RelayCommand IncreaseLoadTimeCommand
        { get;
          set;  
        }

        public RelayCommand DecreaseLoadTimeCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Checks if the ROM loader exists.  If not and a blend exists, 
        /// it will be created.
        /// </summary>
        /// <returns>Returns true if exists or can be created.</returns>
        private bool RomLoaderExists()
        {
            if (loader != null)
            {
                return true;
            }
           else if (blendCycle == null)
            {
                return false;
            }
            else
            {
                TimeSpan loadTime = new TimeSpan(0, 0, 0, 0);
                TimeSpan maxWaitTime = new TimeSpan(0, 0, 0, 0);

                loader = new Loader(blend.Cycle, loadTime, maxWaitTime);
                return true;
            }
        }


        private void IncreaseMaxWaitTime(object parameter)
        {
            if (RomLoaderExists())
            {
                ChangeWaitTime(true);
            }
        }

        private void DecreaseMaxWaitTime(object parameter)
        {

            if (RomLoaderExists())
            {
                if (loader.MaxWaitTime.Minutes == 0)
                {
                    //TODO: Maybe throw a window saying cannot make negative load time. 
                    // Further probably should throw exception.
                    return;
                }
                ChangeWaitTime(false);
            }
        }

        private void IncreaseLoadTime(object parameter)
        {
            if (RomLoaderExists())
            {
                ChangeLoadTime(true);
            }
        }

        private void DecreaseLoadTime(object parameter)
        {

            if (RomLoaderExists()) 
            {
                if (loader.LoadTime.Minutes == 0)
                {
                    //TODO: Maybe throw a window saying cannot make negative load time. 
                    // Further probably should throw exception.
                    return;
                }
                ChangeLoadTime(false);
            }
        }

        private void ChangeWaitTime(bool increase)
        {
            TimeSpan waitTime = loader.MaxWaitTime;
            TimeSpan oneMinute = new TimeSpan(0, 0, 1, 0);

            if (increase)
            {

                loader.MaxWaitTime = waitTime.Add(oneMinute);

            }
            else
            {
                loader.MaxWaitTime = waitTime.Subtract(oneMinute);
            }

            MaxWaitTime = loader.MaxWaitTime.Minutes.ToString();
        }

        private void ChangeLoadTime(bool increase)
        {
            TimeSpan loadTime = loader.LoadTime;
            TimeSpan oneMinute = new TimeSpan(0, 0, 1, 0);

            if (increase)
            {
                
                loader.LoadTime = loadTime.Add(oneMinute);

            }
            else
            {
                loader.LoadTime = loadTime.Subtract(oneMinute);
            }

            LoadTime = loader.LoadTime.Minutes.ToString();
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
            List<Blend> listOfBlends = await DatabaseQueries.GetBlendsAsync(DateTime.Today, database);

            // Remove the blend from the list.
            blend = listOfBlends[0];

            // Create list for blend cycle view.
            BlendCycle = new ObservableCollection<string>(blend.Cycle);
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

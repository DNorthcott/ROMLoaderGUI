using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using ROMLoader.Annotations;
using ROMLoader.Helpers;
using ROMLoader.Models;
using SQLite;


namespace ROMLoader.ViewModels
{
    /// <summary>
    ///     Links the model to the view.  Variables in this class are bound to the
    ///     GUI.  Provides the logic for events.
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
        private ObservableCollection<string> blendCycle;
        private ICollectionView stockpiles;
        private ICollectionView coalMovements;
        private string loadingCoal;
        private int coalIndex;
        private int loadTime;
        private int maxWaitTime;

        /// <summary>
        ///     Create new mainviewmodel.
        /// </summary>
        public MainViewModel()
        {
            database = new SQLiteAsyncConnection("CoalMineDB.db");
            GetBlend();
            GetStockpiles();

            LoadingCoal = "";

            IncreaseLoadTimeCommand = new RelayCommand(IncreaseLoadTime);
            DecreaseLoadTimeCommand = new RelayCommand(DecreaseLoadTime);

            IncreaseMaxWaitTimeCommand = new RelayCommand(IncreaseMaxWaitTime);
            DecreaseMaxWaitTimeCommand = new RelayCommand(DecreaseMaxWaitTime);

            LoadCoalCommand = new RelayCommand(LoadCoal);
            CoalIndex = 0;

        }

        /// <summary>
        /// Get list of stockpiles out of db.
        /// </summary>
        /// <returns></returns>
        private async Task GetStockpiles()
        {
            listOfROMS = await DatabaseQueries.GetRunOfMineAsync(DateTime.Now, database);
            primaryROM = listOfROMS[0];
            Stockpiles = CollectionViewSource.GetDefaultView(primaryROM.Stockpiles);
        }

        /// <summary>
        /// Gets the blend cycle from the db.
        /// </summary>
        private async Task GetBlend()
        {
            List<Blend> listOfBlends = await DatabaseQueries.GetBlendsAsync(DateTime.Today, database);

            // Remove the blend from the list.
            blend = listOfBlends[0];

            // Create list for blend cycle view.
            BlendCycle = new ObservableCollection<string>(blend.Cycle);
        }



        /* ===============================
         *  Buttons
         * ===============================
         */

            /// <summary>
            /// Load coal, executes loader to check for incoming coals and 
            /// determines what the loader will load.
            /// </summary>
            /// <param name="parameter"></param>
        private async void LoadCoal(object parameter)
        {
            if (RomLoaderExists())
            {
                List<CoalMovement> movements = await DatabaseQueries.GetCoalMovements(DateTime.Now, 30, database);

                movements = loader.AllocateCoalMovements(DateTime.Now,
                    movements);

                CoalMovements = CollectionViewSource.GetDefaultView(movements);

                LoadingCoal = loader.LoadROMTruck(DateTime.Now).Coal;

                CoalIndex = loader.CycleIndex();

                //Update allocated movements to be loaded into bin in database.

                foreach (CoalMovement m in movements)
                {
                    await DatabaseQueries.UpdateCoalMovements(m, database);
                }
            }
        }

        /// <summary>
        /// Increases the maxwaittime by 1 minute.
        /// </summary>
        /// <param name="parameter"></param>
        private void IncreaseMaxWaitTime(object parameter)
        {
            if (RomLoaderExists())
            {
                ChangeWaitTime(true);
            }
        }

        /// <summary>
        /// Decreases the maxwaittime by 1 minute.
        /// </summary>
        /// <param name="parameter"></param>
        private void DecreaseMaxWaitTime(object parameter)
        {

            if (RomLoaderExists())
            {
                if (loader.MaxWaitTime.Minutes == 0)
                {
                    return;
                }
                ChangeWaitTime(false);
            }
        }

        /// <summary>
        /// Increases the load time by 1 minute.
        /// </summary>
        /// <param name="parameter"></param>
        private void IncreaseLoadTime(object parameter)
        {
            if (RomLoaderExists())
            {
                ChangeLoadTime(true);
            }
        }

        /// <summary>
        /// Decreases the load time by 1 minute.
        /// </summary>
        /// <param name="parameter"></param>
        private void DecreaseLoadTime(object parameter)
        {

            if (RomLoaderExists())
            {
                if (loader.LoadTime.Minutes == 0)
                {
                    return;
                }
                ChangeLoadTime(false);
            }
        }

        /* ===============================
         *  Helper methods
         * ===============================
         */

        /// <summary>
        ///     Checks if the ROM loader exists.  If not and a blend exists,
        ///     it will be created.
        /// </summary>
        /// <returns>Returns true if exists or can be created.</returns>
        private bool RomLoaderExists()
        {
            if (loader != null)
            {
                return true;
            }
            if (blendCycle == null)
            {
                return false;
            }
            TimeSpan loadTime = new TimeSpan(0, 0, 0, 0);
            TimeSpan maxWaitTime = new TimeSpan(0, 0, 0, 0);

            loader = new Loader(blend, loadTime, maxWaitTime);
            return true;
        }

        /// <summary>
        /// Increases or decreases maximum wait time by 1 minute.
        /// </summary>
        /// <param name="increase">True increase, false decrease.</param>
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

        /// <summary>
        /// Increases or decreases load time time by 1 minute.
        /// </summary>
        /// <param name="increase">True increase, false decrease.</param>
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


        /* ===============================
         *  Properties
         *  Used for displaying information for GUI.
         * ===============================
         */

        public ICollectionView CoalMovements
        {
            get => coalMovements;
            set
            {
                coalMovements = value;
                OnPropertyChanged("CoalMovements");
            }
        }

        public ICollectionView Stockpiles
        {
            get => stockpiles;
            set
            {
                stockpiles = value;
                OnPropertyChanged("Stockpiles");
            }
        }

        public ObservableCollection<string> BlendCycle
        {
            get => blendCycle;
            set
            {
                blendCycle = value;
                OnPropertyChanged("BlendCycle");
            }
        }

        public string LoadTime
        {
            get => loadTime.ToString();
            set
            {
                loadTime = int.Parse(value);
                OnPropertyChanged("LoadTime");
            }
        }

        public string LoadingCoal
        {
            get => loadingCoal;
            set
            {
                loadingCoal = value;
                OnPropertyChanged("LoadingCoal");
            }
        }

        public int CoalIndex
        {
            get => coalIndex;
            set
            {
                coalIndex = value;
                OnPropertyChanged("CoalIndex");
            }
        }

        public string MaxWaitTime
        {
            get => maxWaitTime.ToString();
            set
            {
                maxWaitTime = int.Parse(value);
                OnPropertyChanged("MaxWaitTime");

            }
        }

        public Blend PrimaryBlend { get; set; }


        /* ===============================
         *  Relay commands
         * ===============================
         */


        public RelayCommand IncreaseMaxWaitTimeCommand { get; set; }

        public RelayCommand DecreaseMaxWaitTimeCommand { get; set; }

        public RelayCommand IncreaseLoadTimeCommand { get; set; }

        public RelayCommand DecreaseLoadTimeCommand { get; set; }

        public RelayCommand LoadCoalCommand { get; set; }


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

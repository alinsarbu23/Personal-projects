using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessLogicLayer;
using Supermarket.Models.BussinessLogicLayer;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.ViewModels
{
    internal class BonuriVM : BasePropertyChanged
    {
        private readonly BonuriDAL bonuriDAL;
        private readonly BonuriBLL bonuriBLL;

        public BonuriVM()
        {
            bonuriDAL = new BonuriDAL();
            bonuriBLL = new BonuriBLL();
            BonuriList = new ObservableCollection<Bonuri>(bonuriDAL.GetAllBonuri());
        }

        #region Data Members

        private ObservableCollection<Bonuri> bonuriList;
        public ObservableCollection<Bonuri> BonuriList
        {
            get { return bonuriList; }
            set
            {
                bonuriList = value;
                OnPropertyChanged(nameof(BonuriList));
            }
        }

        private ObservableCollection<SumaIncasataPeZi> sumaIncasataPeZiList;
        public ObservableCollection<SumaIncasataPeZi> SumaIncasataPeZiList
        {
            get { return sumaIncasataPeZiList; }
            set
            {
                sumaIncasataPeZiList = value;
                OnPropertyChanged(nameof(SumaIncasataPeZiList));
            }
        }

        private DateTime selectedMonth = DateTime.Now;
        public DateTime SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                selectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
            }
        }

        #endregion

        #region Command Members
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<Bonuri>(b =>
                    {
                        bonuriDAL.AddBon(b);
                        // Reîncărcăm lista pentru a reflecta modificările
                        BonuriList = new ObservableCollection<Bonuri>(bonuriDAL.GetAllBonuri());
                    });
                }
                return addCommand;
            }
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand<Bonuri>(b =>
                    {
                        bonuriDAL.UpdateBon(b);
                        BonuriList = new ObservableCollection<Bonuri>(bonuriDAL.GetAllBonuri());
                    });
                }
                return updateCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand<int>(bonId =>
                    {
                        bonuriDAL.DeleteBon(bonId);
                        BonuriList = new ObservableCollection<Bonuri>(bonuriDAL.GetAllBonuri());
                    });
                }
                return deleteCommand;
            }
        }

        private ICommand getSumaIncasataPeZiCommand;
        public ICommand GetSumaIncasataPeZiCommand
        {
            get
            {
                if (getSumaIncasataPeZiCommand == null)
                {
                    getSumaIncasataPeZiCommand = new RelayCommand<int>(utilizatorId =>
                    {
                        // Extragem luna și anul selectat din data picker
                        int luna = SelectedMonth.Month;
                        int an = SelectedMonth.Year;

                        SumaIncasataPeZiList = new ObservableCollection<SumaIncasataPeZi>(bonuriDAL.GetSumeIncasatePeZi(utilizatorId, new DateTime(an, luna, 1)));
                    });
                }
                return getSumaIncasataPeZiCommand;
            }
        }

        #endregion
    }
}

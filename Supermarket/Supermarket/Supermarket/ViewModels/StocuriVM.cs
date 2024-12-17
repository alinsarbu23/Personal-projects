using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessLogicLayer;
using Supermarket.Models.BussinessLogicLayer;

namespace Supermarket.ViewModels
{
    internal class StocuriVM : BasePropertyChanged
    {
        private readonly StocuriBLL stocuriBLL;

        public StocuriVM()
        {
            stocuriBLL = new StocuriBLL();
            StocuriList = new ObservableCollection<Stocuri>(stocuriBLL.GetAllStocuri());
        }

        #region Data Members

        private ObservableCollection<Stocuri> stocuriList;
        public ObservableCollection<Stocuri> StocuriList
        {
            get { return stocuriList; }
            set
            {
                stocuriList = value;
                OnPropertyChanged(nameof(StocuriList));
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
                    addCommand = new RelayCommand<Stocuri>(s =>
                    {
                        stocuriBLL.AddStoc(s);
                        // Poți adăuga și alte acțiuni aici, dacă este necesar
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
                    updateCommand = new RelayCommand<Stocuri>(stocuriBLL.UpdateStoc);
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
                    deleteCommand = new RelayCommand<int>(stocId =>
                    {
                        stocuriBLL.DeleteStoc(stocId);
                    });
                }
                return deleteCommand;
            }
        }


        #endregion
    }
}

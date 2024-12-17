using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessLogicLayer;
using Supermarket.Models.BussinessLogicLayer;

namespace Supermarket.ViewModels
{
    internal class ProducatoriVM : BasePropertyChanged
    {
        private readonly ProducatoriBLL producatoriBLL;

        public ProducatoriVM()
        {
            producatoriBLL = new ProducatoriBLL();
            ProducatoriList = new ObservableCollection<Producatori>(producatoriBLL.GetAllProducatori());
        }

        #region Data Members

        private ObservableCollection<Producatori> producatoriList;
        public ObservableCollection<Producatori> ProducatoriList
        {
            get { return producatoriList; }
            set
            {
                producatoriList = value;
                OnPropertyChanged(nameof(ProducatoriList));
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
                    addCommand = new RelayCommand<Producatori>(p =>
                    {
                        producatoriBLL.AddProducator(p);
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
                    updateCommand = new RelayCommand<Producatori>(producatoriBLL.UpdateProducator);
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
                    deleteCommand = new RelayCommand<int>(producatorId =>
                    {
                        producatoriBLL.DeleteProducator(producatorId);
                    });
                }
                return deleteCommand;
            }
        }


        #endregion
    }
}


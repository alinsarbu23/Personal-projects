using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BusinessLogicLayer;
using Supermarket.Models;
using Supermarket.Models.BussinessLogicLayer;

namespace Supermarket.ViewModels
{
    internal class ProduseVM : BasePropertyChanged
    {
        private readonly ProduseBLL produseBLL;

        public ProduseVM()
        {
            produseBLL = new ProduseBLL();
            ProduseList = new ObservableCollection<Produse>(produseBLL.GetAllProduse());
        }

        #region Data Members

        private ObservableCollection<Produse> produseList;
        public ObservableCollection<Produse> ProduseList
        {
            get { return produseList; }
            set
            {
                produseList = value;
                OnPropertyChanged(nameof(ProduseList));
            }
        }

        private ObservableCollection<ProdusDetaliat> produseDetaliateList;
        public ObservableCollection<ProdusDetaliat> ProduseDetaliateList
        {
            get { return produseDetaliateList; }
            set
            {
                produseDetaliateList = value;
                OnPropertyChanged(nameof(ProduseDetaliateList));
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
                    addCommand = new RelayCommand<Produse>(p =>
                    {
                        produseBLL.AddProdus(p);
                        ProduseList.Add(p); // Update the list after adding
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
                    updateCommand = new RelayCommand<Produse>(p =>
                    {
                        produseBLL.UpdateProdus(p);
                        // Optionally update the list if needed
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
                    deleteCommand = new RelayCommand<int>(productId =>
                    {
                        produseBLL.DeleteProdus(productId);
                        var produsToRemove = ProduseList.FirstOrDefault(p => p.ProductId == productId);
                        if (produsToRemove != null)
                        {
                            ProduseList.Remove(produsToRemove);
                        }
                    });
                }
                return deleteCommand;
            }
        }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand<string>(searchTerm =>
                    {
                        ProduseDetaliateList = new ObservableCollection<ProdusDetaliat>(produseBLL.SearchProducts(searchTerm));
                    });
                }
                return searchCommand;
            }
        }

        #endregion

        #region Show Methods

        public void Show()
        {
            ProduseList = new ObservableCollection<Produse>(produseBLL.GetAllProduse());
        }

        #endregion
    }
}

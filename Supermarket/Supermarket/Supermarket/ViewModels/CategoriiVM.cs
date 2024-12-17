// CategoriiVM.cs
using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessLogicLayer;
using Supermarket.Models.BussinessLogicLayer;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.ViewModels
{
    internal class CategoriiVM : BasePropertyChanged
    {
        private readonly CategoriiBLL categoriiBLL;

        public CategoriiVM()
        {
            categoriiBLL = new CategoriiBLL();
            CategoriiList = new ObservableCollection<Categorii>(categoriiBLL.GetCategoriiProduse());
        }

        #region Data Members

        private ObservableCollection<Categorii> categoriiList;
        public ObservableCollection<Categorii> CategoriiList
        {
            get { return categoriiList; }
            set
            {
                categoriiList = value;
                OnPropertyChanged(nameof(CategoriiList));
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
                    addCommand = new RelayCommand<Categorii>(c =>
                    {
                        categoriiBLL.AddCategorieProdus(c);
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
                    updateCommand = new RelayCommand<Categorii>(categoriiBLL.UpdateCategorieProdus);
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
                    deleteCommand = new RelayCommand<int>(categorieId =>
                    {
                        categoriiBLL.DeleteCategorieProdus(categorieId);
                    });
                }
                return deleteCommand;
            }
        }


        #endregion
    }

}

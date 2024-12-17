using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessLogicLayer;
using Supermarket.Models.BussinessLogicLayer;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.ViewModels
{
    internal class UtilizatoriVM : BasePropertyChanged
    {
        private readonly UtilizatoriBLL utilizatoriBLL;

        public UtilizatoriVM()
        {
            utilizatoriBLL = new UtilizatoriBLL();
            UtilizatoriList = new ObservableCollection<Utilizatori>(utilizatoriBLL.GetAllUtilizatori());
        }

        #region Data Members

        private ObservableCollection<Utilizatori> utilizatoriList;
        public ObservableCollection<Utilizatori> UtilizatoriList
        {
            get { return utilizatoriList; }
            set
            {
                utilizatoriList = value;
                OnPropertyChanged(nameof(UtilizatoriList));
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
                    addCommand = new RelayCommand<Utilizatori>(u =>
                    {
                        utilizatoriBLL.AddUtilizator(u);
                        UtilizatoriList.Add(u); // Actualizează lista după adăugare
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
                    updateCommand = new RelayCommand<Utilizatori>(utilizatoriBLL.UpdateUtilizator);
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
                    deleteCommand = new RelayCommand<int>(userId =>
                    {
                        utilizatoriBLL.DeleteUtilizator(userId);
                    });
                }
                return deleteCommand;
            }
        }


        #endregion
    }
}

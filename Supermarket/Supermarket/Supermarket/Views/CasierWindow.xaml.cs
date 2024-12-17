using System;
using System.Windows;
using Supermarket.ViewModels;
using System.Windows.Controls;
using System.Linq;
using System.Globalization;
using Supermarket.Models.BussinessLogicLayer;
using System.Collections.ObjectModel;
using Supermarket.Models;

namespace Supermarket.Views
{
    public partial class CasierWindow : Window
    {
        private ProduseVM produseVM;

        public CasierWindow()
        {
            InitializeComponent();
            produseVM = new ProduseVM();
            DataContext = produseVM;

        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                // Dacă caseta de text este goală, afișăm toate produsele
                DataGrid.ItemsSource = produseVM.ProduseList;
            }
            else
            {
                string searchTerm = searchTextBox.Text;
                produseVM.SearchCommand.Execute(searchTerm);

                // Actualizăm sursa de date a DataGrid-ului cu lista de produse găsite
                DataGrid.ItemsSource = produseVM.ProduseDetaliateList;

                // Aplicăm formatul de afișare pentru coloana "DataEliberare" (doar data, fără oră)
                var dataEliberareColumn = DataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "DataEliberare") as DataGridTextColumn;
                if (dataEliberareColumn != null)
                {
                    dataEliberareColumn.Binding.StringFormat = "dd.MM.yyyy";
                }
            }
        }





        private void EmiterebBonButton_Click(object sender, RoutedEventArgs e)
        {

        }



    }
}


using Supermarket.Models.BussinessLogicLayer;
using Supermarket.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using Supermarket.Models.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Supermarket.Models;
using System.Collections.ObjectModel;

namespace Supermarket.Views
{

    public partial class AdministratorWindow : Window
    {
        private ProduseVM produseVM;
        private StocuriVM stocuriVM;
        private UtilizatoriVM utilizatoriVM;
        private ProducatoriVM producatoriVM;
        private BonuriVM bonuriVM;
        private CategoriiVM categoriiVM;
        private ProductByCategoryVM productByCategoryVM;


        public AdministratorWindow()
        {
            InitializeComponent();
            TablesComboBox.SelectionChanged += TablesComboBox_SelectionChanged;

            produseVM = new ProduseVM();
            stocuriVM = new StocuriVM();
            utilizatoriVM = new UtilizatoriVM();
            producatoriVM = new ProducatoriVM();
            bonuriVM = new BonuriVM();
            categoriiVM = new CategoriiVM();
            productByCategoryVM = new ProductByCategoryVM();
        }

        private void LoadDataForSelectedTable()
        {
            SelectedTableDataGrid.ItemsSource = null;

            ComboBoxItem selectedItem = (ComboBoxItem)TablesComboBox.SelectedItem;
            if (selectedItem == null) return;


            object viewModel = null;
            switch (selectedItem.Content.ToString())
            {
                case "Produse":
                    viewModel = produseVM;
                    break;
                case "Stocuri":
                    viewModel = stocuriVM;
                    break;
                case "Utilizatori":
                    viewModel = utilizatoriVM;
                    break;
                case "Producatori":
                    viewModel = producatoriVM;
                    break;
                case "Bonuri":
                    viewModel = bonuriVM;
                    break;
                case "Categorii":
                    viewModel = categoriiVM;
                    break;
                default:
                    break;
            }

            SelectedTableDataGrid.DataContext = viewModel;

            if (viewModel is ProduseVM)
            {
                SelectedTableDataGrid.ItemsSource = (viewModel as ProduseVM).ProduseList;
            }
            else if (viewModel is CategoriiVM)
            {
                SelectedTableDataGrid.ItemsSource = (viewModel as CategoriiVM).CategoriiList;
            }
            else if (viewModel is StocuriVM)
            {
                SelectedTableDataGrid.ItemsSource = (viewModel as StocuriVM).StocuriList;
            }
            if (viewModel is BonuriVM)
            {
                var bonuriVM = viewModel as BonuriVM;
                SelectedTableDataGrid.ItemsSource = bonuriVM.BonuriList;
                var dataEliberareColumn = SelectedTableDataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "DataEliberare") as DataGridTextColumn;
                if (dataEliberareColumn != null)
                {
                    dataEliberareColumn.Binding.StringFormat = "dd.MM.yyyy";
                }
            }


            else if (viewModel is ProducatoriVM)
            {
                SelectedTableDataGrid.ItemsSource = (viewModel as ProducatoriVM).ProducatoriList;
            }
            else if (viewModel is UtilizatoriVM)
            {
                SelectedTableDataGrid.ItemsSource = (viewModel as UtilizatoriVM).UtilizatoriList;
            }

            foreach (var column in SelectedTableDataGrid.Columns)
            {
                if (column is DataGridTextColumn textColumn && textColumn.Binding is Binding binding && binding.Path != null && binding.Path.Path != null)
                {
                    string columnName = binding.Path.Path;
                    if (columnName == "DataAprovizionare" || columnName == "DataExpirare" || columnName == "DataEliberare")
                    {
                        textColumn.Binding.StringFormat = "dd.MM.yyyy"; // Formatul dorit pentru afișare
                    }
                }
            }


            HideIDColumn();
        }

        private void ProductByCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Window productsByCategoryWindow = new Window
            {
                Title = "Produse după categorie",
                Width = 600,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };

            DataGrid dataGrid = new DataGrid
            {
                AutoGenerateColumns = true,
                ItemsSource = productByCategoryVM.ProductsByCategoryList
            };

            productsByCategoryWindow.Content = dataGrid;
            productsByCategoryWindow.ShowDialog();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)TablesComboBox.SelectedItem;
            if (selectedItem == null) return;

            if (selectedItem.Content.ToString() == "Bonuri")
            {
                // Creăm o fereastră de dialog pentru adăugarea unui nou bon
                Window addBonWindow = new Window
                {
                    Title = "Adaugă Bon",
                    Width = 300,
                    Height = 250,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };

                StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                TextBlock dateLabel = new TextBlock { Text = "Data Eliberare" };
                DatePicker datePicker = new DatePicker();

                TextBlock casierIdLabel = new TextBlock { Text = "Casier ID" };
                TextBox casierIdBox = new TextBox();

                TextBlock sumaLabel = new TextBlock { Text = "Suma Încasată" };
                TextBox sumaBox = new TextBox();

                Button addButton = new Button { Content = "Adaugă", Margin = new Thickness(0, 10, 0, 0) };
                addButton.Click += (s, args) =>
                {
                    // Validăm datele introduse
                    if (datePicker.SelectedDate == null)
                    {
                        MessageBox.Show("Te rugăm să selectezi o dată.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(casierIdBox.Text) || !int.TryParse(casierIdBox.Text, out int casierId))
                    {
                        MessageBox.Show("Te rugăm să introduci un ID Casier valid.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(sumaBox.Text) || !decimal.TryParse(sumaBox.Text, out decimal sumaIncasata))
                    {
                        MessageBox.Show("Te rugăm să introduci o sumă validă.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Adăugăm bonul
                    Bonuri newBon = new Bonuri
                    {
                        DataEliberare = datePicker.SelectedDate.Value,
                        CasierId = casierId,
                        SumaIncasata = sumaIncasata
                    };

                    bonuriVM.AddCommand.Execute(newBon);

                    MessageBox.Show("Bon adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reîncarcăm datele pentru a reflecta adăugarea noului bon
                    LoadDataForSelectedTable();

                    addBonWindow.Close();
                };

                panel.Children.Add(dateLabel);
                panel.Children.Add(datePicker);
                panel.Children.Add(casierIdLabel);
                panel.Children.Add(casierIdBox);
                panel.Children.Add(sumaLabel);
                panel.Children.Add(sumaBox);
                panel.Children.Add(addButton);

                addBonWindow.Content = panel;
                addBonWindow.ShowDialog();
            }

            else if (selectedItem.Content.ToString() == "Categorii")
            {
                // Creăm o fereastră de dialog pentru adăugarea unei noi categorii
                Window addCategoryWindow = new Window
                {
                    Title = "Adaugă Categorie",
                    Width = 300,
                    Height = 200,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };

                StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                TextBlock nameLabel = new TextBlock { Text = "Nume Categorie" };
                TextBox nameBox = new TextBox();

                Button addButton = new Button { Content = "Adaugă", Margin = new Thickness(0, 10, 0, 0) };
                addButton.Click += (s, args) =>
                {
                    // Validăm datele introduse
                    if (string.IsNullOrWhiteSpace(nameBox.Text))
                    {
                        MessageBox.Show("Te rugăm să introduci un nume pentru categorie.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Adăugăm categoria
                    Categorii newCategory = new Categorii
                    {
                        NumeCategorie = nameBox.Text
                    };

                    categoriiVM.AddCommand.Execute(newCategory);

                    MessageBox.Show("Categorie adăugată cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reîncarcăm datele pentru a reflecta adăugarea noii categorii
                    LoadDataForSelectedTable();

                    addCategoryWindow.Close();
                };

                panel.Children.Add(nameLabel);
                panel.Children.Add(nameBox);
                panel.Children.Add(addButton);

                addCategoryWindow.Content = panel;
                addCategoryWindow.ShowDialog();
            }

            else if (selectedItem.Content.ToString() == "Producatori")
            {
                // Creăm o fereastră de dialog pentru adăugarea unui nou producător
                Window addProducerWindow = new Window
                {
                    Title = "Adaugă Producător",
                    Width = 300,
                    Height = 200,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };

                StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                TextBlock nameLabel = new TextBlock { Text = "Nume Producător" };
                TextBox nameBox = new TextBox();

                TextBlock countryLabel = new TextBlock { Text = "Țara de Origine" };
                TextBox countryBox = new TextBox();

                Button addButton = new Button { Content = "Adaugă", Margin = new Thickness(0, 10, 0, 0) };
                addButton.Click += (s, args) =>
                {
                    // Validăm datele introduse
                    if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(countryBox.Text))
                    {
                        MessageBox.Show("Te rugăm să completezi toate câmpurile.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Adăugăm producătorul
                    Producatori newProducer = new Producatori
                    {
                        NumeProducator = nameBox.Text,
                        TaraOrigine = countryBox.Text
                    };

                    producatoriVM.AddCommand.Execute(newProducer);

                    MessageBox.Show("Producător adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reîncărcăm datele pentru a reflecta adăugarea noului producător
                    LoadDataForSelectedTable();

                    addProducerWindow.Close();
                };

                panel.Children.Add(nameLabel);
                panel.Children.Add(nameBox);
                panel.Children.Add(countryLabel);
                panel.Children.Add(countryBox);
                panel.Children.Add(addButton);

                addProducerWindow.Content = panel;
                addProducerWindow.ShowDialog();
            }

            else if (selectedItem.Content.ToString() == "Produse")
            {
                // Creăm o fereastră de dialog pentru adăugarea unui nou produs
                Window addProductWindow = new Window
                {
                    Title = "Adaugă Produs",
                    Width = 300,
                    Height = 250,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };

                StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                TextBlock nameLabel = new TextBlock { Text = "Nume Produs" };
                TextBox nameBox = new TextBox();

                TextBlock barcodeLabel = new TextBlock { Text = "Cod Bare" };
                TextBox barcodeBox = new TextBox();

                TextBlock categoryLabel = new TextBlock { Text = "ID Categorie" };
                TextBox categoryIdBox = new TextBox();

                TextBlock producerLabel = new TextBlock { Text = "ID Producător" };
                TextBox producerIdBox = new TextBox();

                Button addButton = new Button { Content = "Adaugă", Margin = new Thickness(0, 10, 0, 0) };
                addButton.Click += (s, args) =>
                {
                    // Validăm datele introduse
                    if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(barcodeBox.Text) ||
                        string.IsNullOrWhiteSpace(categoryIdBox.Text) || string.IsNullOrWhiteSpace(producerIdBox.Text))
                    {
                        MessageBox.Show("Te rugăm să completezi toate câmpurile.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Convertim id-urile în valori numerice
                    if (!int.TryParse(categoryIdBox.Text, out int categoryId) || !int.TryParse(producerIdBox.Text, out int producerId))
                    {
                        MessageBox.Show("ID-urile categoriei și producătorului trebuie să fie valori numerice întregi.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Adăugăm produsul
                    Produse newProduct = new Produse
                    {
                        NumeProdus = nameBox.Text,
                        CodBare = barcodeBox.Text,
                        CategorieId = categoryId,
                        ProducatorId = producerId
                    };

                    produseVM.AddCommand.Execute(newProduct);

                    MessageBox.Show("Produs adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reîncărcăm datele pentru a reflecta adăugarea noului produs
                    LoadDataForSelectedTable();

                    addProductWindow.Close();
                };

                panel.Children.Add(nameLabel);
                panel.Children.Add(nameBox);
                panel.Children.Add(barcodeLabel);
                panel.Children.Add(barcodeBox);
                panel.Children.Add(categoryLabel);
                panel.Children.Add(categoryIdBox);
                panel.Children.Add(producerLabel);
                panel.Children.Add(producerIdBox);
                panel.Children.Add(addButton);

                addProductWindow.Content = panel;
                addProductWindow.ShowDialog();
            }

            else if (selectedItem.Content.ToString() == "Utilizatori")
            {
                // Creăm o fereastră de dialog pentru adăugarea unui nou utilizator
                Window addUserWindow = new Window
                {
                    Title = "Adaugă Utilizator",
                    Width = 300,
                    Height = 250,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };

                StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                TextBlock nameLabel = new TextBlock { Text = "Nume Utilizator" };
                TextBox nameBox = new TextBox();

                TextBlock passwordLabel = new TextBlock { Text = "Parolă" };
                TextBox passwordBox = new TextBox();

                TextBlock userTypeLabel = new TextBlock { Text = "Tip Utilizator" };
                ComboBox userTypeComboBox = new ComboBox();
                userTypeComboBox.Items.Add("Admin");
                userTypeComboBox.Items.Add("Angajat");
                userTypeComboBox.Items.Add("Casier");

                Button addButton = new Button { Content = "Adaugă", Margin = new Thickness(0, 10, 0, 0) };
                addButton.Click += (s, args) =>
                {
                    // Validăm datele introduse
                    if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Text) || userTypeComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Te rugăm să completezi toate câmpurile.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Adăugăm utilizatorul
                    Utilizatori newUtilizator = new Utilizatori
                    {
                        NumeUtilizator = nameBox.Text,
                        Parola = passwordBox.Text,
                        TipUtilizator = userTypeComboBox.SelectedItem.ToString()
                    };

                    utilizatoriVM.AddCommand.Execute(newUtilizator);

                    MessageBox.Show("Utilizator adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reîncărcăm datele pentru a reflecta adăugarea noului utilizator
                    LoadDataForSelectedTable();

                    addUserWindow.Close();
                };

                panel.Children.Add(nameLabel);
                panel.Children.Add(nameBox);
                panel.Children.Add(passwordLabel);
                panel.Children.Add(passwordBox);
                panel.Children.Add(userTypeLabel);
                panel.Children.Add(userTypeComboBox);
                panel.Children.Add(addButton);

                addUserWindow.Content = panel;
                addUserWindow.ShowDialog();
            }

            else if (selectedItem.Content.ToString() == "Stocuri")
            {
                Window addStockWindow = new Window
                {
                    Title = "Adaugă Stoc",
                    Width = 300,
                    Height = 350,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };

                StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                TextBlock productIdLabel = new TextBlock { Text = "ID Produs" };
                TextBox productIdBox = new TextBox();

                TextBlock quantityLabel = new TextBlock { Text = "Cantitate" };
                TextBox quantityBox = new TextBox();

                TextBlock unitLabel = new TextBlock { Text = "Unitate Măsură" };
                TextBox unitBox = new TextBox();

                TextBlock acquisitionPriceLabel = new TextBlock { Text = "Preț Achiziție" };
                TextBox acquisitionPriceBox = new TextBox();

                TextBlock purchaseDateLabel = new TextBlock { Text = "Data Aprovizionare" };
                DatePicker purchaseDatePicker = new DatePicker();

                TextBlock expiryDateLabel = new TextBlock { Text = "Data Expirare" };
                DatePicker expiryDatePicker = new DatePicker();

                Button addButton = new Button { Content = "Adaugă", Margin = new Thickness(0, 10, 0, 0) };
                addButton.Click += (s, args) =>
                {
                    // Validăm datele introduse
                    if (string.IsNullOrWhiteSpace(productIdBox.Text) || string.IsNullOrWhiteSpace(quantityBox.Text) || string.IsNullOrWhiteSpace(unitBox.Text) || string.IsNullOrWhiteSpace(acquisitionPriceBox.Text) || purchaseDatePicker.SelectedDate == null || expiryDatePicker.SelectedDate == null)
                    {
                        MessageBox.Show("Te rugăm să completezi toate câmpurile.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (!int.TryParse(productIdBox.Text, out int productId) || !int.TryParse(quantityBox.Text, out int quantity) || !decimal.TryParse(acquisitionPriceBox.Text, out decimal acquisitionPrice))
                    {
                        MessageBox.Show("Te rugăm să introduci date valide.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (expiryDatePicker.SelectedDate <= purchaseDatePicker.SelectedDate)
                    {
                        MessageBox.Show("Data de expirare trebuie să fie ulterioară datei de achiziție.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Calculăm prețul de vânzare utilizând metoda CalculateSellingPrice
                    decimal sellingPrice = CalculateSellingPrice(acquisitionPrice);

                    // Adăugăm stocul
                    Stocuri newStock = new Stocuri
                    {
                        ProductId = productId,
                        Cantitate = quantity,
                        UnitateMasura = unitBox.Text,
                        DataAprovizionare = purchaseDatePicker.SelectedDate.Value,
                        DataExpirare = expiryDatePicker.SelectedDate.Value,
                        PretAchizitie = acquisitionPrice,
                        PretVanzare = sellingPrice
                    };

                    stocuriVM.AddCommand.Execute(newStock);

                    MessageBox.Show("Stoc adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reîncărcăm datele pentru a reflecta adăugarea noului stoc
                    LoadDataForSelectedTable();

                    addStockWindow.Close();
                };

                panel.Children.Add(productIdLabel);
                panel.Children.Add(productIdBox);
                panel.Children.Add(quantityLabel);
                panel.Children.Add(quantityBox);
                panel.Children.Add(unitLabel);
                panel.Children.Add(unitBox);
                panel.Children.Add(acquisitionPriceLabel);
                panel.Children.Add(acquisitionPriceBox);
                panel.Children.Add(purchaseDateLabel);
                panel.Children.Add(purchaseDatePicker);
                panel.Children.Add(expiryDateLabel);
                panel.Children.Add(expiryDatePicker); 
                panel.Children.Add(addButton); 

                addStockWindow.Content = panel;
                addStockWindow.ShowDialog();
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)TablesComboBox.SelectedItem;
            if (selectedItem == null) return;

            if (selectedItem.Content.ToString() == "Bonuri")
            {
                if (SelectedTableDataGrid.SelectedItem is Bonuri selectedBon)
                {
                    MessageBoxResult result = MessageBox.Show("Ești sigur că vrei să ștergi acest bon?", "Confirmare ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bonuriVM.DeleteCommand.Execute(selectedBon.BonId); // Transmite doar ID-ul bonului către comanda DeleteCommand
                        LoadDataForSelectedTable();
                    }
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un bon pentru a-l șterge.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Categorii")
            {
                if (SelectedTableDataGrid.SelectedItem is Categorii selectedCategory)
                {
                    MessageBoxResult result = MessageBox.Show($"Ești sigur că vrei să ștergi categoria '{selectedCategory.NumeCategorie}' cu ID-ul {selectedCategory.CategorieId}?", "Confirmare ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        categoriiVM.DeleteCommand.Execute(selectedCategory.CategorieId); // Transmite doar ID-ul categoriei către comanda DeleteCommand
                        LoadDataForSelectedTable();
                    }
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi o categorie pentru a o șterge.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Producatori")
            {
                if (SelectedTableDataGrid.SelectedItem is Producatori selectedProducer)
                {
                    MessageBoxResult result = MessageBox.Show($"Ești sigur că vrei să ștergi producătorul ?", "Confirmare ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        producatoriVM.DeleteCommand.Execute(selectedProducer.ProducatorId); // Transmite doar ID-ul producătorului către comanda DeleteCommand
                        LoadDataForSelectedTable();
                    }
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un producător pentru a-l șterge.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Produse")
            {
                if (SelectedTableDataGrid.SelectedItem is Produse selectedProduct)
                {
                    MessageBoxResult result = MessageBox.Show($"Ești sigur că vrei să ștergi produsul ?", "Confirmare ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        produseVM.DeleteCommand.Execute(selectedProduct.ProductId); 
                        LoadDataForSelectedTable();
                    }
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un produs pentru a-l șterge.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Stocuri")
            {
                if (SelectedTableDataGrid.SelectedItem is Stocuri selectedStock)
                {
                    MessageBoxResult result = MessageBox.Show($"Ești sigur că vrei să ștergi stocul ?", "Confirmare ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        stocuriVM.DeleteCommand.Execute(selectedStock.StocId);
                        LoadDataForSelectedTable();
                    }
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un stoc pentru a-l șterge.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Utilizatori")
            {
                if (SelectedTableDataGrid.SelectedItem is Utilizatori selectedUser)
                {
                    MessageBoxResult result = MessageBox.Show($"Ești sigur că vrei să ștergi utilizatorul ?", "Confirmare ștergere", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        utilizatoriVM.DeleteCommand.Execute(selectedUser.UtilizatoriId);
                        LoadDataForSelectedTable();
                    }
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un utilizator pentru a-l șterge.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }


        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)TablesComboBox.SelectedItem;
            if (selectedItem == null) return;

            if (selectedItem.Content.ToString() == "Bonuri")
            {
                if (SelectedTableDataGrid.SelectedItem is Bonuri selectedBon)
                {
                    // Creăm o fereastră de dialog pentru actualizarea bonului
                    Window updateBonWindow = new Window
                    {
                        Title = "Actualizează Bon",
                        Width = 300,
                        Height = 250,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = this
                    };

                    StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                    TextBlock dateLabel = new TextBlock { Text = "Data Eliberare" };
                    DatePicker datePicker = new DatePicker();
                    datePicker.SelectedDate = selectedBon.DataEliberare; // Completăm câmpul cu data existentă

                    TextBlock casierIdLabel = new TextBlock { Text = "Casier ID" };
                    TextBox casierIdBox = new TextBox();
                    casierIdBox.Text = selectedBon.CasierId.ToString(); // Completăm câmpul cu ID-ul casierului existent

                    TextBlock sumaLabel = new TextBlock { Text = "Suma Încasată" };
                    TextBox sumaBox = new TextBox();
                    sumaBox.Text = selectedBon.SumaIncasata.ToString(); // Completăm câmpul cu suma existentă

                    Button updateButton = new Button { Content = "Actualizează", Margin = new Thickness(0, 10, 0, 0) };
                    updateButton.Click += (s, args) =>
                    {
                        // Validăm datele introduse
                        if (datePicker.SelectedDate == null || string.IsNullOrWhiteSpace(casierIdBox.Text) || !int.TryParse(casierIdBox.Text, out int casierId) || string.IsNullOrWhiteSpace(sumaBox.Text) || !decimal.TryParse(sumaBox.Text, out decimal sumaIncasata))
                        {
                            MessageBox.Show("Te rugăm să completezi toate câmpurile cu date valide.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Actualizăm bonul
                        selectedBon.DataEliberare = datePicker.SelectedDate.Value;
                        selectedBon.CasierId = casierId;
                        selectedBon.SumaIncasata = sumaIncasata;

                        // Apelăm metoda de actualizare din ViewModel
                        bonuriVM.UpdateCommand.Execute(selectedBon);

                        MessageBox.Show("Bon actualizat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Reîncărcăm datele pentru a reflecta modificările
                        LoadDataForSelectedTable();

                        updateBonWindow.Close();
                    };

                    panel.Children.Add(dateLabel);
                    panel.Children.Add(datePicker);
                    panel.Children.Add(casierIdLabel);
                    panel.Children.Add(casierIdBox);
                    panel.Children.Add(sumaLabel);
                    panel.Children.Add(sumaBox);
                    panel.Children.Add(updateButton);

                    updateBonWindow.Content = panel;
                    updateBonWindow.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Te rugăm să selectezi un bon pentru a-l actualiza.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Produse")
            {
                if (SelectedTableDataGrid.SelectedItem is Produse selectedProduct)
                {
                    // Creăm o fereastră de dialog pentru actualizarea produsului
                    Window updateProductWindow = new Window
                    {
                        Title = "Actualizează Produs",
                        Width = 300,
                        Height = 250,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = this
                    };

                    StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                    TextBlock nameLabel = new TextBlock { Text = "Nume Produs" };
                    TextBox nameBox = new TextBox();
                    nameBox.Text = selectedProduct.NumeProdus; // Completăm câmpul cu numele produsului existent

                    TextBlock barcodeLabel = new TextBlock { Text = "Cod Bare" };
                    TextBox barcodeBox = new TextBox();
                    barcodeBox.Text = selectedProduct.CodBare; // Completăm câmpul cu codul bare existent al produsului

                    TextBlock categoryIdLabel = new TextBlock { Text = "ID Categorie" };
                    TextBox categoryIdBox = new TextBox();
                    categoryIdBox.Text = selectedProduct.CategorieId.ToString(); // Completăm câmpul cu ID-ul categoriei existente a produsului

                    TextBlock producerIdLabel = new TextBlock { Text = "ID Producator" };
                    TextBox producerIdBox = new TextBox();
                    producerIdBox.Text = selectedProduct.ProducatorId.ToString(); // Completăm câmpul cu ID-ul producătorului existent al produsului

                    Button updateButton = new Button { Content = "Actualizează", Margin = new Thickness(0, 10, 0, 0) };
                    updateButton.Click += (s, args) =>
                    {
                        // Validăm datele introduse
                        if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(barcodeBox.Text) || !int.TryParse(categoryIdBox.Text, out int categoryId) || !int.TryParse(producerIdBox.Text, out int producerId))
                        {
                            MessageBox.Show("Te rugăm să completezi toate câmpurile cu date valide.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Actualizăm produsul
                        selectedProduct.NumeProdus = nameBox.Text;
                        selectedProduct.CodBare = barcodeBox.Text;
                        selectedProduct.CategorieId = categoryId;
                        selectedProduct.ProducatorId = producerId;

                        // Apelăm metoda de actualizare din ViewModel
                        produseVM.UpdateCommand.Execute(selectedProduct);

                        MessageBox.Show("Produs actualizat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Reîncărcăm datele pentru a reflecta modificările
                        LoadDataForSelectedTable();

                        updateProductWindow.Close();
                    };

                    panel.Children.Add(nameLabel);
                    panel.Children.Add(nameBox);
                    panel.Children.Add(barcodeLabel);
                    panel.Children.Add(barcodeBox);
                    panel.Children.Add(categoryIdLabel);
                    panel.Children.Add(categoryIdBox);
                    panel.Children.Add(producerIdLabel);
                    panel.Children.Add(producerIdBox);
                    panel.Children.Add(updateButton);

                    updateProductWindow.Content = panel;
                    updateProductWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un produs pentru a-l actualiza.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Categorii")
            {
                if (SelectedTableDataGrid.SelectedItem is Categorii selectedCategory)
                {
                    // Creăm o fereastră de dialog pentru actualizarea categoriei
                    Window updateCategoryWindow = new Window
                    {
                        Title = "Actualizează Categorie",
                        Width = 300,
                        Height = 200,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = this
                    };

                    StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                    TextBlock nameLabel = new TextBlock { Text = "Nume Categorie" };
                    TextBox nameBox = new TextBox();
                    nameBox.Text = selectedCategory.NumeCategorie; // Completăm câmpul cu numele categoriei existente

                    Button updateButton = new Button { Content = "Actualizează", Margin = new Thickness(0, 10, 0, 0) };
                    updateButton.Click += (s, args) =>
                    {
                        // Validăm datele introduse
                        if (string.IsNullOrWhiteSpace(nameBox.Text))
                        {
                            MessageBox.Show("Te rugăm să completezi numele categoriei.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Actualizăm categoria
                        selectedCategory.NumeCategorie = nameBox.Text;

                        // Apelăm metoda de actualizare din ViewModel
                        categoriiVM.UpdateCommand.Execute(selectedCategory);

                        MessageBox.Show("Categorie actualizată cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Reîncărcăm datele pentru a reflecta modificările
                        LoadDataForSelectedTable();

                        updateCategoryWindow.Close();
                    };

                    panel.Children.Add(nameLabel);
                    panel.Children.Add(nameBox);
                    panel.Children.Add(updateButton);

                    updateCategoryWindow.Content = panel;
                    updateCategoryWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi o categorie pentru a o actualiza.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }

            else if (selectedItem.Content.ToString() == "Producatori")
            {
                if (SelectedTableDataGrid.SelectedItem is Producatori selectedProducer)
                {
                    // Creăm o fereastră de dialog pentru actualizarea producătorului
                    Window updateProducerWindow = new Window
                    {
                        Title = "Actualizează Producător",
                        Width = 300,
                        Height = 200,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = this
                    };

                    StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                    TextBlock nameLabel = new TextBlock { Text = "Nume Producător" };
                    TextBox nameBox = new TextBox();
                    nameBox.Text = selectedProducer.NumeProducator; // Completăm câmpul cu numele producătorului existent

                    Button updateButton = new Button { Content = "Actualizează", Margin = new Thickness(0, 10, 0, 0) };
                    updateButton.Click += (s, args) =>
                    {
                        // Validăm datele introduse
                        if (string.IsNullOrWhiteSpace(nameBox.Text))
                        {
                            MessageBox.Show("Te rugăm să completezi numele producătorului.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Actualizăm producătorul
                        selectedProducer.NumeProducator = nameBox.Text;

                        // Apelăm metoda de actualizare din ViewModel
                        producatoriVM.UpdateCommand.Execute(selectedProducer);

                        MessageBox.Show("Producător actualizat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Reîncărcăm datele pentru a reflecta modificările
                        LoadDataForSelectedTable();

                        updateProducerWindow.Close();
                    };

                    panel.Children.Add(nameLabel);
                    panel.Children.Add(nameBox);
                    panel.Children.Add(updateButton);

                    updateProducerWindow.Content = panel;
                    updateProducerWindow.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Te rugăm să selectezi un producător pentru a-l actualiza.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (selectedItem.Content.ToString() == "Utilizatori")
            {
                if (SelectedTableDataGrid.SelectedItem is Utilizatori selectedUser)
                {
                    // Creăm o fereastră de dialog pentru actualizarea utilizatorului
                    Window updateUserWindow = new Window
                    {
                        Title = "Actualizează Utilizator",
                        Width = 300,
                        Height = 250,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = this
                    };

                    StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                    TextBlock usernameLabel = new TextBlock { Text = "Nume Utilizator" };
                    TextBox usernameBox = new TextBox();
                    usernameBox.Text = selectedUser.NumeUtilizator; // Completăm câmpul cu numele utilizatorului existent

                    TextBlock passwordLabel = new TextBlock { Text = "Parolă" };
                    PasswordBox passwordBox = new PasswordBox(); // Folosim PasswordBox pentru a ascunde parola
                    passwordBox.Password = selectedUser.Parola; // Completăm câmpul cu parola existentă

                    Button updateButton = new Button { Content = "Actualizează", Margin = new Thickness(0, 10, 0, 0) };
                    updateButton.Click += (s, args) =>
                    {
                        // Validăm datele introduse
                        if (string.IsNullOrWhiteSpace(usernameBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Password))
                        {
                            MessageBox.Show("Te rugăm să completezi toate câmpurile.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Actualizăm utilizatorul
                        selectedUser.NumeUtilizator = usernameBox.Text;
                        selectedUser.Parola = passwordBox.Password;

                        // Apelăm metoda de actualizare din ViewModel
                        utilizatoriVM.UpdateCommand.Execute(selectedUser);

                        MessageBox.Show("Utilizator actualizat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Reîncărcăm datele pentru a reflecta modificările
                        LoadDataForSelectedTable();

                        updateUserWindow.Close();
                    };

                    panel.Children.Add(usernameLabel);
                    panel.Children.Add(usernameBox);
                    panel.Children.Add(passwordLabel);
                    panel.Children.Add(passwordBox);
                    panel.Children.Add(updateButton);

                    updateUserWindow.Content = panel;
                    updateUserWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un utilizator pentru a-l actualiza.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }



            else if (selectedItem.Content.ToString() == "Stocuri")
            {
                if (SelectedTableDataGrid.SelectedItem is Stocuri selectedStock)
                {
                    // Creăm o fereastră de dialog pentru actualizarea stocului
                    Window updateStockWindow = new Window
                    {
                        Title = "Actualizează Stoc",
                        Width = 400,
                        Height = 350,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = this
                    };

                    StackPanel panel = new StackPanel { Margin = new Thickness(10) };

                    TextBlock productIdLabel = new TextBlock { Text = "ID Produs" };
                    TextBox productIdBox = new TextBox();
                    productIdBox.Text = selectedStock.ProductId.ToString(); // Completăm câmpul cu ID-ul produsului existent
                    productIdBox.IsEnabled = false; // Nu permitem modificarea ID-ului produsului

                    TextBlock quantityLabel = new TextBlock { Text = "Cantitate" };
                    TextBox quantityBox = new TextBox();
                    quantityBox.Text = selectedStock.Cantitate.ToString(); // Completăm câmpul cu cantitatea existentă

                    TextBlock unitLabel = new TextBlock { Text = "Unitate de Măsură" };
                    TextBox unitBox = new TextBox();
                    unitBox.Text = selectedStock.UnitateMasura; // Completăm câmpul cu unitatea de măsură existentă

                    TextBlock supplyDateLabel = new TextBlock { Text = "Data Aprovizionare" };
                    DatePicker supplyDatePicker = new DatePicker();
                    supplyDatePicker.SelectedDate = selectedStock.DataAprovizionare; // Completăm câmpul cu data de aprovizionare existentă

                    TextBlock expiryDateLabel = new TextBlock { Text = "Data Expirare" };
                    DatePicker expiryDatePicker = new DatePicker();
                    expiryDatePicker.SelectedDate = selectedStock.DataExpirare; // Completăm câmpul cu data de expirare existentă

                    TextBlock purchasePriceLabel = new TextBlock { Text = "Preț Achiziție" };
                    TextBox purchasePriceBox = new TextBox();
                    purchasePriceBox.Text = selectedStock.PretAchizitie.ToString(); // Completăm câmpul cu prețul de achiziție existent
                    purchasePriceBox.IsEnabled = false; // Nu permitem modificarea prețului de achiziție

                    TextBlock sellingPriceLabel = new TextBlock { Text = "Preț Vânzare" };
                    TextBox sellingPriceBox = new TextBox();
                    sellingPriceBox.Text = selectedStock.PretVanzare.ToString(); // Completăm câmpul cu prețul de vânzare existent

                    Button updateButton = new Button { Content = "Actualizează", Margin = new Thickness(0, 10, 0, 0) };
                    updateButton.Click += (s, args) =>
                    {
                        // Validăm datele introduse
                        if (!int.TryParse(productIdBox.Text, out int productId) ||
                            !int.TryParse(quantityBox.Text, out int quantity) ||
                            string.IsNullOrWhiteSpace(unitBox.Text) ||
                            supplyDatePicker.SelectedDate == null ||
                            expiryDatePicker.SelectedDate == null ||
                            !decimal.TryParse(sellingPriceBox.Text, out decimal sellingPrice) ||
                            sellingPrice <= selectedStock.PretAchizitie) // Verificăm dacă prețul de vânzare este mai mare decât prețul de achiziție
                        {
                            MessageBox.Show("Te rugăm să completezi toate câmpurile cu date valide și să asiguri că prețul de vânzare este mai mare decât prețul de achiziție.", "Eroare de validare", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Actualizăm stocul
                        selectedStock.Cantitate = quantity;
                        selectedStock.UnitateMasura = unitBox.Text;
                        selectedStock.DataAprovizionare = supplyDatePicker.SelectedDate.Value;
                        selectedStock.DataExpirare = expiryDatePicker.SelectedDate.Value;
                        selectedStock.PretVanzare = sellingPrice;

                        // Apelăm metoda de actualizare din ViewModel
                        stocuriVM.UpdateCommand.Execute(selectedStock);

                        MessageBox.Show("Stoc actualizat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Reîncărcăm datele pentru a reflecta modificările
                        LoadDataForSelectedTable();

                        updateStockWindow.Close();
                    };

                    panel.Children.Add(productIdLabel);
                    panel.Children.Add(productIdBox);
                    panel.Children.Add(quantityLabel);
                    panel.Children.Add(quantityBox);
                    panel.Children.Add(unitLabel);
                    panel.Children.Add(unitBox);
                    panel.Children.Add(supplyDateLabel);
                    panel.Children.Add(supplyDatePicker);
                    panel.Children.Add(expiryDateLabel);
                    panel.Children.Add(expiryDatePicker);
                    panel.Children.Add(purchasePriceLabel);
                    panel.Children.Add(purchasePriceBox);
                    panel.Children.Add(sellingPriceLabel);
                    panel.Children.Add(sellingPriceBox);
                    panel.Children.Add(updateButton);

                    updateStockWindow.Content = panel;
                    updateStockWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Te rugăm să selectezi un stoc pentru a-l actualiza.", "Selectare necesară", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }


        }



        private void HideIDColumn()
        {

            List<string> idStrings = new List<string> { "ID", "Id", "id" };


            foreach (var column in SelectedTableDataGrid.Columns)
            {
                foreach (string idString in idStrings)
                {
                    if (column.Header != null && column.Header.ToString().Contains(idString))
                    {
                        column.Visibility = Visibility.Collapsed;
                        return;
                    }
                }
            }
        }

        public decimal CalculateSellingPrice(decimal acquisitionPrice)
        {
            decimal profitMargin = 0.4m;
            decimal sellingPrice = acquisitionPrice * (1 + profitMargin);
            return sellingPrice;
        }

        private void TablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDataForSelectedTable();
        }






    }
}

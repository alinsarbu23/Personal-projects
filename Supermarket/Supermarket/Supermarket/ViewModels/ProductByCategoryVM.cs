using System.Collections.ObjectModel;
using BusinessLogicLayer;
using Supermarket.Models;
using Supermarket.Models.BusinessLogicLayer;

namespace Supermarket.ViewModels
{
    public class ProductByCategoryVM : BasePropertyChanged
    {
        private readonly ProductByCategoryBLL productByCategoryBLL;

        public ProductByCategoryVM()
        {
            productByCategoryBLL = new ProductByCategoryBLL();
            ProductsByCategoryList = new ObservableCollection<ProductByCategory>();
            LoadProductsByCategory();
        }

        private ObservableCollection<ProductByCategory> productsByCategoryList;
        public ObservableCollection<ProductByCategory> ProductsByCategoryList
        {
            get { return productsByCategoryList; }
            set
            {
                productsByCategoryList = value;
                OnPropertyChanged(nameof(ProductsByCategoryList));
            }
        }

        private void LoadProductsByCategory()
        {
            var products = productByCategoryBLL.GetSumPricesByCategory();
            ProductsByCategoryList = new ObservableCollection<ProductByCategory>(products);
        }
    }
}

using System.Collections.Generic;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.BusinessLogicLayer
{
    public class ProductByCategoryBLL
    {
        private readonly ProductByCategoryDAL productByCategoryDAL;

        public ProductByCategoryBLL()
        {
            productByCategoryDAL = new ProductByCategoryDAL();
        }

        public List<ProductByCategory> GetSumPricesByCategory()
        {
            return productByCategoryDAL.GetSumPricesByCategory();
        }
    }
}

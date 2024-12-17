// CategoriiBLL.cs

using System.Collections.Generic;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.BussinessLogicLayer
{
    internal class CategoriiBLL
    {
        private readonly CategoriiDAL categoriiDAL;

        public CategoriiBLL()
        {
            categoriiDAL = new CategoriiDAL();
        }

        public void AddCategorieProdus(Categorii categorie)
        {
            categoriiDAL.AddCategorieProdus(categorie);
        }

        public void DeleteCategorieProdus(int categorieId)
        {
            categoriiDAL.DeleteCategorieProdus(categorieId);
        }

        public List<Categorii> GetCategoriiProduse()
        {
            return categoriiDAL.GetCategoriiProduse();
        }

        public void UpdateCategorieProdus(Categorii categorie)
        {
            categoriiDAL.UpdateCategorieProdus(categorie);
        }

    }
}

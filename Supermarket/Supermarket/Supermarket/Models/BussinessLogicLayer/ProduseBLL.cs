using System;
using System.Collections.Generic;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.BussinessLogicLayer
{
    internal class ProduseBLL
    {
        private ProduseDAL produseDAL;

        public ProduseBLL()
        {
            produseDAL = new ProduseDAL();
        }

        public void AddProdus(Produse produs)
        {
            produseDAL.AddProdus(produs);
        }

        public void DeleteProdus(int produsId)
        {
            produseDAL.DeleteProdus(produsId);
        }

        public List<Produse> GetAllProduse()
        {
            return produseDAL.GetAllProduse();
        }

        public void UpdateProdus(Produse produs)
        {
            produseDAL.UpdateProdus(produs);
        }

        public List<ProdusDetaliat> SearchProducts(string searchTerm)
        {
            // Inițializează o listă în care să stocăm rezultatele căutării
            List<ProdusDetaliat> rezultateCautare = new List<ProdusDetaliat>();

            try
            {
                // Creează o instanță a clasei ProduseDAL
                ProduseDAL produseDAL = new ProduseDAL();

                // Apelul metodei SearchProductsDetailed din ProduseDAL pentru a efectua căutarea
                rezultateCautare = produseDAL.SearchProductsDetailed(searchTerm);
            }
            catch (Exception ex)
            {
                // Gestionează orice excepție care ar putea apărea și afișează un mesaj de eroare
                Console.WriteLine("An error occurred while searching for products: " + ex.Message);
            }

            // Returnează rezultatele căutării
            return rezultateCautare;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.BussinessLogicLayer
{
    public class Produse
    {
        private int productId;
        private string numeProdus;
        private string codBare;
        private int categorieId;
        private int producatorId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public string NumeProdus
        {
            get { return numeProdus; }
            set { numeProdus = value; }
        }

        public string CodBare
        {
            get { return codBare; }
            set { codBare = value; }
        }

        public int CategorieId
        {
            get { return categorieId; }
            set { categorieId = value; }
        }

        public int ProducatorId
        {
            get { return producatorId; }
            set { producatorId = value; }
        }
    }

}

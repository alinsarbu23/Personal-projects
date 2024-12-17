using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.BussinessLogicLayer
{
    public class Categorii
    {
        private int categorieId;
        private string numeCategorie;

        public int CategorieId
        {
            get { return categorieId; }
            set
            {
                categorieId = value;
            }
        }

        public string NumeCategorie
        {
            get { return numeCategorie; }
            set { numeCategorie = value; }
        }
    }
}

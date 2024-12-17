using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.BussinessLogicLayer
{
    public class Utilizatori
    {
        private int utilizatoriId;
        private string numeUtilizator;
        private string parola;
        private string tipUtilizator;

        public int UtilizatoriId
        {
            get { return utilizatoriId; }
            set { utilizatoriId = value; }
        }

        public string NumeUtilizator
        {
            get { return numeUtilizator; }
            set { numeUtilizator = value; }
        }

        public string Parola
        {
            get { return parola; }
            set { parola = value; }
        }

        public string TipUtilizator
        {
            get { return tipUtilizator; }
            set { tipUtilizator = value; }
        }
    }

}

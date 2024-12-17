using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.BussinessLogicLayer
{
    public class Bonuri
    {
        private int bonId;
        private DateTime dataEliberare;
        private int casierId;
        private decimal sumaIncasata;

        public int BonId
        {
            get { return bonId; }
            set { bonId = value; }
        }

        public DateTime DataEliberare
        {
            get { return dataEliberare; }
            set { dataEliberare = value; }
        }

        public int CasierId
        {
            get { return casierId; }
            set { casierId = value; }
        }

        public decimal SumaIncasata
        {
            get { return sumaIncasata; }
            set { sumaIncasata = value; }
        }
    }

}

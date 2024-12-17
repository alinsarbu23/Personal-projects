using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.BussinessLogicLayer
{
    public class Producatori
    {
        private int producatorId;
        private string numeProducator;
        private string taraOrigine;

        public int ProducatorId
        {
            get { return producatorId; }
            set { producatorId = value; }
        }

        public string NumeProducator
        {
            get { return numeProducator; }
            set { numeProducator = value; }
        }

        public string TaraOrigine
        {
            get { return taraOrigine; }
            set { taraOrigine = value; }
        }
    }

}

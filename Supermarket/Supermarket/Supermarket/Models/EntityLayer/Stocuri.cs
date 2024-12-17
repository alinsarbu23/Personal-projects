using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.BussinessLogicLayer
{
    public class Stocuri
    {
        private int stocId;
        private int productId;
        private int cantitate;
        private string unitateMasura;
        private DateTime dataAprovizionare;
        private DateTime dataExpirare;
        private decimal pretAchizitie;
        private decimal pretVanzare;

        public int StocId
        {
            get { return stocId; }
            set { stocId = value; }
        }

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public int Cantitate
        {
            get { return cantitate; }
            set { cantitate = value; }
        }

        public string UnitateMasura
        {
            get { return unitateMasura; }
            set { unitateMasura = value; }
        }

        public DateTime DataAprovizionare
        {
            get { return dataAprovizionare; }
            set { dataAprovizionare = value; }
        }

        public DateTime DataExpirare
        {
            get { return dataExpirare; }
            set { dataExpirare = value; }
        }

        public decimal PretAchizitie
        {
            get { return pretAchizitie; }
            set { pretAchizitie = value; }
        }

        public decimal PretVanzare
        {
            get { return pretVanzare; }
            set { pretVanzare = value; }
        }
    }

}

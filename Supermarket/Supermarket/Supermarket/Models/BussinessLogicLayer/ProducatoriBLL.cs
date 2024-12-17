using System;
using System.Collections.Generic;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.BussinessLogicLayer
{
    internal class ProducatoriBLL
    {
        private readonly ProducatoriDAL producatoriDAL;

        public ProducatoriBLL()
        {
            producatoriDAL = new ProducatoriDAL();
        }

        public void AddProducator(Producatori producator)
        {
            producatoriDAL.AddProducator(producator);
        }

        public void DeleteProducator(int producatorId)
        {
            producatoriDAL.DeleteProducator(producatorId);
        }

        public List<Producatori> GetAllProducatori()
        {
            return producatoriDAL.GetAllProducatori();
        }

        public void UpdateProducator(Producatori producator)
        {
            producatoriDAL.UpdateProducator(producator);
        }

        // Alte metode pentru alte operații specifice cu producătorii
    }
}

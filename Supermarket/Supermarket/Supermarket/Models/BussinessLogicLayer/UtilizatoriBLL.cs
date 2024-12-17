using System;
using System.Collections.Generic;
using Supermarket.Models.BussinessLogicLayer;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.BussinessLogicLayer
{
    internal class UtilizatoriBLL
    {
        private UtilizatoriDAL utilizatoriDAL;

        public UtilizatoriBLL()
        {
            utilizatoriDAL = new UtilizatoriDAL();
        }

        public void AddUtilizator(Utilizatori utilizator)
        {
            utilizatoriDAL.AddUtilizator(utilizator);
        }

        public void DeleteUtilizator(int utilizatorId)
        {
            utilizatoriDAL.DeleteUtilizator(utilizatorId);
        }

        public List<Utilizatori> GetAllUtilizatori()
        {
            return utilizatoriDAL.GetAllUtilizatori();
        }

        public void UpdateUtilizator(Utilizatori utilizator)
        {
            utilizatoriDAL.UpdateUtilizator(utilizator);
        }
    }
}

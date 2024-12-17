using System;
using System.Collections.Generic;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.BussinessLogicLayer
{
    internal class BonuriBLL
    {
        private readonly BonuriDAL bonuriDAL;

        public BonuriBLL()
        {
            bonuriDAL = new BonuriDAL();
        }

        public void AddBon(Bonuri bon)
        {
            bonuriDAL.AddBon(bon);
        }

        public void DeleteBon(int bonId)
        {
            bonuriDAL.DeleteBon(bonId);
        }

        public List<Bonuri> GetAllBonuri()
        {
            return bonuriDAL.GetAllBonuri();
        }

        public void UpdateBon(Bonuri bon)
        {
            bonuriDAL.UpdateBon(bon);
        }

        public List<SumaIncasataPeZi> GetSumeIncasatePeZi(int utilizatorId, DateTime luna)
        {
            return bonuriDAL.GetSumeIncasatePeZi(utilizatorId, luna);
        }

    }
}

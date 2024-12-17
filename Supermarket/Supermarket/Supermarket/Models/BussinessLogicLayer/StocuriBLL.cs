using System;
using System.Collections.Generic;
using Supermarket.Models.BussinessLogicLayer;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.BussinessLogicLayer
{
    internal class StocuriBLL
    {
        private StocuriDAL stocuriDAL;

        public StocuriBLL()
        {
            stocuriDAL = new StocuriDAL();
        }

        public void AddStoc(Stocuri stoc)
        {
            stocuriDAL.AddStoc(stoc);
        }

        public void DeleteStoc(int stocId)
        {
            stocuriDAL.DeleteStoc(stocId);
        }

        public List<Stocuri> GetAllStocuri()
        {
            return stocuriDAL.GetAllStocuri();
        }

        public void UpdateStoc(Stocuri stoc)
        {
            stocuriDAL.UpdateStoc(stoc);
        }
    }
}

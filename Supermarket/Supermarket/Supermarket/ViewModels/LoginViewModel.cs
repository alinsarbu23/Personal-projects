using Supermarket.Models.DataAccessLayer;
using System;
using System.Windows;

namespace Supermarket.ViewModels
{
    public class LoginViewModel
    {
        private readonly UtilizatoriDAL utilizatoriDAL;

        public LoginViewModel()
        {
            utilizatoriDAL = new UtilizatoriDAL();
        }

        public bool Login(string username, string password)
        {
            return utilizatoriDAL.ValidateAdmin(username, password);
        }
    }
}

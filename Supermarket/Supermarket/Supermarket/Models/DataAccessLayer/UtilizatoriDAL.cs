using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Supermarket.Models.BussinessLogicLayer;
using System.Windows;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.DataAccessLayer
{
    internal class UtilizatoriDAL
    {
        public void AddUtilizator(Utilizatori utilizator)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("AddUtilizator", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NumeUtilizator", utilizator.NumeUtilizator);
                        cmd.Parameters.AddWithValue("@Parola", utilizator.Parola);
                        cmd.Parameters.AddWithValue("@TipUtilizator", utilizator.TipUtilizator);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding user: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteUtilizator(int utilizatorId)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("DeleteUtilizator", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_UtilizatorId", utilizatorId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting user: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Utilizatori> GetAllUtilizatori()
        {
            List<Utilizatori> utilizatori = new List<Utilizatori>();
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetUtilizatori", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Utilizatori utilizator = new Utilizatori
                                {
                                    UtilizatoriId = Convert.ToInt32(reader["UtilizatorId"]),
                                    NumeUtilizator = reader["NumeUtilizator"].ToString(),
                                    Parola = reader["Parola"].ToString(),
                                    TipUtilizator = reader["TipUtilizator"].ToString()
                                };
                                utilizatori.Add(utilizator);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving users: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return utilizatori;
        }

        public void UpdateUtilizator(Utilizatori utilizator)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdateUtilizator", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pUtilizatoriId", utilizator.UtilizatoriId);
                        cmd.Parameters.AddWithValue("@pNumeUtilizator", utilizator.NumeUtilizator);
                        cmd.Parameters.AddWithValue("@pParola", utilizator.Parola);
                        cmd.Parameters.AddWithValue("@pTipUtilizator", utilizator.TipUtilizator);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating user: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public bool ValidateAdmin(string username, string password)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("ValidateAdmin", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pNumeUtilizator", username);
                        cmd.Parameters.AddWithValue("@pParola", password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string userType = reader["TipUtilizator"].ToString();
                                // Check if the user type is admin or administrator
                                if (userType.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
                                    userType.Equals("administrator", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Additional check for correct username and password
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating user: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }


    }
}

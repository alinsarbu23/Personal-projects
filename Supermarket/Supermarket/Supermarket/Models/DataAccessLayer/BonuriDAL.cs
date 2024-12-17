using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using Supermarket.Models.BussinessLogicLayer;

namespace Supermarket.Models.DataAccessLayer
{
    internal class BonuriDAL
    {
        public void AddBon(Bonuri bon)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("AddBonCasa", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataEliberare", bon.DataEliberare);
                        cmd.Parameters.AddWithValue("@CasierId", bon.CasierId);
                        cmd.Parameters.AddWithValue("@SumaIncasata", bon.SumaIncasata);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding a receipt: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteBon(int bonId)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("DeleteBonCasa", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_BonId", bonId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting a receipt: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Bonuri> GetAllBonuri()
        {
            List<Bonuri> bonuriList = new List<Bonuri>();
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetAllBonuriCasa", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Bonuri bon = new Bonuri();
                                bon.BonId = Convert.ToInt32(reader["BonId"]);
                                bon.DataEliberare = Convert.ToDateTime(reader["DataEliberare"]);
                                bon.CasierId = Convert.ToInt32(reader["CasierId"]);
                                bon.SumaIncasata = Convert.ToDecimal(reader["SumaIncasata"]);
                                bonuriList.Add(bon);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the list of receipts: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return bonuriList;
        }

        public void UpdateBon(Bonuri bon)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdateBonCasa", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_BonId", bon.BonId);
                        cmd.Parameters.AddWithValue("@p_DataEliberare", bon.DataEliberare);
                        cmd.Parameters.AddWithValue("@p_CasierId", bon.CasierId);
                        cmd.Parameters.AddWithValue("@p_SumaIncasata", bon.SumaIncasata);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating a receipt: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<SumaIncasataPeZi> GetSumeIncasatePeZi(int utilizatorId, DateTime luna)
        {
            List<SumaIncasataPeZi> result = new List<SumaIncasataPeZi>();

            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetSumeIncasatePeZi", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_UtilizatorId", utilizatorId);
                        cmd.Parameters.AddWithValue("@p_Luna", luna);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SumaIncasataPeZi sumaIncasataPeZi = new SumaIncasataPeZi
                                {
                                    Zi = reader.GetInt32("Zi"),
                                    SumaIncasata = reader.GetDecimal("SumaIncasata")
                                };
                                result.Add(sumaIncasataPeZi);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the daily collected amounts: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }






    }
}

public class SumaIncasataPeZi
{
    public int Zi { get; set; }
    public decimal SumaIncasata { get; set; }
}

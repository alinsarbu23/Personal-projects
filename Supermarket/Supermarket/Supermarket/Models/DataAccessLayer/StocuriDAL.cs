using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Supermarket.Models.BussinessLogicLayer;
using System.Windows;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.DataAccessLayer
{
    internal class StocuriDAL
    {
        public void AddStoc(Stocuri stoc)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("AddStoc", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", stoc.ProductId);
                        cmd.Parameters.AddWithValue("@Cantitate", stoc.Cantitate);
                        cmd.Parameters.AddWithValue("@UnitateMasura", stoc.UnitateMasura);
                        cmd.Parameters.AddWithValue("@DataAprovizionare", stoc.DataAprovizionare);
                        cmd.Parameters.AddWithValue("@DataExpirare", stoc.DataExpirare);
                        cmd.Parameters.AddWithValue("@PretAchizitie", stoc.PretAchizitie);
                        cmd.Parameters.AddWithValue("@PretVanzare", stoc.PretVanzare);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding stock: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteStoc(int stocId)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("DeleteStoc", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_StocId", stocId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting stock: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Stocuri> GetAllStocuri()
        {
            List<Stocuri> stocuri = new List<Stocuri>();
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetStocuri", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Stocuri stoc = new Stocuri();
                                stoc.StocId = Convert.ToInt32(reader["StocId"]);
                                stoc.ProductId = Convert.ToInt32(reader["ProductId"]);
                                stoc.Cantitate = Convert.ToInt32(reader["Cantitate"]);
                                stoc.UnitateMasura = reader["UnitateMasura"].ToString();

                                // Check for DBNull before converting
                                if (reader["DataAprovizionare"] != DBNull.Value)
                                    stoc.DataAprovizionare = Convert.ToDateTime(reader["DataAprovizionare"]);

                                if (reader["DataExpirare"] != DBNull.Value)
                                    stoc.DataExpirare = Convert.ToDateTime(reader["DataExpirare"]);

                                if (reader["PretAchizitie"] != DBNull.Value)
                                    stoc.PretAchizitie = Convert.ToDecimal(reader["PretAchizitie"]);

                                if (reader["PretVanzare"] != DBNull.Value)
                                    stoc.PretVanzare = Convert.ToDecimal(reader["PretVanzare"]);

                                stocuri.Add(stoc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving stocks: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return stocuri;
        }


        public void UpdateStoc(Stocuri stoc)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdateStoc", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pStocId", stoc.StocId);
                        cmd.Parameters.AddWithValue("@pProduct", stoc.ProductId);
                        cmd.Parameters.AddWithValue("@pCantitate", stoc.Cantitate);
                        cmd.Parameters.AddWithValue("@pUnitateMasura", stoc.UnitateMasura);
                        cmd.Parameters.AddWithValue("@pDataAprovizionare", stoc.DataAprovizionare);
                        cmd.Parameters.AddWithValue("@pDataExpirare", stoc.DataExpirare);
                        cmd.Parameters.AddWithValue("@PretAchizitie", stoc.PretAchizitie);
                        cmd.Parameters.AddWithValue("@pPretVanzare", stoc.PretVanzare);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating stock: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

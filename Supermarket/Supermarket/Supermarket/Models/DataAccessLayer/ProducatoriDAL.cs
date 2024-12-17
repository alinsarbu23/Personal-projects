using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Supermarket.Models.BussinessLogicLayer;
using System.Windows;
using Supermarket.Models.DataAccessLayer;

namespace Supermarket.Models.DataAccessLayer
{
    internal class ProducatoriDAL
    {
        public void AddProducator(Producatori producator)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("AddProducator", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NumeProducator", producator.NumeProducator);
                        cmd.Parameters.AddWithValue("@TaraOrigine", producator.TaraOrigine);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding a producer: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteProducator(int producatorId)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("DeleteProducator", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ProducatorId", producatorId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting a producer: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Producatori> GetAllProducatori()
        {
            List<Producatori> producatori = new List<Producatori>();
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetProducatori", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producatori producator = new Producatori
                                {
                                    ProducatorId = Convert.ToInt32(reader["ProducatorId"]),
                                    NumeProducator = reader["NumeProducator"].ToString(),
                                    TaraOrigine = reader["TaraOrigine"].ToString()
                                };
                                producatori.Add(producator);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving producers: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return producatori;
        }




        public void UpdateProducator(Producatori producator)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdateProducator", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pProducatorId", producator.ProducatorId);
                        cmd.Parameters.AddWithValue("@pNumeProducator", producator.NumeProducator);
                        cmd.Parameters.AddWithValue("@pTaraOrigine", producator.TaraOrigine);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating a producer: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

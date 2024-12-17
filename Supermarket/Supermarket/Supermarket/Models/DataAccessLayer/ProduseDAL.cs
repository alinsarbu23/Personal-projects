using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using Supermarket.Models.BussinessLogicLayer;
using System.Data;

namespace Supermarket.Models.DataAccessLayer
{
    internal class ProduseDAL
    {
        public void AddProdus(Produse produs)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("AddProdus", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NumeProdus", produs.NumeProdus);
                        cmd.Parameters.AddWithValue("@CodBare", produs.CodBare);
                        cmd.Parameters.AddWithValue("@CategorieId", produs.CategorieId);
                        cmd.Parameters.AddWithValue("@ProducatorId", produs.ProducatorId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding a product: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteProdus(int produsId)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("DeleteProdus", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_ProdusId", produsId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting a product: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Produse> GetAllProduse()
        {
            List<Produse> produse = new List<Produse>();
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetProduse", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Produse produs = new Produse();
                                produs.ProductId = Convert.ToInt32(reader["ProductId"]);
                                produs.NumeProdus = reader["NumeProdus"].ToString();
                                produs.CodBare = reader["CodBare"].ToString();
                                produs.CategorieId = Convert.ToInt32(reader["CategorieId"]);
                                produs.ProducatorId = Convert.ToInt32(reader["ProducatorId"]);
                                produse.Add(produs);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving products: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return produse;
        }

        public void UpdateProdus(Produse produs)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdateProdus", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pProductId", produs.ProductId);
                        cmd.Parameters.AddWithValue("@pNumeProdus", produs.NumeProdus);
                        cmd.Parameters.AddWithValue("@pCodBare", produs.CodBare);
                        cmd.Parameters.AddWithValue("@pCategorieId", produs.CategorieId);
                        cmd.Parameters.AddWithValue("@pProducatorId", produs.ProducatorId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating a product: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public List<ProdusDetaliat> SearchProductsDetailed(string searchTerm)
        {
            List<ProdusDetaliat> produseDetaliat = new List<ProdusDetaliat>();

            MySqlConnection con = null;
            MySqlDataReader reader = null;

            try
            {
                con = DatabaseConnection.GetConnection();
                using (MySqlCommand cmd = new MySqlCommand("SearchProductsDetailed", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProdusDetaliat produsDetaliat = new ProdusDetaliat();
                        produsDetaliat.NumeProdus = reader["NumeProdus"].ToString();
                        produsDetaliat.CodBare = reader["CodBare"].ToString();
                        produsDetaliat.DataExpirare = Convert.ToDateTime(reader["DataExpirare"]);
                        produsDetaliat.NumeProducator = reader["NumeProducator"].ToString();
                        produsDetaliat.NumeCategorie = reader["NumeCategorie"].ToString();

                        produseDetaliat.Add(produsDetaliat);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching for products: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Închideți conexiunea și cititorul de date
                reader?.Close();
                con?.Close();
            }

            return produseDetaliat;
        }






    }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Supermarket.Models.BussinessLogicLayer;
using System.Windows;

namespace Supermarket.Models.DataAccessLayer
{
    internal class CategoriiDAL
    {
        public void AddCategorieProdus(Categorii categorie)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("AddCategorieProdus", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NumeCategorie", categorie.NumeCategorie);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding a category: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void DeleteCategorieProdus(int categorieId)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("DeleteCategorieProdus", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_CategorieId", categorieId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting a category: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Categorii> GetCategoriiProduse()
        {
            List<Categorii> categorii = new List<Categorii>();
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetCategoriiProduse", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetBoolean("IsActive")) 
                                {
                                    Categorii categorie = new Categorii
                                    {
                                        CategorieId = Convert.ToInt32(reader["CategorieId"]),
                                        NumeCategorie = reader["NumeCategorie"].ToString()
                                    };
                                    categorii.Add(categorie);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving categories: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return categorii;
        }


        public void UpdateCategorieProdus(Categorii categorie)
        {
            try
            {
                using (MySqlConnection con = DatabaseConnection.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdateCategorieProdus", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pCategorieId", categorie.CategorieId);
                        cmd.Parameters.AddWithValue("@pNumeCategorie", categorie.NumeCategorie);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating a category: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

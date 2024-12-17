using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Supermarket.Models.DataAccessLayer
{
    public class ProductByCategoryDAL
    {
        public List<ProductByCategory> GetSumPricesByCategory()
        {
            List<ProductByCategory> productsByCategory = new List<ProductByCategory>();

            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "GetSumPricesByCategory";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductByCategory product = new ProductByCategory
                        {
                            NumeCategorie = reader["NumeCategorie"].ToString(),
                            SumaPreturilor = Convert.ToDecimal(reader["SumaPreturilor"])
                        };

                        productsByCategory.Add(product);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return productsByCategory;
        }
    }
}

using Assignment2.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        #region Create

        /// <summary>
        /// Adds a Customer to the database.
        /// </summary>
        /// <param name="entity">A Customer object to be added to the database.</param>
        /// <returns>A boolean indicating success.</returns>
        public bool Add(Customer entity)
        {
            string sql = "INSERT  INTO Customer (FirstName, LastName, Country, PostalCode, Phone, Email) VALUES (@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                        command.Parameters.AddWithValue("@LastName", entity.LastName);
                        command.Parameters.AddWithValue("@Country", entity.Country);
                        command.Parameters.AddWithValue("@PostalCode", entity.PostalCode);
                        command.Parameters.AddWithValue("@Phone", entity.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", entity.Email);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        #endregion

        #region Read
        
        /// <summary>
        /// Gets a list of all customers from the database.
        /// </summary>
        /// <returns>A list of Customer objects.</returns>
        public List<Customer> GetAll()
        {
            List<Customer> customerList = new List<Customer>();

            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer()
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    // If table values are NULL, we substitute the string "NULL"
                                    Country = reader.IsDBNull(3) ? "NULL" : reader.GetString(3),
                                    PostalCode = reader.IsDBNull(4) ? "NULL" : reader.GetString(4),
                                    PhoneNumber = reader.IsDBNull(5) ? "NULL" : reader.GetString(5),
                                    Email = reader.GetString(6)
                                };

                                customerList.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }
            return customerList;
        }
        
        /// <summary>
        /// Get a subset of customers from the database by ID.
        /// </summary>
        /// <param name="limit">An integer representing the number of customers to be returned.</param>
        /// <param name="offset">An integer representing the start of the list.</param>
        /// <returns>A list of Customer objects.</returns>
        public List<Customer> GetPage(int limit, int offset = 0)
        {
            List<Customer> customerList = new List<Customer>();

            string sql = "SELECT TOP (@Limit) CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId > @Offset";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Limit", limit);
                        command.Parameters.AddWithValue("@Offset", offset);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer()
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    // If table values are NULL, we substitute the string "NULL"
                                    Country = reader.IsDBNull(3) ? "NULL" : reader.GetString(3),
                                    PostalCode = reader.IsDBNull(4) ? "NULL" : reader.GetString(4),
                                    PhoneNumber = reader.IsDBNull(5) ? "NULL" : reader.GetString(5),
                                    Email = reader.GetString(6)
                                };

                                customerList.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }
            return customerList;
        }
        
        /// <summary>
        /// Get a single customer from the database by ID.
        /// </summary>
        /// <param name="id">An integer representing the ID of the customer to be returned.</param>
        /// <returns>A customer object.</returns>
        public Customer GetByID(int id)
        {
            Customer customer = new Customer();

            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @customerId";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@customerId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();

                            customer.Id = reader.GetInt32(0);
                            customer.FirstName = reader.GetString(1);
                            customer.LastName = reader.GetString(2);
                            // If table values are NULL, we substitute the string "NULL"
                            customer.Country = reader.IsDBNull(3) ? "NULL" : reader.GetString(3);
                            customer.PostalCode = reader.IsDBNull(4) ? "NULL" : reader.GetString(4);
                            customer.PhoneNumber = reader.IsDBNull(5) ? "NULL" : reader.GetString(5);
                            customer.Email = reader.GetString(6);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }
            return customer;
        }

        /// <summary>
        /// Get a customer from the database by name.
        /// </summary>
        /// <param name="name">A string representing the name of the customer to be returned.
        ///     If a full name is given, the two parts are separated and searched separately.</param>
        /// <returns>A list of customers whose first or last name contains the first or last part of the string name.</returns>
        public List<Customer> GetByName(string name)
        {
            List<Customer> customerList = new List<Customer>();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE FirstName LIKE @FirstName OR LastName LIKE @LastName";

            string firstName;
            string lastName;

            // If a full name is given, the first and last name are separated.
            if (name.Contains(' ')) 
            {
                string[] nameSegments = name.Split(' ');
                firstName = nameSegments[0];
                lastName = nameSegments[nameSegments.Length - 1];
            } else
            {
                firstName = name;
                lastName = name;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", $"%{firstName}%");
                        command.Parameters.AddWithValue("@LastName", $"%{lastName}%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer()
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    // If table values are NULL, we substitute the string "NULL"
                                    Country = reader.IsDBNull(3) ? "NULL" : reader.GetString(3),
                                    PostalCode = reader.IsDBNull(4) ? "NULL" : reader.GetString(4),
                                    PhoneNumber = reader.IsDBNull(5) ? "NULL" : reader.GetString(5),
                                    Email = reader.GetString(6)
                                };

                                customerList.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }
            return customerList;
        }

        /// <summary>
        /// Get a list of countries and the number of customers living in those countries in descending order of number of customers.
        /// </summary>
        /// <returns>A list of CustomerCountry objects, representing the countries and the number of customers living there.</returns>
        public List<CustomerCountry> GetCountryCount()
        {
            List<CustomerCountry> countryList = new List<CustomerCountry>();

            string sql = "SELECT Country, COUNT(Country) FROM Customer GROUP BY Country ORDER BY Count(Country) DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerCountry country = new CustomerCountry()
                                {
                                    Name = reader.GetString(0),
                                    Count = reader.GetInt32(1)
                                };

                                countryList.Add(country);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }

            return countryList;
        }

        /// <summary>
        /// Get a list of customers and the amount they've spent in descending order of the money customers have spent.
        /// </summary>
        /// <returns>A list of CustomerSpender objects, representing the customer and the amount they've spent.</returns>
        public List<CustomerSpender> GetCustomerSpenders()
        {
            List<CustomerSpender> spenderList = new List<CustomerSpender>();

            string sql = "SELECT DISTINCT Customer.CustomerId, (SELECT SUM(Total) From Invoice WHERE Invoice.CustomerId = Customer.CustomerId) AS Total FROM Customer ORDER BY Total DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerSpender spender = new CustomerSpender()
                                {
                                    Customer = GetByID(reader.GetInt32(0)),
                                    Total = (double)reader.GetDecimal(1)
                                };

                                spenderList.Add(spender);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }

            return spenderList;
        }

        /// <summary>
        /// Get a list of genres that are all tied for the most popular genre a customer has listened to.
        /// </summary>
        /// <param name="id">An integer representing the ID of the customer for which to find their most popular genres.</param>
        /// <returns>A list of CustomerGenre objects, representing the customer, the name of the genre and the amount of time they've listened to that genre.</returns>
        public List<CustomerGenre> GetMostPopularGenres(int id)
        {
            List<CustomerGenre> genreList = new List<CustomerGenre>();

            string sql = "SELECT Genre.Name, SUM(InvoiceLine.Quantity) AS Count FROM Genre " +
                            "INNER JOIN Track ON Genre.GenreId = Track.GenreId " + 
                            "INNER JOIN InvoiceLine ON InvoiceLine.TrackId = Track.TrackId " + 
                            "INNER JOIN Invoice ON InvoiceLine.InvoiceId = Invoice.InvoiceId " + 
                            "WHERE Invoice.CustomerId = @CustomerId GROUP BY Genre.Name ORDER BY Count DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            int maxCount = reader.GetInt32(1);
                            do
                            {
                                CustomerGenre genre = new CustomerGenre()
                                {
                                    Customer = GetByID(id),
                                    Genre = reader.GetString(0),
                                    Count = maxCount
                                };

                                genreList.Add(genre);
                                reader.Read();
                            } while (reader.GetInt32(1) == maxCount);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }

            return genreList;
        }

        #endregion

        #region Update

        /// <summary>
        /// Update an existing customer in the database.
        /// </summary>
        /// <param name="entity">A customer object representing the updated information.</param>
        /// <param name="id">An integer representing the ID of the customer to be updated.</param>
        /// <returns>A boolean indicating success.</returns>
        public bool Edit(Customer entity, int id)
        {
            string sql = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Country = @Country, PostalCode = @PostalCode, Phone = @Phone, Email = @Email WHERE CustomerId = @CustomerId";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", id);
                        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                        command.Parameters.AddWithValue("@LastName", entity.LastName);
                        command.Parameters.AddWithValue("@Country", entity.Country);
                        command.Parameters.AddWithValue("@PostalCode", entity.PostalCode);
                        command.Parameters.AddWithValue("@Phone", entity.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", entity.Email);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete a customer from the database by ID.
        /// </summary>
        /// <param name="id">An integer representing the ID of the customer to be deleted.</param>
        /// <returns>A boolean indicating success.</returns>
        public bool Delete(int id)
        {
            string sql = "DELETE FROM Customer WHERE CustomerId = @CustomerId";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", id);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with SQL");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something general went wrong");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        #endregion
    }
}

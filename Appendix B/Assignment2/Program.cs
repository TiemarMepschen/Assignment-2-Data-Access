using System;
using System.Collections.Generic;
using Assignment2.Models;
using Assignment2.Repositories;
using Microsoft.Data.SqlClient;

namespace Assignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICustomerRepository repository = new CustomerRepository();

            #region part 1.

            printAllCustomers(repository.GetAll());

            #endregion

            #region part 2.

            //printCustomer(repository.GetByID(33));

            #endregion

            #region part 3.

            //printAllCustomers(repository.GetByName("Enrique"));

            //printAllCustomers(repository.GetByName("Taylor"));

            //printAllCustomers(repository.GetByName("Luis Gonçalves"));

            #endregion

            #region part 4.

            //printAllCustomers(repository.GetPage(5));

            //printAllCustomers(repository.GetPage(6, 9));

            #endregion

            #region part 5.

            Customer tiemar = new Customer()
            {
                FirstName = "Tiemar",
                LastName = "Mepschen",
                Country = "The Netherlands",
                PostalCode = "1337 FU",
                Email = "TiemarMepschen@fakeemail.com",
                PhoneNumber = "+31 415926535"
            };

            //Console.WriteLine(repository.Add(tiemar));

            #endregion

            #region part 6.

            tiemar.Id = 60;
            tiemar.FirstName = "Mehmet";
            tiemar.LastName = "Balci";

            //Console.WriteLine(repository.Edit(tiemar, tiemar.Id));

            //Console.WriteLine(repository.Delete(tiemar.Id));

            #endregion

            #region part 7.

            //printAllCountries(repository.GetCountryCount());

            #endregion

            #region part 8.

            //printAllSpenders(repository.GetCustomerSpenders());

            #endregion

            #region part 9.

            //printAllGenres(repository.GetMostPopularGenres(42));

            //printAllGenres(repository.GetMostPopularGenres(12));

            #endregion
        }

        /// <summary>
        /// Writes a single customer to the console.
        /// </summary>
        /// <param name="customer">The Customer object to be displayed to the console.</param>
        public static void printCustomer(Customer customer)
        {
            Console.WriteLine($"--- {customer.Id} {customer.FirstName} {customer.LastName} {customer.Country} {customer.PostalCode} {customer.PhoneNumber} {customer.Email} ---");
        }

        /// <summary>
        /// Writes a list of customers to the console.
        /// </summary>
        /// <param name="customers">The list of Customer objects to be displayed to the console.</param>
        public static void printAllCustomers(List<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                printCustomer(customer);
            }
        }

        /// <summary>
        /// Writes a country and its customer count to the console.
        /// </summary>
        /// <param name="country">The CustomerCountry object to be displayed to the console.</param>
        public static void printCountry(CustomerCountry country)
        {
            Console.WriteLine($"--- {country.Name} {country.Count} ---");
        }

        /// <summary>
        /// Writes a list of countries and customer counts to the console.
        /// </summary>
        /// <param name="countries">The list of CustomerCountry objects to be displayed to the console.</param>
        public static void printAllCountries(List<CustomerCountry> countries)
        {
            foreach (CustomerCountry country in countries)
            {
                printCountry(country);
            }
        }

        /// <summary>
        /// Writes a customer and how much they've spent in total to the console.
        /// </summary>
        /// <param name="spender">The CustomerSpender object to be displayed to the console.</param>
        public static void printSpender(CustomerSpender spender)
        {
            Console.WriteLine($"--- {spender.Customer.Id} {spender.Customer.FirstName} {spender.Customer.LastName} {spender.Customer.Country} {spender.Customer.PostalCode} {spender.Customer.PhoneNumber} {spender.Customer.Email} {spender.Total} ---");
        }

        /// <summary>
        /// Writes a list of customers and how much they've spent in total to the console.
        /// </summary>
        /// <param name="spenders">The list of CustomerSpender objects to be displayed to the console.</param>
        public static void printAllSpenders(List<CustomerSpender> spenders)
        {
            foreach (CustomerSpender spender in spenders)
            {
                printSpender(spender);
            }
        }

        /// <summary>
        /// Writes a customer, a genre and the amount the customer has listened to this genre to the console.
        /// </summary>
        /// <param name="genre">The CustomerGenre object to be displayed to the console.</param>
        public static void printGenre(CustomerGenre genre)
        {
            Console.WriteLine($"--- {genre.Customer.Id} {genre.Customer.FirstName} {genre.Customer.LastName} {genre.Customer.Country} {genre.Customer.PostalCode} {genre.Customer.PhoneNumber} {genre.Customer.Email} {genre.Genre} {genre.Count} ---");
        }

        /// <summary>
        /// Writes a list of customers, genres and the amount the customer has listened to that genre to the console.
        /// </summary>
        /// <param name="genres">The list of CustomerGenre objects to be displayed to the console.</param>
        public static void printAllGenres(List<CustomerGenre> genres)
        {
            foreach (CustomerGenre genre in genres)
            {
                printGenre(genre);
            }
        }
    }
}

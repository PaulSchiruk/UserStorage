using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        /// <summary>
        /// Users set
        /// </summary>
        private readonly HashSet<User> users;

        /// <summary>
        /// 
        /// </summary>
        public UserStorageService()
        {
            users = new HashSet<User>();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        private bool IsLoggingEnabled { get; set; }        

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("LastName is null or empty or whitespace", nameof(user));
            }

            if (user.Age < 3 || user.Age > 120)
            {
                throw new ArgumentException("Age doesn't make sense", nameof(user));
            }

            users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove()
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            users.Clear();
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> Search(Predicate<User> comparer)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Search() method is called.");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return users.Select(x => x).Where(x => comparer(x));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByFirstName(string firstName) method is called.");
            }

            if (firstName == null)
            {
                throw new ArgumentNullException("FirstName invalid");
            }

            return Search(x => x.FirstName == firstName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByLactName(string lastName) method is called.");
            }

            if (lastName == null)
            {
                throw new ArgumentNullException("LastName invalid");
            }

            return Search(x => x.LastName == lastName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public IEnumerable<User> SearchByAge(int age)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByAge(int age) method is called.");
            }

            if (age < 3 || age > 120)
            {
                throw new ArgumentException("Age invalid");
            }

            return Search(x => x.Age == age);
        }
    }
}

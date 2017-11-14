﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Exceptions;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        /// <summary>
        /// Users set
        /// </summary>
        private readonly HashSet<User> users;

        /// <summary>
        /// 
        /// </summary>
        private readonly IUserValidate userValidate;

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
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            userValidate.Validate(user);

            users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public bool Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return users.Remove(user);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> Search(Predicate<User> comparer)
        {
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
            if (firstName == null)
            {
                throw new FirstNameIsNullOrEmptyException("FirstName invalid");
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
            if (lastName == null)
            {
                throw new LastNameIsNullOrEmptyException("LastName invalid");
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
            if (age < 3 || age > 120)
            {
                throw new AgeExceedsLimitsException("Age invalid");
            }

            return Search(x => x.Age == age);
        }

        public IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty");
            }

            return Search(x => x.FirstName == firstName && x.LastName == lastName);
        }

        public IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty");
            }

            if (age < 3 || age > 120)
            {
                throw new AgeExceedsLimitsException("Age invalid");
            }

            return Search(x => x.FirstName == firstName && x.Age == age);
        }

        public IEnumerable<User> SearchByLastNameAndAge(string lastName, int age)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty");
            }

            if (age < 3 || age > 120)
            {
                throw new AgeExceedsLimitsException("Age invalid");
            }

            return Search(x => x.LastName == lastName && x.Age == age);
        }

        public IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty");
            }

            if (age < 3 || age > 120)
            {
                throw new AgeExceedsLimitsException("Age invalid");
            }

            return Search(x => x.FirstName == firstName && x.LastName == lastName && x.Age == age);
        }
    }
}

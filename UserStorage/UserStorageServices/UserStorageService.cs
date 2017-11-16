using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Exceptions;
using UserStorageServices.Validation;

namespace UserStorageServices
{
    public enum UserStorageServiceMode
    {
        MasterNode,
        SlaveNode
    }

    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public abstract class UserStorageServiceBase : Switch, IUserStorageService, INotificationSubscriber
    {
        /// <summary>
        /// Users set
        /// </summary>
        private readonly HashSet<User> users;

        /// <summary>
        /// 
        /// </summary>
        private readonly IUserValidate<User> userValidate;

        protected UserStorageServiceBase(IUserValidate<User> userValidate) : base("enableLogging", "If logging enabled")
        {
            this.userValidate = userValidate;

            if (this.userValidate == null)
            {
                this.userValidate = new CompositeValidation();
            }

            this.users = new HashSet<User>();
        }

        protected UserStorageServiceBase(IEnumerable<User> users, IUserValidate<User> validator = null) : this(validator)
        {
            foreach (User u in users)
            {
                this.Add(u);
            }
        }

        protected UserStorageServiceBase(IUserValidate<User> validator = null, params User[] users) : this(validator)
        {
            foreach (User u in users)
            {
                this.Add(u);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count { get; }

        public abstract UserStorageServiceMode ServiceMode { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            this.userValidate.Validate(user);

            this.users.Add(user);
        }

        public void UserAdded(User user)
        {
            Trace.Write("For Subscriber : User added");
        }

        public void UserRemoved(User user)
        {
            Trace.Write("For Subscriber : User removed");
        }      

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual void Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException($"User {nameof(user)} is not defined");
            }

            if (!this.Contains(user))
            {
                throw new ArgumentException("No user with such Id was found");
            }

            this.users.Remove(user);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public virtual IEnumerable<User> Search(Predicate<User> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return this.Search(comparer);            
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

            return this.Search(x => x.FirstName == firstName);
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

            return this.Search(x => x.LastName == lastName);
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

            return this.Search(x => x.Age == age);
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

            return this.Search(x => x.FirstName == firstName && x.LastName == lastName);
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

            return this.Search(x => x.FirstName == firstName && x.Age == age);
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

            return this.Search(x => x.LastName == lastName && x.Age == age);
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

            return this.Search(x => x.FirstName == firstName && x.LastName == lastName && x.Age == age);
        }

        private bool Contains(User user) => this.users.Any(u => u.Id == user.Id);
    }
}

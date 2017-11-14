using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Exceptions;

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
    public class UserStorageService : IUserStorageService
    {
        private readonly UserStorageServiceMode mode;

        /// <summary>
        /// Users set
        /// </summary>
        private readonly HashSet<User> users;

        /// <summary>
        /// 
        /// </summary>
        private readonly IUserValidate userValidate;

        private List<IUserStorageService> slaveServices = new List<IUserStorageService>();

        private List<INotificationSubscriber> subscribers = new List<INotificationSubscriber>();

        public UserStorageService(UserStorageServiceMode mode, IEnumerable<IUserStorageService> slaves = null) : this()
        {
            this.mode = mode;

            if (mode == UserStorageServiceMode.MasterNode && slaves != null)
            {
                subscribers = new List<INotificationSubscriber>();

                slaveServices = new List<IUserStorageService>();

                this.slaveServices = slaves.ToList();

                foreach (var sub in slaves)
                {
                    subscribers.Add((INotificationSubscriber)sub);
                }
            }
        }

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
            if (!OperationAllowed())
            {
                throw new NotSupportedException();
            }

            userValidate.Validate(user);

            if (mode == UserStorageServiceMode.MasterNode && slaveServices != null)
            {
                foreach (var service in slaveServices)
                {
                    service.Add(user);
                }
            }
            else
            {
                this.users.Add(user);
            }

            if (mode == UserStorageServiceMode.MasterNode)
            {
                foreach (var sub in subscribers)
                {
                    sub.UserAdded(user);
                }
            }
        }

        public void UserAdded(User user)
        {
            Trace.Write("For Subscriber : User added");
        }

        public void UserRemoved(User user)
        {

            Trace.Write("For Subscriber : User removed");
        }

        public void AddSubscriber(INotificationSubscriber sub)
        {
            if (mode == UserStorageServiceMode.SlaveNode) return;
            if (sub == null) throw new ArgumentNullException($"{nameof(sub)} is null");
            subscribers.Add(sub);
        }

        public void RemoveSubscriber(INotificationSubscriber sub)
        {
            if (mode == UserStorageServiceMode.SlaveNode) return;
            if (sub == null) throw new ArgumentNullException($"{nameof(sub)} is null");
            if (!subscribers.Contains(sub)) throw new InvalidOperationException("No such subscruber was found");
            subscribers.Remove(sub);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!OperationAllowed())
            {
                throw new NotSupportedException();
            }

            if (mode == UserStorageServiceMode.MasterNode)
            {
                foreach (var service in slaveServices)
                {
                    service.Remove(user);
                }
            }
            else
            {
                if (!this.users.Remove(user))
                {
                    throw new ArgumentNullException("No user with such Id was found");
                }
            }

            if (mode == UserStorageServiceMode.MasterNode)
            {
                foreach (var sub in subscribers)
                {
                    sub.UserRemoved(user);
                }
            }
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

            if (mode == UserStorageServiceMode.SlaveNode)
            {
                return this.Search(comparer);
            }
            else
            {
                List<User> result = new List<User>();
                foreach (var service in slaveServices)
                {
                    if (service.Search(comparer) != null)
                    {
                        result.AddRange(service.Search(comparer));
                    }
                }

                return result;
            }
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

        private bool OperationAllowed()
        {
            StackTrace stack = new StackTrace();
            var currentMethod = stack.GetFrame(1).GetMethod();
            var stackFramesContainsCurrentMethod = stack.GetFrames();
            var counterOfSameFrames = 0;
            foreach (var frame in stackFramesContainsCurrentMethod)
            {
                if (frame.GetMethod() == currentMethod)
                {
                    counterOfSameFrames++;
                }

                if (counterOfSameFrames >= 2)
                {
                    break;
                }
            }

            return mode == UserStorageServiceMode.MasterNode || counterOfSameFrames >= 2;
        }
    }
}

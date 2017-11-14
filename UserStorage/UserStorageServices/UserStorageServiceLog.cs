using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        private readonly BooleanSwitch logging = new BooleanSwitch("enableLogging", "switch in app.config");

        public UserStorageServiceLog(IUserStorageService storageService) : base(storageService) { }

        public override int Count
        {
            get
            {
                if (logging.Enabled)
                {
                    Console.WriteLine("Count() method is called.");
                }

                return StorageService.Count;
            }
        }

        public override void Add(User user)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            StorageService.Add(user);
        }
        public override bool Remove(User user)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            return StorageService.Remove(user);
        }
        public override IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByFirstName() method is called.");
            }

            return StorageService.SearchByFirstName(firstName);
        }
        public override IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByFirstNameAndLastName() method is called.");
            }

            return StorageService.SearchByFirstNameAndLastName(firstName, lastName);
        }
        public override IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByFirstNameAndAge() method is called.");
            }

            return StorageService.SearchByFirstNameAndAge(firstName, age);
        }
        public override IEnumerable<User> SearchByLastName(string lastName)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByLastName() method is called.");
            }

            return StorageService.SearchByLastName(lastName);
        }
        public override IEnumerable<User> SearchByLastNameAndAge(string lastName, int age)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByLastNameAndAge() method is called.");
            }

            return StorageService.SearchByLastNameAndAge(lastName, age);
        }
        public override IEnumerable<User> SearchByAge(int age)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByAge() method is called.");
            }

            return StorageService.SearchByAge(age);
        }
        public override IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByFirstNameAndLastNameAndAge() method is called.");
            }

            return StorageService.SearchByFirstNameAndLastNameAndAge(firstName, lastName, age);
        }

        public override IEnumerable<User> Search(Predicate<User> comparer)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByFirstNameAndLastNameAndAge() method is called.");
            }

            return StorageService.Search(comparer);
        }
    }
}

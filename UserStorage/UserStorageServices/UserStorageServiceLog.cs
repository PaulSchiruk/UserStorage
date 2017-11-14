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

        public UserStorageServiceLog(IUserStorageService storageService) : base(storageService)
        {
        }

        public override int Count
        {
            get
            {
                if (this.logging.Enabled)
                {
                    Trace.WriteLine("Count() method is called.");
                }

                return StorageService.Count;
            }
        }

        public override void Add(User user)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("Add() method is called.");
            }

            StorageService.Add(user);
        }

        public override bool Remove(User user)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("Remove() method is called.");
            }

            return StorageService.Remove(user);
        }

        public override IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstName() method is called.");
            }

            return StorageService.SearchByFirstName(firstName);
        }

        public override IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstNameAndLastName() method is called.");
            }

            return StorageService.SearchByFirstNameAndLastName(firstName, lastName);
        }

        public override IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstNameAndAge() method is called.");
            }

            return StorageService.SearchByFirstNameAndAge(firstName, age);
        }

        public override IEnumerable<User> SearchByLastName(string lastName)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByLastName() method is called.");
            }

            return StorageService.SearchByLastName(lastName);
        }

        public override IEnumerable<User> SearchByLastNameAndAge(string lastName, int age)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByLastNameAndAge() method is called.");
            }

            return StorageService.SearchByLastNameAndAge(lastName, age);
        }

        public override IEnumerable<User> SearchByAge(int age)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByAge() method is called.");
            }

            return StorageService.SearchByAge(age);
        }

        public override IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstNameAndLastNameAndAge() method is called.");
            }

            return StorageService.SearchByFirstNameAndLastNameAndAge(firstName, lastName, age);
        }

        public override IEnumerable<User> Search(Predicate<User> comparer)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstNameAndLastNameAndAge() method is called.");
            }

            return StorageService.Search(comparer);
        }
    }
}

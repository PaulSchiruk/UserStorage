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

        public override void Remove(User user)
        {
            if (this.logging.Enabled)
            {
                Trace.WriteLine("Remove() method is called.");
            }

            StorageService.Remove(user);
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

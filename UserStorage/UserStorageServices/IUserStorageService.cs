using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserStorageService
    {
        int Count { get; }

        void Add(User user);

        void Remove(User user);

        IEnumerable<User> SearchByFirstName(string firstName);

        IEnumerable<User> SearchByLastName(string lastName);

        IEnumerable<User> SearchByAge(int age);

        IEnumerable<User> Search(Predicate<User> comparer);

        IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName);

        IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age);

        IEnumerable<User> SearchByLastNameAndAge(string lastName, int age);

        IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age);
    }
}

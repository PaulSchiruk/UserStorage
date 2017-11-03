using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            
            // Act
            userStorageService.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }
        
        [TestMethod]
        public void SearchByFirstName_Positive()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            userStorageService.Add(new User() { Age = 25, LastName = "Black", FirstName = "Alex" });

            // Act 
            IEnumerable<User> result = userStorageService.SearchByFirstName("Alex");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void SearchByLastName_Positive()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            userStorageService.Add(new User() { Age = 25, LastName = "Black", FirstName = "Alex" });

            // Act 
            var result = userStorageService.SearchByLastName("Black");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void SearchByAge_Positive()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            userStorageService.Add(new User() { Age = 25, LastName = "Pupkin", FirstName = "Vasya" });

            // Act 
            var result = userStorageService.SearchByAge(25);

            // Assert
            Assert.AreEqual(1, result.Count());
        }
    }
}

using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Exceptions;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        #region Add
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
        [ExpectedException(typeof(FirstNameIsNullOrEmptyException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = null,
                Age = 5
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void Add_UserAgeIs0_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Hello",
                LastName = "World",
                Age = 0
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_SimpleUser_Added()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User { FirstName = "Hello", LastName = "World", Age = 14 });

            // Assert 
            Assert.AreEqual(1, userStorageService.Count);
        }
        #endregion

        #region Remove
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_UserExists_Removed()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            userStorageService.Add(user);

            // Act
            userStorageService.Remove(user);

            // Assert 
            Assert.AreEqual(0, userStorageService.Count);
        }

        [TestMethod]
        public void Remove_UserNotExists_NotRemoved()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user1 = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            var user2 = new User { FirstName = "Hello", LastName = "Eld", Age = 88 };
            userStorageService.Add(user1);

            // Act
            userStorageService.Remove(user2);

            // Assert 
            Assert.AreEqual(1, userStorageService.Count);
        }
        #endregion

        #region Search by FirstName 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_By_FirstName_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByFirstName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Search_By_FirstName_UserExists_Found()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            userStorageService.Add(user);

            // Act
            var res = userStorageService.SearchByFirstName("Hello");

            // Assert 
            Assert.AreEqual(1, res.Count());
        }

        [TestMethod]
        public void Search_By_FirstName_UserNotExists_NotFound()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            userStorageService.Add(user);

            // Act
            var res = userStorageService.SearchByFirstName("Hi");

            // Assert 
            Assert.AreEqual(0, res.Count());
        }
        #endregion

        #region Search by LastName 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_By_LastName_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByLastName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Search_By_LasttName_UserExists_Found()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            userStorageService.Add(user);

            // Act
            var res = userStorageService.SearchByLastName("World");

            // Assert 
            Assert.AreEqual(1, res.Count());
        }

        [TestMethod]
        public void Search_By_LastName_UserNotExists_NotFound()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            userStorageService.Add(user);

            // Act
            var res = userStorageService.SearchByLastName("Hi");

            // Assert 
            Assert.AreEqual(0, res.Count());
        }
        #endregion

        #region Search by FirstName 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_By_Age_AgeLessThen1_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByAge(-4);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Search_By_Age_UserExists_Found()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            userStorageService.Add(user);

            // Act
            var res = userStorageService.SearchByAge(14);

            // Assert 
            Assert.AreEqual(1, res.Count());
        }

        [TestMethod]
        public void Search_By_Age_UserNotExists_NotFound()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { FirstName = "Hello", LastName = "World", Age = 14 };
            userStorageService.Add(user);

            // Act
            var res = userStorageService.SearchByAge(88);

            // Assert 
            Assert.AreEqual(0, res.Count());
        }
        #endregion

    }
}
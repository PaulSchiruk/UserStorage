using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using UserStorageServices;
using UserStorageServices.Exceptions;

namespace UserStorageServices.Tests
{
    [TestFixture]
    public class UserStorageServiceTests
    {
        [TestCase]
        public void MasterMethodAdd_AnyUser_UserAddedToSlaveNodes()
        {
            User user = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            UserStorageServiceSlave slaveService1 = new UserStorageServiceSlave();
            UserStorageServiceSlave slaveService2 = new UserStorageServiceSlave();
            UserStorageServiceMaster masterService = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { slaveService1, slaveService2 }));

            masterService.Add(user);

            Assert.AreEqual(1, slaveService1.Count);
            Assert.AreEqual(1, slaveService2.Count);
        }

        [Test]
        public void SlaveMethodAdd_AnyUser_NotSupportedException()
        {
            User user = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            UserStorageServiceSlave slaveService1 = new UserStorageServiceSlave();

            Assert.Catch<NotSupportedException>(() => slaveService1.Add(user));
        }
    }
}
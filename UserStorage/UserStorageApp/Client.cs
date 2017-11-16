﻿using System.Collections.Generic;
using UserStorageServices;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageService"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService _userStorageService = null)
        {
            this._userStorageService = _userStorageService;
            if (_userStorageService == null)
            {
                _userStorageService = new UserStorageServiceLog(new UserStorageServiceMaster(null));
            }
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            _userStorageService.Add(new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            });

            UserStorageServiceMaster m = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave(), }));
            
            m.AddSubscriber(new UserStorageServiceSlave());
            m.Add(new User()
            {
                FirstName = "a",
                LastName = "b",
                Age = 55
            });
        }
    }
}

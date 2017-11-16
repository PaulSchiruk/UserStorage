using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Exceptions;

namespace UserStorageServices.Validation
{
    public class LastNameValidation : IUserValidate<User>
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }
        }
    }
}

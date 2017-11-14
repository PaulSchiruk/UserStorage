using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Exceptions;

namespace UserStorageServices.Validation
{
    public class FirstNameValidation : IUserValidate
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }
        }
    }
}

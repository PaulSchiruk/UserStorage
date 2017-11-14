using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Exceptions;

namespace UserStorageServices.Validation
{
    public class AgeValidation : IUserValidate
    {
        public void Validate(User user)
        {
            if (user.Age < 3 || user.Age > 120)
            {
                throw new AgeExceedsLimitsException("Age doesn't make sense");
            }
        }
    }
}

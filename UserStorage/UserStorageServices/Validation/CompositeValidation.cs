using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation
{
     public class CompositeValidation : IUserValidate
    {
        private readonly IUserValidate[] validators;

        public CompositeValidation()
        {
            validators = new IUserValidate[]
            {
                new AgeValidation(),
                new FirstNameValidation(),
                new LastNameValidation()
            };
        }

        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            foreach (var x in validators)
            {
                x.Validate(user);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation
{
     public class CompositeValidation : IUserValidate<User>
    {
        private readonly IUserValidate<User>[] validators;

        public CompositeValidation()
        {
            validators = new IUserValidate<User>[]
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

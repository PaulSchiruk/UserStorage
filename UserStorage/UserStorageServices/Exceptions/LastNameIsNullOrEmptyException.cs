﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Exceptions
{
    public class LastNameIsNullOrEmptyException : Exception
    {
        public LastNameIsNullOrEmptyException()
        {
        }

        public LastNameIsNullOrEmptyException(string message) : base(message)
        {
        }

        public LastNameIsNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LastNameIsNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

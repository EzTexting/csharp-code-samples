using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsTexting
{
    public class SmsTextingException :  Exception
    {
        public BaseObject details;

        private SmsTextingException(string message, BaseObject details) : base(message)
        {
            this.details = details;
        }

        public static SmsTextingException Build(BaseObject details)
        {
            String message = "";
            if (details != null && details.Errors != null)
            {
                message = details.Errors.Aggregate((current, next) => current + "; " + next);
            }
            return new SmsTextingException(message, details);
        }
    }
}

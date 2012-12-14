using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsTexting.ModelWrappers
{
    public class InboxMessagesWrapper : BaseObject
    {
        public List<InboxMessage> Entries { get; set; }
    }
}

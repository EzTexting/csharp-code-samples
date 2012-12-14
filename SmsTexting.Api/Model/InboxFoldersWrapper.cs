using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsTexting.ModelWrappers
{
    public class InboxFoldersWrapper : BaseObject
    {
        public List<InboxFolder> Entries { get; set; }
    }
}

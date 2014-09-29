using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class EmailInfo
    {
        #region Properties
        public string To { get; set; }
        public string FriendlyName { get; set; }
        public string Cc { get; set; }
        public string From { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string HTMLBody { get; set; }
        #endregion
    }
}

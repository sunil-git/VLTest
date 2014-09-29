using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class SmtpServerInfo
    {
        #region Properties
        public string SmtpHost { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        #endregion
    }
}

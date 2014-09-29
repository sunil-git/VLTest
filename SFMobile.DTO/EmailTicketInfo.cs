using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMobile.DTO
{
    public class EmailTicketInfo
    {
        #region Properties
        public string MessageFromName { get; set; }
        public string MessageFromEmail { get; set; }
        public string MessageTo { get; set; }
        public string OrderID { get; set; }
        public string SubjectStr { get; set; }
        public string EncryptedMessage { get; set; }
        public int ReasonID { get; set; }
        public int SourceID { get; set; }
        public int IdTicket { get; set; }
        public string EncryptionType { get; set; }
        public DateTime MessageDate { get; set; }
        #endregion
    }
}

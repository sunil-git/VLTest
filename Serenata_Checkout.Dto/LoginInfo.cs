using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class LoginInfo
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }
        public string Confirmpassword { get; set; }
        public string SocialUserID { get; set; }
        public string SocialMediaType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using System.Globalization;
using Serenata_Checkout.ExactTargetAPI.ExactTargetServiceReference;
using Serenata_Checkout.Log;
namespace Serenata_Checkout.ExactTargetAPI
{
    public class ExactTargetEmail
    {
         /// <summary>
        /// Default constructor for Pure360Logic
        /// <summary>
        public ExactTargetEmail()
        {  }

        public bool SendEmailUsingExactTarget(string email, string firstname, string password, string passwordlink, int SiteId)
        {
            bool Sent = false;
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Name = "UserNameSoapBinding";
                binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
                binding.OpenTimeout = new TimeSpan(0, 5, 0); binding.CloseTimeout = new TimeSpan(0, 5, 0);
                binding.SendTimeout = new TimeSpan(0, 5, 0);

                EndpointAddress endpoint = new EndpointAddress("https://webservice.s6.exacttarget.com/Service.asmx");

                SoapClient client = new SoapClient(binding, endpoint);
                client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetUserName"];
                client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetPassword"];

                TriggeredSendDefinition tsd = new TriggeredSendDefinition();
                tsd.CustomerKey = ConfigurationManager.AppSettings["PasswordReminderKey"];

                TriggeredSend ts = new TriggeredSend();
                ts.TriggeredSendDefinition = tsd;

                ts.Subscribers = new Subscriber[3];
                ts.Subscribers[0] = new Subscriber();
                ts.Subscribers[0].Owner = new Owner();
                ts.Subscribers[0].Owner.FromAddress = FromAddress(SiteId);//hardcoded for now
                ts.Subscribers[0].Owner.FromName = Brand(SiteId);//hardcoded for now
                ts.Subscribers[0].EmailAddress = email;
                ts.Subscribers[0].SubscriberKey = email;

                ts.Subscribers[0].Attributes = new ExactTargetServiceReference.Attribute[4];
                ts.Subscribers[0].Attributes[0] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[0].Name = "FirstName";
                ts.Subscribers[0].Attributes[0].Value = firstname;

                ts.Subscribers[0].Attributes[1] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[1].Name = "Password";
                ts.Subscribers[0].Attributes[1].Value = password;

                ts.Subscribers[0].Attributes[2] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[2].Name = "Brand";
                ts.Subscribers[0].Attributes[2].Value = Brand(SiteId);

                ts.Subscribers[0].Attributes[3] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[3].Name = "Resetpasswordlink";
                ts.Subscribers[0].Attributes[3].Value = passwordlink;


                string requestID, status;
                CreateResult[] results = client.Create(new CreateOptions(), new APIObject[] { ts }, out requestID, out status);
                if (status == "OK")
                {
                    Sent = true;
                }
                if (status == "Error")
                {
                    Sent = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
                Sent = false;
            }

            return Sent;
        }

        public bool SendEmailfromContactUSUsingExactTarget(string subkey,string email,string OrderID,string ContactName,string HTMLBody,int SiteID)
        { 
            bool Sent = false;
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Name = "UserNameSoapBinding";
                binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
                binding.OpenTimeout = new TimeSpan(0, 5, 0); binding.CloseTimeout = new TimeSpan(0, 5, 0);
                binding.SendTimeout = new TimeSpan(0, 5, 0);

                EndpointAddress endpoint = new EndpointAddress("https://webservice.s6.exacttarget.com/Service.asmx");

                SoapClient client = new SoapClient(binding, endpoint);
                client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetUserName"];
                client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetPassword"];

                TriggeredSendDefinition tsd = new TriggeredSendDefinition();
                tsd.CustomerKey = ConfigurationManager.AppSettings["ContactUsKey"];

                TriggeredSend ts = new TriggeredSend();
                ts.TriggeredSendDefinition = tsd;

                ts.Subscribers = new Subscriber[1];
                ts.Subscribers[0] = new Subscriber();
                ts.Subscribers[0].Owner = new Owner();
                ts.Subscribers[0].Owner.FromAddress = FromAddress(SiteID);
                ts.Subscribers[0].Owner.FromName =Brand(SiteID);
                ts.Subscribers[0].EmailAddress = email;
                ts.Subscribers[0].SubscriberKey = email;

                ts.Subscribers[0].Attributes = new ExactTargetServiceReference.Attribute[5];
                ts.Subscribers[0].Attributes[0] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[0].Name = "ContactName";
                ts.Subscribers[0].Attributes[0].Value = ContactName;

                ts.Subscribers[0].Attributes[1] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[1].Name = "OrderID";
                ts.Subscribers[0].Attributes[1].Value = OrderID.Replace("00000000","No Order ID Available");

                ts.Subscribers[0].Attributes[2] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[2].Name = "Brand";
                ts.Subscribers[0].Attributes[2].Value =Brand(SiteID);

                ts.Subscribers[0].Attributes[3] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[3].Name = "HTMLBody";
                ts.Subscribers[0].Attributes[3].Value = HTMLBody;

                ts.Subscribers[0].Attributes[4] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[4].Name = "FromAddress";
                ts.Subscribers[0].Attributes[4].Value = FromAddress(SiteID);


                string requestID, status;
                CreateResult[] results = client.Create(new CreateOptions(), new APIObject[] { ts }, out requestID, out status);
                if (status == "OK")
                {
                    Sent = true;
                }
                if (status == "Error")
                {
                    Sent = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
                Sent = false;
            }

            return Sent;


        }
        public bool SendVoucherEmailUsingExactTarget(string email, string firstname, string strVoucherCode, int SiteId)
        {
            bool Sent = false;
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Name = "UserNameSoapBinding";
                binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
                binding.OpenTimeout = new TimeSpan(0, 5, 0); binding.CloseTimeout = new TimeSpan(0, 5, 0);
                binding.SendTimeout = new TimeSpan(0, 5, 0);

                EndpointAddress endpoint = new EndpointAddress("https://webservice.s6.exacttarget.com/Service.asmx");

                SoapClient client = new SoapClient(binding, endpoint);
                client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetUserName"];
                client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetPassword"];

                TriggeredSendDefinition tsd = new TriggeredSendDefinition();
                tsd.CustomerKey = ConfigurationManager.AppSettings["PasswordReminderKey"];

                TriggeredSend ts = new TriggeredSend();
                ts.TriggeredSendDefinition = tsd;

                ts.Subscribers = new Subscriber[2];
                ts.Subscribers[0] = new Subscriber();
                ts.Subscribers[0].Owner = new Owner();
                ts.Subscribers[0].Owner.FromAddress = FromAddress(SiteId);//hardcoded for now
                ts.Subscribers[0].Owner.FromName = Brand(SiteId);//hardcoded for now
                ts.Subscribers[0].EmailAddress = email;
                ts.Subscribers[0].SubscriberKey = email;

                ts.Subscribers[0].Attributes = new ExactTargetServiceReference.Attribute[2];
                ts.Subscribers[0].Attributes[0] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[0].Name = "FirstName";
                ts.Subscribers[0].Attributes[0].Value = firstname;

                ts.Subscribers[0].Attributes[1] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[1].Name = "Password";
                ts.Subscribers[0].Attributes[1].Value = strVoucherCode;

                ts.Subscribers[0].Attributes[2] = new ExactTargetServiceReference.Attribute();
                ts.Subscribers[0].Attributes[2].Name = "Brand";
                ts.Subscribers[0].Attributes[2].Value = Brand(SiteId); ;

                string requestID, status;
                CreateResult[] results = client.Create(new CreateOptions(), new APIObject[] { ts }, out requestID, out status);
                if (status == "OK")
                {
                    Sent = true;
                }
                if (status == "Error")
                {
                    Sent = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
                Sent = false;
            }

            return Sent;
        }

        public string Brand(int intSiteId)
        {
            string brand = String.Empty;
            try
            {
                switch (intSiteId)
                {
                    case 1:
                        brand = ConfigurationManager.AppSettings["FlowersBrand"];
                        break;
                    case 3:
                        brand = ConfigurationManager.AppSettings["ChocolatesBrand"];
                        break;
                    case 4:
                        brand = ConfigurationManager.AppSettings["WinesBrand"];
                        break;
                    case 6:
                        brand = ConfigurationManager.AppSettings["HampersBrand"];
                        break;
                    default:
                        brand = ConfigurationManager.AppSettings["FlowersBrand"];
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return brand;
        }
        public string FromAddress(int intSiteId)
        {
            string FromAddress = String.Empty;
            try
            {
                switch (intSiteId)
                {
                    case 1:
                        FromAddress = ConfigurationManager.AppSettings["FromFlowersBrand"];
                        break;
                    case 3:
                        FromAddress = ConfigurationManager.AppSettings["FromChocolatesBrand"];
                        break;
                    case 4:
                        FromAddress = ConfigurationManager.AppSettings["FromWinesBrand"];
                        break;
                    case 6:
                        FromAddress = ConfigurationManager.AppSettings["FromHampersBrand"];
                        break;
                    default:
                        FromAddress = ConfigurationManager.AppSettings["FromFlowersBrand"];
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return FromAddress;
        }


        public void testRetrieveDataExtension()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "UserNameSoapBinding";
            binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            binding.OpenTimeout = new TimeSpan(0, 5, 0); binding.CloseTimeout = new TimeSpan(0, 5, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);

            EndpointAddress endpoint = new EndpointAddress("https://webservice.s6.exacttarget.com/Service.asmx");

            SoapClient client = new SoapClient(binding, endpoint);
            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetUserName"];
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetPassword"];











            APIObject[] results;
            // Create RetrieveRequest
            RetrieveRequest rr = new RetrieveRequest();
            rr.ObjectType = "DataExtension";
            rr.Properties =
                new string[]
                    {
                        "ObjectID", "CustomerKey", "Name", "IsSendable", "SendableSubscriberField.Name"
                    };
            SimpleFilterPart filter = new SimpleFilterPart();
            filter.Property = "CustomerKey";
            filter.SimpleOperator = SimpleOperators.equals;
            filter.Value = new string[] { ConfigurationManager.AppSettings["PasswordReminderKey"] };
            rr.Filter = filter;

            string requestID;

            // Execute RetrieveRequest
            String status = client.Retrieve(rr, out requestID, out results);

            // Output the Values
            Console.WriteLine(status);
            Console.WriteLine(requestID);
            Console.WriteLine(results.Length);
            Console.WriteLine("_________Properties______________");
        }
    }
}

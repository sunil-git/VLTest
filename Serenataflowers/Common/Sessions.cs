using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SFMobile.DTO;
namespace Serenataflowers
{
    /// <summary>
    ///SerenataflowersSession provides a facade to the ASP.NET Session object.
    /// All access to Session variables must be through this class.
    /// </summary>
    public static class SerenataflowersSessions
    {
        # region Private Constants
        //---------------------------------------------------------------------
        private const string countryid = "CountryId";
        private const string countryName = "CountryName";
        private const string orderid = "OrderId";
        private const string productid = "ProductId";
        private const string configxml = "configXml";
        private const string lstExtras = "lstExtras";
        private const string cartInfo = "cartInfo";
        private const string maxPriceProductId = "maxPriceProductId";
        private const string countcartitem = "CountCartItem";
        

        //---------------------------------------------------------------------
        # endregion

        # region Public Properties
        //---------------------------------------------------------------------
        /// <summary>
        ///    CountryId is used to store the current state of the 
        ///    Serenatflowes pages.
        /// </summary>
        public static string CountryId
        {
            get
            {
                return (string)HttpContext.Current.Session[countryid];
            }

            set
            {
                HttpContext.Current.Session[countryid] = value;
            }
        }
        /// <summary>
        ///     countryName is used to store the current state of the 
        ///    Serenatflowes pages.
        /// </summary>
        public static string CountryName
        {
            get
            {
                return (string)HttpContext.Current.Session[countryName];
            }

            set
            {
                HttpContext.Current.Session[countryName] = value;
            }
        }
        /// <summary>
        ///     OrderId is used to store the current state Order of the 
        ///     Serenatflowes page.
        /// </summary>
        public static string OrderId
        {
            get
            {
                return (string)HttpContext.Current.Session[orderid];
            }

            set
            {
                HttpContext.Current.Session[orderid] = value;
            }
        }
        /// <summary>
        ///     ProductId is used to store the current state of Product in the 
        ///     Serenatflowers page.
        /// </summary>
        public static string ProductId
        {
            get
            {
                return (string)HttpContext.Current.Session[productid];
            }

            set
            {
                HttpContext.Current.Session[productid] = value;
            }
        }
        /// <summary>
        ///     ConfigXML is used to store the current state of ConfigXML in the 
        ///     Serenatflowers page.
        /// </summary>
        public static string ConfigXML
        {
            get
            {
                return (string)HttpContext.Current.Session[configxml];
            }

            set
            {
                HttpContext.Current.Session[configxml] = value;
            }
        }

        /// <summary>
        ///     LstExtras is used to store the current state of LstExtras in the 
        ///     Serenatflowers page.
        /// </summary>
        public static List<CartInfo> LstExtras
        {
            get
            {
                return (List<CartInfo>)HttpContext.Current.Session[lstExtras];
            }

            set
            {
                HttpContext.Current.Session[lstExtras] = value;
            }
        }
        /// <summary>
        ///     CartInfo is used to store the current state of CartInfo in the 
        ///     Serenatflowers page.
        /// </summary>
        public static List<CartInfo> CartInfo
        {
            get
            {
                return (List<CartInfo>) HttpContext.Current.Session[cartInfo];
            }

            set
            {
                HttpContext.Current.Session[cartInfo] = value;
            }
        }
        /// <summary>
        ///     CartInfo is used to store the current state of CartInfo in the 
        ///     Serenatflowers page.
        /// </summary>
        public static int MaxPriceProductId
        {
            get
            {
                return (Int32)HttpContext.Current.Session[maxPriceProductId];
            }

            set
            {
                HttpContext.Current.Session[maxPriceProductId] = value;
            }
        }

        /// <summary>
        ///     CartInfo is used to store the current state of CartInfo in the 
        ///     Serenatflowers page.
        /// </summary>
        public static string CountCartItem
        {
            get
            {
                return (string)HttpContext.Current.Session[countcartitem];
            }

            set
            {
                HttpContext.Current.Session[countcartitem] = value;
            }
        }
        //---------------------------------------------------------------------
        # endregion
    }
}
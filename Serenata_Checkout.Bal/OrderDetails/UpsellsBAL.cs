using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Xml;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Dal;
using Serenata_Checkout.Log;

namespace Serenata_Checkout.Bal
{
    public class UpsellsBAL
    {
        /// <summary>
        /// To get Upsells details
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <returns></returns>
        public List<ProductInfo> GetUpsells(int partnerId, int productId, int itemId1, int itemId2, int itemId3)
        { List<ProductInfo> objProductInfoList  = new List<ProductInfo>();
            try
            {
                objProductInfoList = new UpsellsDAL().GetUpsells(partnerId, productId, itemId1, itemId2, itemId3);

                objProductInfoList = SetFinalPrice(objProductInfoList);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }

          return objProductInfoList;
        }

        /// <summary>
        /// To add upsells product
        /// </summary>
        /// <param name="objOrderLines"></param>
        public void AddUpsellsProduct(OrderLinesInfo objOrderLines)
        {
            new UpsellsDAL().AddUpsellsProduct(objOrderLines);

        }
        #region  To get delivery details by using delivery option ID.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: SFsp_GetUpsaleCountInBasket()
        /// StoredProcedure Name: SFsp_GetUpsaleCountInBasket
        /// Description: This method contains logic to execute stored procedure "SFsp_GetUpsaleCountInBasket" to get upsale count.
        /// </summary>
        /// <param name="objOrder"></param>
        public int GetUpsaleCount(string orderid)
        {
            return new UpsellsDAL().GetUpsaleCount(orderid);
        }
        #endregion

        #region  To get delivery partnerID by using orderid.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: SFsp_GetUpsaleCountInBasket()
        /// StoredProcedure Name: SFsp_CheckDefaultDeliveryProductInBasket
        /// Description: This method contains logic to execute stored procedure "SFsp_CheckDefaultDeliveryProductInBasket" to get DeliveryPartnerid
        /// </summary>
        /// <param name="objOrder"></param>
        public int GetDeliveryPartnerid(string orderid)
        {
            return new UpsellsDAL().GetDeliveryPartnerid(orderid);
        }
        #endregion

        /// <summary>
        /// This methiod used to set final price with respective offers
        /// </summary>
        /// <param name="objProductInfoList"></param>
        /// <returns></returns>
        private List<ProductInfo> SetFinalPrice(List<ProductInfo> objProductInfoList)
        {
            List<ProductInfo> objProductPriceInfo = new List<ProductInfo>();
            try
            {
                foreach (ProductInfo objProductInfo in objProductInfoList)
                {
                    ProductInfo objtempProductInfo = new ProductInfo();

                    //objtempProductInfo.img1_small_low = objProductInfo.img1_small_low;
                    //objtempProductInfo.img1_big_high = objProductInfo.img1_big_high;
                    objtempProductInfo.img1_small_low = objProductInfo.img1_small_low.Replace("http", "https");
                    objtempProductInfo.img1_big_high = objProductInfo.img1_big_high.Replace("http", "https");
                    objtempProductInfo.offer = objProductInfo.offer;
                    objtempProductInfo.productid = objProductInfo.productid;
                    objtempProductInfo.producttitle = objProductInfo.producttitle;
                    objtempProductInfo.info2 = objProductInfo.info2;

                    DateTime dtStartDate = objProductInfo.offerstartdate;
                    DateTime dtEndDate = objProductInfo.offerenddate;

                    DateTime dt = DateTime.Now;

                    if (dtStartDate <= dt && dtEndDate >= dt && objProductInfo.offer != 0.00)
                    {
                        objtempProductInfo.price = objProductInfo.offer;
                    }
                    else
                    {
                        objtempProductInfo.price = objProductInfo.price;
                    }

                    objProductPriceInfo.Add(objtempProductInfo);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return objProductPriceInfo;
        }

        /// <summary>
        /// This method used to get PIONVariables from ProductPION object.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rowNo"></param>
        /// <param name="colNo"></param>
        /// <returns></returns>
        public string GetPIONVariables(int productId, int rowNo, int colNo)
        {
            ProductPION objProductPION = new ProductPION();
            string strRtn = string.Empty;
            try
            {
                objProductPION = new UpsellsDAL().GetPIONSByProductID(productId);
                strRtn = objProductPION.productid + "," + objProductPION.price + "," + "R" + rowNo + "C" + colNo + "," + IsZero(Convert.ToString(objProductPION.discount)) + "," + objProductPION.raiting + "," + objProductPION.noOfreviews + "," + objProductPION.hasVideo + "," + objProductPION.hasDiscountRibbon;

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strRtn;
        }

        private string IsZero(string strVal)
        {
            string str = string.Empty;
            try
            {
                if (strVal.Equals("0"))
                {
                    str = string.Empty;
                }
                else
                {
                    str = strVal;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return str;
        }

        //public string BindPIONVariablesByXML(string filename)
        //{
           
        //    string strRtn = string.Empty;
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds.ReadXml(filename);
        //        if (ds.Tables != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables.Contains("upsellsProduct"))
        //            {
        //                DataTable dtProducts = ds.Tables["upsellsProduct"];

        //                foreach (DataRow row in dtProducts.Rows)
        //                {
        //                    ProductPION objProductPION = new ProductPION();
        //                    objProductPION.productid = Convert.ToInt32(row["id"]);
        //                    objProductPION.price = Convert.ToDouble(row["upsellsPrice"]); 

                            
                            

        //                }
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.Error(ex);
        //    }
        //    return strRtn;

        //}


        public void BindProductByXML( string filename)
        {

            try
            {
                int startrowindex = 0;
                int endrowindex = 0;

                List<ProductInfo> ukProducts = new List<ProductInfo>();
               
                XmlDocument doc = new XmlDocument();
                XmlNodeList nodeList;
                doc.Load(filename);

                nodeList = doc.SelectNodes("response/product");
               
                if (nodeList.Count <= endrowindex)
                {
                    endrowindex = nodeList.Count;
                }
                XmlElement ProductElement = (XmlElement)nodeList[1];

                XmlNodeList ReviewDataList = ProductElement.GetElementsByTagName("upsells");

                foreach (XmlNode ReviewDatanode in ReviewDataList)
                {
                    XmlElement Reviewsnode = (XmlElement)ReviewDatanode;
                    if (Reviewsnode != null && Reviewsnode.InnerXml != "")
                    {
                        string strvalue = Reviewsnode.GetElementsByTagName("name")[0].InnerText;

                        //if (Reviewsnode.GetElementsByTagName("totalReviews")[0].InnerText != "")
                        //{
                        //    objproduct.TotalReviews = Reviewsnode.GetElementsByTagName("totalReviews")[0].InnerText;
                        //}
                        //if (Reviewsnode.GetElementsByTagName("starPercentage")[0].InnerText != "")
                        //{
                        //    objproduct.StarPercentage = Reviewsnode.GetElementsByTagName("starPercentage")[0].InnerText;
                        //}
                        //if (Reviewsnode.GetElementsByTagName("starRating")[0].InnerText != "")
                        //{
                        //    objproduct.StarRating = Reviewsnode.GetElementsByTagName("starRating")[0].InnerText;
                        //}

                    }
                }

                //for (int i = startrowindex; i < endrowindex; i++)
                //{

                //    XmlElement ProductElement = (XmlElement)nodeList[i];
                //    ProductInfo objproduct = new ProductInfo();
                //    if (ProductElement.GetAttribute("id") != "")
                //    {
                //       int intId = Convert.ToInt32(ProductElement.GetAttribute("id"));

                //    }
                //    //objproduct.ProductName = GetFixedChar(ProductElement.GetElementsByTagName("name")[0].InnerText, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisplayProdnamelength"]));
                //    //objproduct.ProductDesc = ProductElement.GetElementsByTagName("info2")[0].InnerText;
                //    //if (ProductElement.GetElementsByTagName("price")[0].InnerText != "")
                //    //{
                //    //    objproduct.ProductOldPrice = Convert.ToDecimal(ProductElement.GetElementsByTagName("price")[0].InnerText);
                //    //}
                //    //if (ProductElement.GetElementsByTagName("offer")[0].Attributes.Item(0).Value != "0")
                //    //{
                //    //    objproduct.ProductOfferPrice = Convert.ToDecimal(ProductElement.GetElementsByTagName("offer")[0].InnerText);
                //    //}
                //    //objproduct.TotalRecords = Convert.ToInt32(nodeList.Count);

                //    //XmlNodeList ReviewDataList = ProductElement.GetElementsByTagName("reviewData");
                //    //foreach (XmlNode ReviewDatanode in ReviewDataList)
                //    //{
                //    //    XmlElement Reviewsnode = (XmlElement)ReviewDatanode;
                //    //    if (Reviewsnode != null && Reviewsnode.InnerXml != "")
                //    //    {
                //    //        //if (Reviewsnode.GetElementsByTagName("totalReviews")[0].InnerText != "")
                //    //        //{
                //    //        //    objproduct.TotalReviews = Reviewsnode.GetElementsByTagName("totalReviews")[0].InnerText;
                //    //        //}
                //    //        //if (Reviewsnode.GetElementsByTagName("starPercentage")[0].InnerText != "")
                //    //        //{
                //    //        //    objproduct.StarPercentage = Reviewsnode.GetElementsByTagName("starPercentage")[0].InnerText;
                //    //        //}
                //    //        //if (Reviewsnode.GetElementsByTagName("starRating")[0].InnerText != "")
                //    //        //{
                //    //        //    objproduct.StarRating = Reviewsnode.GetElementsByTagName("starRating")[0].InnerText;
                //    //        //}
                           
                //    //    }

                //    //}

                //    //XmlNodeList ImageList = ProductElement.GetElementsByTagName("images");
                //    //foreach (XmlNode Imagenode in ImageList)
                //    //{
                //    //    XmlElement ProductImage = (XmlElement)Imagenode;
                //    //    XmlNodeList ImagesList = ProductImage.GetElementsByTagName("image");
                //    //    foreach (XmlNode Imagesnode in ImagesList)
                //    //    {
                //    //        XmlElement ImageName = (XmlElement)Imagesnode;
                //    //        if (ImageName.GetAttribute("id") == "img1SmallHigh")
                //    //        {
                //    //            objproduct.ImagePath = ImageName.InnerText;
                //    //        }
                //    //    }
                //    //}
                //    //ukProducts.Add(objproduct);
                //    //ProductInfo objproduct = new ProductInfo();
                //    //objproduct.ProductId = Convert.ToInt32(nodeList[i].Attributes[0].Value);
                //    //objproduct.ProductName = nodeList[i].ChildNodes[0].InnerText;
                //    //objproduct.ProductDesc = nodeList[i].ChildNodes[1].InnerText;
                //    //objproduct.ImagePath = nodeList[i].ChildNodes[11].ChildNodes[2].InnerText;
                //    //objproduct.ProductOldPrice = Convert.ToDecimal(nodeList[i].ChildNodes[5].InnerText);
                //    //objproduct.ProductOfferPrice = Convert.ToDecimal(nodeList[i].ChildNodes[6].InnerText);
                //    //objproduct.TotalRecords = Convert.ToInt32(nodeList.Count);
                //    //ukProducts.Add(objproduct);
                //}

              
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }


        }

        public ProductInfo GetSpotLightUpsell(string strOrderId)
        {
            return new UpsellsDAL().GetSpotLightUpsell(strOrderId);
        }


    }
}

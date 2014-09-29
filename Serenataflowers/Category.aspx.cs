using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFMobile.BAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SFMobile.BAL.SiteData;
using SFMobile.BAL.Products;
using SFMobile.DTO;
using System.Xml;
using SFMobile.Exceptions;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.SessionState;
using System.Globalization;

namespace Serenataflowers
{
    public partial class Category : System.Web.UI.Page
    {
        #region variables

        CountriesLogic objCountries;
        CategoriesLogic objCategories;
        CommonFunctions objCommondetails;
        public static int productSetId;
        #endregion

        #region Page Events
        /// <summary>
        /// This Event is fired usually the most common used method on the server side 
        /// application code for an .aspx file. All code inside of this method is executed once at the beginning of 
        /// the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    Paging();
                    XMLPaging();
                    if (Request.QueryString["catId"] != null)
                    {
                        ViewState["catId"] = Request.QueryString["catId"];
                        ViewState["ProdType"] = Request.QueryString["ProdType"];
                    }
                    else
                    {
                        ViewState["catId"] = null;
                        ViewState["ProdType"] = null;
                    }
                    ViewState["urlredirect"] = "true";
                    ViewState["urlCatredirect"] = "true";
                    if (Convert.ToString(ViewState["urlredirect1"]) != "false")
                    {
                        FillCountries();
                        drpCountry_SelectedIndexChanged(null, null);
                        drpCategory_SelectedIndexChanged(null, null);                 
                    }
                    ViewState["meta"] = "true";
                    FillItemsViaQS();
                }
                catch (Exception ex)
                {
                    SFMobileLog.Error(ex);
                }
            }

            CreateMetaTags();
            //if (drpCategory.SelectedItem.Text == "Bestseller")
            //    CommonFunctions.AddFloodLightTags(this.Page, "Bestseller");
            //else
            //    CommonFunctions.AddFloodLightTags(this.Page);




        }
        /// <summary>
        /// This event is SelectedIndexChanged event for country dropdown.it will fired when user changes any
        /// country from dropdownlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpCountry.SelectedValue != "0")
                {

                    
                    FillCategoriesByProductType(drpCountry.SelectedValue);
                    Paging();
                    XMLPaging();
                    FillBanner(Convert.ToInt32(drpCategory.SelectedValue));
                    if (Convert.ToString(ViewState["meta"]) == "false")
                    {
                        AddcatMetaTag();
                    }
                    ViewState["meta"] = "false";
                    productSetId = GetProductSetIdByCategoryId(Convert.ToInt32(drpCategory.SelectedValue));
                    ViewState["productSetId"] = productSetId;
                    if (productSetId != 0)
                    {
                        BindProductByXML(Convert.ToInt32(ViewState["startxmlRowSize"]), Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                    }
                    //else if (drpCategory.SelectedValue == "9999")
                    //{
                    //    BindProductByXML(Convert.ToInt32(ViewState["startxmlRowSize"]), Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                    //}
                    else
                    {
                        BindProductsByCategory(Convert.ToInt32(drpCategory.SelectedValue));
                    }
                    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), Guid.NewGuid().ToString(), "hideModal();", true);
                    ViewState["urlredirect1"] = "false";
                    if (Convert.ToString(ViewState["urlredirect"]) != "true" && Convert.ToString(ViewState["urlredirect1"]) == "false")
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                        {
                            Response.Redirect("Default.aspx?catId=" + drpCategory.SelectedItem.Value + "&prodType=" + drpCountry.SelectedItem.Value + "&s=" + Request.QueryString["s"], false);
                        }
                        else
                        {
                            Response.Redirect("Default.aspx?catId=" + drpCategory.SelectedItem.Value + "&prodType=" + drpCountry.SelectedItem.Value, false);
                        }
                    }
                    ViewState["urlredirect"] = "false";
                    
                }
                else
                {
                    Response.Redirect("international.aspx", false);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// This Event is SelectedIndexChanged event for category dropdownlist.This event will 
        /// fired when user select any country from country dropdownlist or user can select any category from catrgory 
        /// dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Paging();
                XMLPaging();              
                FillBanner(Convert.ToInt32(drpCategory.SelectedValue));
                AddcatMetaTag();
                productSetId = GetProductSetIdByCategoryId(Convert.ToInt32(drpCategory.SelectedValue));
                // productSetId = 3573;
                ViewState["productSetId"] = productSetId;
                if (productSetId != 0)
                {
                    BindProductByXML(Convert.ToInt32(ViewState["startxmlRowSize"]), Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                }
                else
                {
                    BindProductsByCategory(Convert.ToInt32(drpCategory.SelectedValue));
                }
                                
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), Guid.NewGuid().ToString(), "hideModal();", true);
                if (Convert.ToString(ViewState["urlCatredirect"]) != "true")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                    {
                        Response.Redirect("Category.aspx?catId=" + drpCategory.SelectedItem.Value + "&prodType=" + drpCountry.SelectedItem.Value + "&s=" + Request.QueryString["s"], false);
                    }
                    else
                    {
                        Response.Redirect("Category.aspx?catId=" + drpCategory.SelectedItem.Value + "&prodType=" + drpCountry.SelectedItem.Value, false);
                    }
                }
                ViewState["urlCatredirect"] = "false";
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        /// <summary>
        /// This event provides you with the last opportunity to access the price information for datalist item before it is displayed on the client.
        /// After this event is raised, the data item is no longer available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProductList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {

                HtmlGenericControl offerprice = e.Item.FindControl("offerprice") as HtmlGenericControl;
                if (offerprice != null)
                {
                    if (offerprice.InnerText.Trim() == "£0")
                    {
                        offerprice.Visible = false;
                        HtmlGenericControl oldprice = e.Item.FindControl("oldprice") as HtmlGenericControl;
                        if (oldprice != null)
                        {
                            oldprice.Attributes.Add("class", "normalPrice");
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    HtmlAnchor anc = e.Item.FindControl("ancOrdernow") as HtmlAnchor;
                    if (anc != null)
                    {
                        anc.HRef += "&s=" + Request.QueryString["s"];


                    }
                    HtmlAnchor ancname = e.Item.FindControl("ancname") as HtmlAnchor;
                    if (ancname != null)
                    {
                        ancname.HRef += "&s=" + Request.QueryString["s"];


                    }
                    HtmlAnchor ancbtn = e.Item.FindControl("ancbtn") as HtmlAnchor;
                    if (ancbtn != null)
                    {
                        ancbtn.HRef += "&s=" + Request.QueryString["s"];


                    }
                }
                HtmlGenericControl divreview = e.Item.FindControl("divTotalReview") as HtmlGenericControl;
                if (divreview != null)
                {
                    if (divreview.InnerText.Trim() == "" || divreview.InnerText.Trim() == "0")
                    {
                        HtmlGenericControl divcost = e.Item.FindControl("divcust") as HtmlGenericControl;
                        if (divcost != null)
                        {
                            divcost.InnerHtml = "";
                        }
                        HtmlGenericControl divratVal = e.Item.FindControl("divratVal") as HtmlGenericControl;
                        if (divratVal != null)
                        {
                            divratVal.InnerHtml = "";
                        }
                        HtmlGenericControl divStar = e.Item.FindControl("divStar") as HtmlGenericControl;
                        if (divStar != null)
                        {
                            divStar.InnerHtml = "";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// This event is used for pagination for datalist ,when user click the button it will display the
        /// data based on paging configuration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMore_Click(object sender, EventArgs e)
        {
            try
            {
                int ipageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
                ViewState["startRowSize"] = Convert.ToInt32(ViewState["endRowSize"]) + 1;
                ViewState["endRowSize"] = Convert.ToInt32(ViewState["startRowSize"]) + ipageSize - 1;
                ViewState["startxmlRowSize"] = Convert.ToInt32(ViewState["endRowSize"]) + 1;
                ViewState["endxmlRowSize"] = Convert.ToInt32(ViewState["startxmlRowSize"]) - 1;

                int productSetId = Convert.ToInt32(ViewState["productSetId"]);

                if (productSetId != 0)
                {
                    //BindProductByXML(Convert.ToInt32(ViewState["startxmlRowSize"]), Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                    BindProductByXML(0, Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                }
                //else if (drpCountry.SelectedValue == "215" && drpCategory.SelectedValue == "9999")
                //{
                //    //BindProductByXML(Convert.ToInt32(ViewState["startxmlRowSize"]), Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                //    BindProductByXML(0, Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                //}
                else
                {
                    //BindProducts(Convert.ToInt32(drpCountry.SelectedValue), Convert.ToInt32(drpCategory.SelectedValue));
                    BindProductsByCategory(Convert.ToInt32(drpCategory.SelectedValue), 0, Convert.ToInt32(ViewState["endRowSize"]));

                }


                if (drpCountry.SelectedValue != "215")
                {
                    // BindProducts(Convert.ToInt32(drpCountry.SelectedValue), Convert.ToInt32(drpCategory.SelectedValue), 0, Convert.ToInt32(ViewState["endRowSize"]));
                    if (Convert.ToInt32(ViewState["Totalrecord"]) > Convert.ToInt32(ViewState["endRowSize"]))
                    {
                        lnkMore.Visible = true;
                    }
                    else
                    {
                        lnkMore.Visible = false;
                    }
                }
                else
                {
                    //BindProductByXML(0, Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());

                    if (Convert.ToInt32(ViewState["Totalrecord"]) > Convert.ToInt32(ViewState["endxmlRowSize"]))
                    {
                        lnkMore.Visible = true;
                    }
                    else
                    {
                        lnkMore.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }


        }

        #endregion

        #region Page Methods
        /// <summary> 
        /// Description: This Method contains the logic to bind the countries in Country dropdownlist
        /// </summary>        
        private void FillCountries()
        {


            try
            {
                List<ProductTypeInfo> lstProductTypeInfo = new List<ProductTypeInfo>();
                objCommondetails = new CommonFunctions();
                string xPathProductType = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["ProductTypeCategoryXML"].ToString());
                lstProductTypeInfo = objCommondetails.GetProductType(xPathProductType);
                if (lstProductTypeInfo != null)
                {
                    if (lstProductTypeInfo.Count > 0)
                    {
                        drpCountry.DataSource = lstProductTypeInfo;
                        drpCountry.DataTextField = "ProductType";
                        drpCountry.DataValueField = "ProductTypeId";
                        drpCountry.DataBind();
                    }
                }
                int selectedIndex = GetDomain(lstProductTypeInfo);
                //if (selectedIndex != 0)
                //{
                //    drpCountry.Items.FindByValue(selectedIndex.ToString()).Selected = true;
                //}
                if (ViewState["ProdType"] != null)
                {
                    drpCountry.Items.FindByValue(ViewState["ProdType"].ToString()).Selected = true;
                }
                else
                {
                    if (selectedIndex != 0)
                    {
                        drpCountry.Items.FindByValue(selectedIndex.ToString()).Selected = true;
                    }
                }

                ViewState["ProdType"] = null;
                //dsCountry = objCountries.GetCountries();
                //if (dsCountry != null)
                //{
                //    if (dsCountry.Tables[0].Rows.Count > 0)
                //    {
                //        drpCountry.DataSource = dsCountry;
                //        drpCountry.DataTextField = "CountryName";
                //        drpCountry.DataValueField = "Id";
                //        drpCountry.DataBind();

                //    }
                //}
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        /// <summary>
        /// This Method contains the logic to bind the categories in category dropdownlist
        /// </summary>
        /// <param name="countryId">int Ex:38</param>
        private void FillCategories(int countryId)
        {
            objCategories = new CategoriesLogic();
            DataSet dscategory = new DataSet();
            try
            {
                dscategory = objCategories.GetCategoriesByCountryId(countryId);
                if (dscategory != null)
                {
                    if (dscategory.Tables[0].Rows.Count > 0)
                    {
                        drpCategory.Items.Clear();
                        drpCategory.DataSource = dscategory;
                        drpCategory.DataTextField = "CategoryName";
                        drpCategory.DataValueField = "submenuid";
                        drpCategory.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// This Method contains the logic to fill Products list based on country and category
        /// </summary>
        /// <param name="countryId">int Ex:215 </param>
        /// <param name="categoryId">int :38</param>
        private void BindProducts(int countryId, int categoryId)
        {
            try
            {
                List<ProductDTO> listProduct = new List<ProductDTO>();
                ProductsLogic objProducts = new ProductsLogic();
                listProduct = objProducts.GetProductsByCountryCagetory(countryId, categoryId);
                if (listProduct.Count > 0)
                {
                    foreach (ProductDTO objProduct in listProduct)
                    {
                        objProduct.ProductName = GetFixedChar(objProduct.ProductName, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisplayProdnamelength"]));
                    }
                    GetPionVeriable(listProduct);
                    ProductList.Visible = true;
                    ProductList.DataSource = listProduct;
                    ProductList.DataBind();
                    ViewState["Totalrecord"] = listProduct[0].TotalRecords;
                    if (listProduct[0].TotalRecords > Convert.ToInt32(ViewState["endRowSize"]))
                    {
                        lnkMore.Visible = true;
                    }
                    else
                    {
                        lnkMore.Visible = false;
                    }

                }
                else
                {
                    if (countryId == 215)
                    {
                        BindProductByXML(Convert.ToInt32(ViewState["startxmlRowSize"]), Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                    }
                    else
                    {
                        listProduct = objProducts.GetProductsByCountryCagetory(countryId, 8245);
                        if (listProduct.Count > 0)
                        {
                            GetPionVeriable(listProduct);
                            ProductList.Visible = true;
                            ProductList.DataSource = listProduct;
                            ProductList.DataBind();
                            ViewState["Totalrecord"] = listProduct[0].TotalRecords;
                            if (listProduct[0].TotalRecords > Convert.ToInt32(ViewState["endRowSize"]))
                            {
                                lnkMore.Visible = true;
                            }
                            else
                            {
                                lnkMore.Visible = false;
                            }

                        }
                        else
                        {

                            lnkMore.Visible = false;
                            ProductList.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Overloaded Method to fill Products list based on country and category
        /// </summary>
        /// <param name="countryId">int</param>
        /// <param name="categoryId">int</param>
        /// <param name="stratRow">int</param>
        /// <param name="endRow">int</param>
        private void BindProducts(int countryId, int categoryId, int stratRow, int endRow)
        {
            try
            {
                List<ProductDTO> listProduct = new List<ProductDTO>();
                ProductsLogic objProducts = new ProductsLogic();
                listProduct = objProducts.GetProductsByCountryCagetory(countryId, categoryId, stratRow, endRow);
                if (listProduct.Count > 0)
                {
                    GetPionVeriable(listProduct);
                    ProductList.Visible = true;
                    ProductList.DataSource = listProduct;
                    ProductList.DataBind();
                    if (listProduct[0].TotalRecords > Convert.ToInt32(ViewState["endRowSize"]))
                    {
                        lnkMore.Visible = true;
                    }
                    else
                    {
                        lnkMore.Visible = false;
                    }

                }
                else
                {
                    if (countryId == 215)
                    {
                        BindProductByXML(0, Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                    }
                    else
                    {
                        listProduct = objProducts.GetProductsByCountryCagetory(countryId, 8245);
                        if (listProduct.Count > 0)
                        {
                            GetPionVeriable(listProduct);
                            ProductList.Visible = true;
                            ProductList.DataSource = listProduct;
                            ProductList.DataBind();
                            ViewState["Totalrecord"] = listProduct[0].TotalRecords;
                            if (listProduct[0].TotalRecords > Convert.ToInt32(ViewState["endRowSize"]))
                            {
                                lnkMore.Visible = true;
                            }
                            else
                            {
                                lnkMore.Visible = false;
                            }

                        }
                        else
                        {

                            lnkMore.Visible = false;
                            ProductList.Visible = false;
                        }
                    }
                }
                //else
                //{

                //    lnkMore.Visible = false;
                //    ProductList.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }
        }
        /// <summary>
        /// This Method contains the logic to fill Products from XML datasource  based paging
        /// </summary>
        /// <param name="startrowindex">int</param>
        /// <param name="endrowindex">int</param>
        private void BindProductByXML(int startrowindex, int endrowindex, string filename)
        {

            try
            {
                ProductsLogic objProducts = new ProductsLogic();
                List<ProductDTO> ukProducts = new List<ProductDTO>();
              //  string xproductDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["ProductXMLsDirectory"].ToString();
                string xdefaultXmlPath = System.Configuration.ConfigurationManager.AppSettings["DefaultProductXML"].ToString();
                int productSet = Convert.ToInt32(filename);
               // string xPath = Path.GetFullPath(xproductDirectoryPath + "\\" + filename + ".xml");
                // Added by valuelabs on 1st aug 2013 to get XML from DB
                string strProductSETXML = objProducts.GetProductSetXMLByProductSet(productSet);
                string xDefaultProduct = Path.GetFullPath(xdefaultXmlPath);
                string xDefaultProductfile = Server.MapPath("xml/DefaultProducts.xml");
                XmlDocument doc = new XmlDocument();
                XmlNodeList nodeList;
                    //Commented by valuelabs on 1st Aug 2013 to get XML from DB
                //if (File.Exists(xPath))
                //{
                    
                  //  doc.Load(xPath);
                //}
                if (!string.IsNullOrEmpty(strProductSETXML))
                {
                    doc.LoadXml(strProductSETXML);
                }
                else if (File.Exists(xDefaultProduct))
                {

                    doc.Load(xDefaultProduct);
                }
                else
                {
                    doc.Load(xDefaultProductfile);
                }
                nodeList = doc.SelectNodes("response/products/product");
                if (nodeList.Count == 0)
                {
                    doc.Load(xDefaultProductfile);
                }
                nodeList = doc.SelectNodes("response/products/product");
                if (nodeList.Count < endrowindex)
                {
                    endrowindex = nodeList.Count;
                }
                for (int i = startrowindex; i < endrowindex; i++)
                {

                    XmlElement ProductElement = (XmlElement)nodeList[i];
                    ProductDTO objproduct = new ProductDTO();
                    if (ProductElement.GetAttribute("id") != "")
                    {
                        objproduct.ProductId = Convert.ToInt32(ProductElement.GetAttribute("id"));

                    }
                    objproduct.ProductName = GetFixedChar(ProductElement.GetElementsByTagName("name")[0].InnerText, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisplayProdnamelength"]));
                    objproduct.ProductDesc = ProductElement.GetElementsByTagName("info2")[0].InnerText;
                    if (ProductElement.GetElementsByTagName("price")[0].InnerText != "")
                    {
                        objproduct.ProductOldPrice = Convert.ToDecimal(ProductElement.GetElementsByTagName("price")[0].InnerText);
                    }
                    if (ProductElement.GetElementsByTagName("offer")[0].Attributes.Item(0).Value != "0")
                    {
                        objproduct.ProductOfferPrice = Convert.ToDecimal(ProductElement.GetElementsByTagName("offer")[0].InnerText);
                    }
                    objproduct.TotalRecords = Convert.ToInt32(nodeList.Count);

                    XmlNodeList ReviewDataList = ProductElement.GetElementsByTagName("reviewData");
                    foreach (XmlNode ReviewDatanode in ReviewDataList)
                    {
                        XmlElement Reviewsnode = (XmlElement)ReviewDatanode;
                        if (Reviewsnode != null && Reviewsnode.InnerXml != "")
                        {
                            if (Reviewsnode.GetElementsByTagName("totalReviews")[0].InnerText != "")
                            {
                                objproduct.TotalReviews = Reviewsnode.GetElementsByTagName("totalReviews")[0].InnerText;
                            }
                            if (Reviewsnode.GetElementsByTagName("starPercentage")[0].InnerText != "")
                            {
                                objproduct.StarPercentage = Reviewsnode.GetElementsByTagName("starPercentage")[0].InnerText;
                            }
                            if (Reviewsnode.GetElementsByTagName("starRating")[0].InnerText != "")
                            {
                                objproduct.StarRating = Reviewsnode.GetElementsByTagName("starRating")[0].InnerText;
                            }
                            if (Reviewsnode.GetElementsByTagName("starRatingRounded")[0].InnerText != "")
                            {
                                objproduct.StarRatingRounded = Reviewsnode.GetElementsByTagName("starRatingRounded")[0].InnerText;
                                string ratingimage = "0.0";
                                if (objproduct.StarRatingRounded.Length == 1)
                                {
                                    ratingimage = objproduct.StarRatingRounded + ".0";
                                }
                                else
                                {
                                    ratingimage = objproduct.StarRatingRounded ;
                                }
                                objproduct.StarRatingImageURL = "http://images.serenataflowers.com/star." + ratingimage + ".png";
                            }
                        }

                    }

                    XmlNodeList ImageList = ProductElement.GetElementsByTagName("images");
                    foreach (XmlNode Imagenode in ImageList)
                    {
                        XmlElement ProductImage = (XmlElement)Imagenode;
                        XmlNodeList ImagesList = ProductImage.GetElementsByTagName("image");
                        foreach (XmlNode Imagesnode in ImagesList)
                        {
                            XmlElement ImageName = (XmlElement)Imagesnode;
                            if (ImageName.GetAttribute("id") == "img1SmallHigh")
                            {
                                objproduct.ImagePath = ImageName.InnerText;
                            }
                        }
                    }
                    ukProducts.Add(objproduct);
                    //ProductInfo objproduct = new ProductInfo();
                    //objproduct.ProductId = Convert.ToInt32(nodeList[i].Attributes[0].Value);
                    //objproduct.ProductName = nodeList[i].ChildNodes[0].InnerText;
                    //objproduct.ProductDesc = nodeList[i].ChildNodes[1].InnerText;
                    //objproduct.ImagePath = nodeList[i].ChildNodes[11].ChildNodes[2].InnerText;
                    //objproduct.ProductOldPrice = Convert.ToDecimal(nodeList[i].ChildNodes[5].InnerText);
                    //objproduct.ProductOfferPrice = Convert.ToDecimal(nodeList[i].ChildNodes[6].InnerText);
                    //objproduct.TotalRecords = Convert.ToInt32(nodeList.Count);
                    //ukProducts.Add(objproduct);
                }

                if (ukProducts.Count > 0)
                {
                    GetPionVeriable(ukProducts);

                    ProductList.Visible = true;
                    ProductList.DataSource = ukProducts;
                    ProductList.DataBind();
                    ViewState["Totalrecord"] = ukProducts[0].TotalRecords;
                    if (ukProducts[0].TotalRecords > Convert.ToInt32(ViewState["endRowSize"]))
                    {
                        lnkMore.Visible = true;
                    }
                    else
                    {
                        lnkMore.Visible = false;
                    }

                }
                else
                {
                    lnkMore.Visible = false;
                    ProductList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }


        }
        /// <summary>
        /// This methods contain the Paging logic for dalalist
        /// </summary>
        private void Paging()
        {
            try
            {
                int pageSize = 0;
                pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
                ViewState["startRowSize"] = pageSize;
                ViewState["endRowSize"] = pageSize;

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        /// <summary>
        /// This method contains the Paging for XML data source
        /// </summary>
        private void XMLPaging()
        {
            try
            {
                int pageSize = 0;
                pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
                ViewState["startxmlRowSize"] = 0;
                ViewState["endxmlRowSize"] = pageSize;

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        /// <summary>
        /// Display banner based on category
        /// </summary>
        /// <param name="categoryId"></param>
        private void FillBanner(int categoryId, int countryId)
        {
            objCategories = new CategoriesLogic();
            DataSet dscategoryBanner = new DataSet();
            try
            {
                dscategoryBanner = objCategories.GetBannerDetailsByCategoryId(categoryId, countryId);
                if (dscategoryBanner.Tables[0].Rows.Count > 0)
                {
                    lblContentTitle.Text = dscategoryBanner.Tables[0].Rows[0]["BannerTitle"].ToString();
                    lblcontentDesc.Text = dscategoryBanner.Tables[0].Rows[0]["BannerText"].ToString();
                    Page.Title = dscategoryBanner.Tables[0].Rows[0]["MetaTitle"].ToString();
                    banner.Src = "";
                    banner.Visible = false;
                }
                else
                {
                    Page.Title = " ";
                    lblContentTitle.Text = "";
                    lblcontentDesc.Text = "";
                    string xbannerPath = System.Configuration.ConfigurationManager.AppSettings["Banner"].ToString();
                    if (File.Exists(xbannerPath))
                    {
                        XmlDocument bannerdoc = new XmlDocument();
                        bannerdoc.Load(xbannerPath);
                        XmlNode bannernode = bannerdoc.SelectSingleNode("banners/category[@Id='" + categoryId + "']");
                        if (bannernode != null)
                        {
                            if (bannernode.InnerText != "")
                            {
                                banner.Src = bannernode.InnerText;
                                banner.Visible = true;
                            }
                        }
                        else
                        {
                            banner.Src = "";
                            banner.Visible = false;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Get the productSetId flag to get the product details from DB
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private int GetProductSetIdByCountryCategory(int countryId, int categoryId)
        {
            int productSetId = 0;
            try
            {
                ProductsLogic objProductSet = new ProductsLogic();
                productSetId = objProductSet.GetProductSetByCountryCategory(countryId, categoryId);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }

            return productSetId;

        }




        #region PION Meta Tag
        private void CreateMetaTags()
        {


            HtmlHead head = (HtmlHead)Page.Header;

            HtmlMeta hmdomain = new HtmlMeta();
            hmdomain.Name = "serenata.domain";
            hmdomain.Content = "serenataflowers.com";

            HtmlMeta hmpageName = new HtmlMeta();
            hmpageName.Name = "serenata.pageName";
            hmpageName.Content = "Mobile:Home";

            HtmlMeta hmchannel = new HtmlMeta();
            hmchannel.Name = "serenata.channel";
            hmchannel.Content = "home";

            HtmlMeta hmsessionID = new HtmlMeta();
            hmsessionID.Name = "serenata.sessionID";
            hmsessionID.Content = Session.SessionID;

            HtmlMeta hmdayOfWeek = new HtmlMeta();
            hmdayOfWeek.Name = "serenata.dayOfWeek";
            hmdayOfWeek.Content = DayOfWeek();

            HtmlMeta hmhourOfDay = new HtmlMeta();
            hmhourOfDay.Name = "serenata.hourOfDay";
            hmhourOfDay.Content = DateTime.Now.Hour.ToString();

            HtmlMeta hmcountry = new HtmlMeta();
            hmcountry.Name = "serenata.country";
            hmcountry.Content = "United Kingdom";

            HtmlMeta hmcurrencyID = new HtmlMeta();
            hmcurrencyID.Name = "serenata.currencyID";
            hmcurrencyID.Content = "1";

            CommonFunctions objCommondetails = new CommonFunctions();

            HtmlMeta hmserverIP = new HtmlMeta();
            hmserverIP.Name = "serenata.serverIP";
            hmserverIP.Content = objCommondetails.GetServerIp();

            HtmlMeta hmbrowserIP = new HtmlMeta();
            hmbrowserIP.Name = "serenata.browserIP";
            hmbrowserIP.Content = objCommondetails.GetUserIp();

            HtmlMeta hmdate = new HtmlMeta();
            hmdate.Name = "serenata.date";
            hmdate.Content = DateTime.Now.Date.ToString("dd/MM/yyyy");

            HtmlMeta hmdatetime = new HtmlMeta();
            hmdatetime.Name = "serenata.datetime";
            hmdatetime.Content = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            HtmlMeta hmnumSessionVariables = new HtmlMeta();
            hmnumSessionVariables.Name = "serenata.numSessionVariables";
            hmnumSessionVariables.Content = "0";

            head.Controls.Add(hmdomain);
            head.Controls.Add(hmpageName);
            head.Controls.Add(hmchannel);
            head.Controls.Add(hmsessionID);
            head.Controls.Add(hmdayOfWeek);
            head.Controls.Add(hmhourOfDay);
            head.Controls.Add(hmcountry);
            head.Controls.Add(hmcurrencyID);
            head.Controls.Add(hmserverIP);
            head.Controls.Add(hmbrowserIP);
            head.Controls.Add(hmdate);
            head.Controls.Add(hmdatetime);
            head.Controls.Add(hmdatetime);
            head.Controls.Add(hmnumSessionVariables);



        }
        private string DayOfWeek()
        {
            string DayofWeek;

            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    DayofWeek = "1";
                    break;
                case "Tuesday":
                    DayofWeek = "2";
                    break;
                case "Wednesday":
                    DayofWeek = "3";
                    break;
                case "Thursday":
                    DayofWeek = "4";
                    break;
                case "Friday":
                    DayofWeek = "5";
                    break;
                case "Saturday":
                    DayofWeek = "6";
                    break;
                case "Sunday":
                    DayofWeek = "7";
                    break;
                default:
                    DayofWeek = "1";
                    break;
            }
            return DayofWeek;
        }
        #endregion

        #region PION Veriable
        private void GetPionVeriable(List<ProductDTO> listProduct)
        {
            string pionlist = string.Empty;
            foreach (ProductDTO objProductInfo in listProduct)
            {
                if (objProductInfo.ProductOfferPrice == 0)
                {
                    pionlist += objProductInfo.ProductId.ToString() + "," + objProductInfo.ProductOldPrice + "|";
                }
                else
                {
                    pionlist += objProductInfo.ProductId.ToString() + "," + objProductInfo.ProductOfferPrice + "|";
                }
            }
            pion.Text = "<!-- PION VARIABLES  serenata.productList1=\"" + pionlist + "\"-->";

        }
        #endregion

        private void FillCategoriesByProductType(string ProductType)
        {
            List<CategoryInfo> lstCategoryList = new List<CategoryInfo>();
            CommonFunctions objCommondetails = new CommonFunctions();

            try
            {
                string xPathProductType = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["ProductTypeCategoryXML"].ToString());
                lstCategoryList = objCommondetails.GetCategoriesByProductType(xPathProductType, ProductType);
                if (lstCategoryList != null)
                {
                    if (lstCategoryList.Count > 0)
                    {
                        drpCategory.Items.Clear();
                        drpCategory.DataSource = lstCategoryList;
                        drpCategory.DataTextField = "CategoryName";
                        drpCategory.DataValueField = "CategoryId";
                        drpCategory.DataBind();
                        if (ViewState["catId"] != null)
                        {
                            drpCategory.Items.FindByValue(ViewState["catId"].ToString()).Selected = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private int GetProductSetIdByCategoryId(int categoryId)
        {
            int productSetId = 0;
            try
            {
                ProductsLogic objProductSet = new ProductsLogic();
                productSetId = objProductSet.GetProductSetByCategoryId(categoryId);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }

            return productSetId;

        }
        private void BindProductsByCategory(int categoryId)
        {
            DataTable objDtReviewData;
            try
            {
                List<ProductDTO> listProduct = new List<ProductDTO>();
                ProductsLogic objProducts = new ProductsLogic();
                listProduct = objProducts.GetProductsByCagetoryId(categoryId);
                if (listProduct.Count > 0)
                {
                    foreach (ProductDTO objProduct in listProduct)
                    {
                        objProduct.ProductName = GetFixedChar(objProduct.ProductName, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisplayProdnamelength"]));
                        objDtReviewData = new DataTable();
                        objDtReviewData = objProducts.GetReviewDataByProductId(objProduct.ProductId);
                        if (objDtReviewData != null)
                        {
                            objProduct.TotalReviews = Convert.ToString(objDtReviewData.Rows[0]["TotalReviews"]);
                            objProduct.StarPercentage = Convert.ToString(objDtReviewData.Rows[0]["StarPercentage"]);
                            objProduct.StarRating = Convert.ToString(objDtReviewData.Rows[0]["StarRating"]);
                            objProduct.StarRatingRounded = Convert.ToDouble(objDtReviewData.Rows[0]["StarRatingRounded"]).ToString("0.0", CultureInfo.InvariantCulture);
                            objProduct.StarRatingImageURL = "http://images.serenataflowers.com/star." + objProduct.StarRatingRounded + ".png";
                        }
                    }
                    GetPionVeriable(listProduct);
                    ProductList.Visible = true;
                    ProductList.DataSource = listProduct;
                    ProductList.DataBind();
                    ViewState["Totalrecord"] = listProduct[0].TotalRecords;
                    if (listProduct[0].TotalRecords > Convert.ToInt32(ViewState["endRowSize"]))
                    {
                        lnkMore.Visible = true;
                    }
                    else
                    {
                        lnkMore.Visible = false;
                    }

                }
                else
                {
                    lnkMore.Visible = false;
                    ProductList.Visible = false;
                }


            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void BindProductsByCategory(int categoryId, int stratRow, int endRow)
        {
            DataTable objDtReviewData;
            try
            {
                List<ProductDTO> listProduct = new List<ProductDTO>();
                ProductsLogic objProducts = new ProductsLogic();
                listProduct = objProducts.GetProductsByCagetoryId(categoryId, stratRow, endRow);
                if (listProduct.Count > 0)
                {
                    foreach (ProductDTO objProduct in listProduct)
                    {
                        objProduct.ProductName = GetFixedChar(objProduct.ProductName, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisplayProdnamelength"]));
                        objDtReviewData = new DataTable();
                        objDtReviewData = objProducts.GetReviewDataByProductId(objProduct.ProductId);
                        if (objDtReviewData != null)
                        {
                            objProduct.TotalReviews = Convert.ToString(objDtReviewData.Rows[0]["TotalReviews"]);
                            objProduct.StarPercentage = Convert.ToString(objDtReviewData.Rows[0]["StarPercentage"]);
                            objProduct.StarRating = Convert.ToString(objDtReviewData.Rows[0]["StarRating"]);
                            objProduct.StarRatingRounded = Convert.ToDouble(objDtReviewData.Rows[0]["StarRatingRounded"]).ToString("0.0", CultureInfo.InvariantCulture);
                            objProduct.StarRatingImageURL = "http://images.serenataflowers.com/star." + objProduct.StarRatingRounded + ".png";
                        }
                    }
                    GetPionVeriable(listProduct);
                    ProductList.Visible = true;
                    ProductList.DataSource = listProduct;
                    ProductList.DataBind();
                    if (listProduct[0].TotalRecords > Convert.ToInt32(ViewState["endRowSize"]))
                    {
                        lnkMore.Visible = true;
                    }
                    else
                    {
                        lnkMore.Visible = false;
                    }

                }
                else
                {
                    lnkMore.Visible = false;
                    ProductList.Visible = false;
                }
                //else
                //{

                //    lnkMore.Visible = false;
                //    ProductList.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }
        }
        private void FillBanner(int categoryId)
        {
            objCategories = new CategoriesLogic();
            DataSet dscategoryBanner = new DataSet();
            CommonFunctions objcommon = new CommonFunctions();
            MetaDataInfo objMetaData = new MetaDataInfo();
            ViewState["CatMetaKeywords"] = null;
            ViewState["CatMetaDesc"] = null;
            try
            {
                objMetaData = objcommon.GetMetaData(System.Configuration.ConfigurationManager.AppSettings["MetaDataXML"]);
                if (objMetaData != null)
                {

                    dscategoryBanner = objCategories.GetBannerDetailsByCategorySiteId(categoryId, Convert.ToInt32(objMetaData.SiteId));
                }
                else
                {
                    dscategoryBanner = objCategories.GetBannerDetailsByCategoryId(categoryId);
                }

                if (dscategoryBanner.Tables[0].Rows.Count > 0)
                {
                    lblContentTitle.Text = dscategoryBanner.Tables[0].Rows[0]["BannerTitle"].ToString();
                    lblcontentDesc.Text = dscategoryBanner.Tables[0].Rows[0]["BannerText"].ToString();
                    spntxt.InnerText = lblcontentDesc.Text;
                    lblcontentDesc.Text = lblcontentDesc.Text.Substring(0, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisplayProductDescLength"])) + "...<a href='javascript:showCatDescMore()'>more</a>";
                    if (dscategoryBanner.Tables[0].Rows[0]["MetaTitle"].ToString() != "")
                    {
                        ltTitle.Text = "\n<title>" + dscategoryBanner.Tables[0].Rows[0]["MetaTitle"].ToString() + "</title>\n";
                        //Page.Title = dscategoryBanner.Tables[0].Rows[0]["MetaTitle"].ToString();
                    }
                    else
                    {
                        ltTitle.Text = "\n<title>" + objMetaData.Title + "</title>\n";
                        //Page.Title = objMetaData.Title;
                    }
                    ViewState["CatMetaKeywords"] = dscategoryBanner.Tables[0].Rows[0]["MetaKeywords"].ToString();
                    ViewState["CatMetaDesc"] = dscategoryBanner.Tables[0].Rows[0]["MetaDesc"].ToString();
                    banner.Src = "";
                    banner.Visible = false;
                }
                else
                {
                    ltTitle.Text = "\n<title>" + "" + "</title>\n";
                    if (categoryId == 9999 || categoryId == 8205 || categoryId == 2249 || categoryId == 2249)
                    {
                        //Page.Title = objMetaData.Title;
                        ltTitle.Text = "\n<title>" + objMetaData.Title + "</title>\n";
                        ViewState["CatMetaKeywords"] = objMetaData.MetaKey;
                        ViewState["CatMetaDesc"] = objMetaData.MetaDesc;
                    }
                    lblContentTitle.Text = "";
                    lblcontentDesc.Text = "";
                    string xbannerPath = System.Configuration.ConfigurationManager.AppSettings["Banner"].ToString();
                    if (File.Exists(xbannerPath))
                    {
                        XmlDocument bannerdoc = new XmlDocument();
                        bannerdoc.Load(xbannerPath);
                        XmlNode bannernode = bannerdoc.SelectSingleNode("banners/category[@Id='" + categoryId + "']");
                        if (bannernode != null)
                        {
                            if (bannernode.InnerText != "")
                            {
                                ancBanner.HRef = bannernode.Attributes["linkUrl"].Value;
                                banner.Src = bannernode.InnerText;
                                banner.Visible = true;
                            }
                        }
                        else
                        {
                            banner.Src = "";
                            banner.Visible = false;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        private int GetDomain(List<ProductTypeInfo> lstdProducyTypedomain)
        {
            int strDomain = 0;
            try
            {
                string url = Request.Url.ToString();
                Uri baseUri = new Uri(url);
                string domain = baseUri.Host;
                foreach (ProductTypeInfo objdomain in lstdProducyTypedomain)
                {
                    if (objdomain.domainName.ToLower() == domain.ToLower())
                    {
                        strDomain = objdomain.ProductTypeId;
                        break;
                    }

                }

            }
            catch (Exception ex)
            {

                SFMobileLog.Error(ex);
            }
            return strDomain;
        }
        private string GetFixedChar(string strings, int length)
        {
            if (strings.Length > length)
            {
                strings = strings.Substring(0, length - 3);
                strings += "...";
            }
            return strings;
        }
        private void AddMetaTag(MetaDataInfo objmetaData)
        {
            HtmlHead head = (HtmlHead)Page.Header;
            if (objmetaData.MetaKey != "" && objmetaData.MetaDesc != "")
            {
                HtmlMeta hmkeyword = new HtmlMeta();
                hmkeyword.Name = "keywords";
                hmkeyword.Content = objmetaData.MetaKey;


                HtmlMeta hmdescription = new HtmlMeta();
                hmdescription.Name = "description";
                hmdescription.Content = objmetaData.MetaDesc;
                head.Controls.Add(hmdescription);

                head.Controls.Add(hmkeyword);
                head.Controls.Add(hmdescription);
            }

        }
        private void AddcatMetaTag()
        {
            HtmlHead head = (HtmlHead)Page.Header;
            if (Convert.ToString(ViewState["CatMetaKeywords"]) != "" && Convert.ToString(ViewState["CatMetaDesc"]) != "")
            {
                HtmlMeta hmkeyword = new HtmlMeta();
                hmkeyword.Name = "keywords";
                hmkeyword.Content = Convert.ToString(ViewState["CatMetaKeywords"]);


                HtmlMeta hmdescription = new HtmlMeta();
                hmdescription.Name = "description";
                hmdescription.Content = Convert.ToString(ViewState["CatMetaDesc"]);
                head.Controls.Add(hmdescription);

                head.Controls.Add(hmkeyword);
                head.Controls.Add(hmdescription);
            }

        }

        private void FillItemsViaQS()
        {
            try
            {
                int categoryId = Convert.ToInt32(Request.QueryString["catId"]);
                string productType = Convert.ToString(Request.QueryString["prodType"]);
                FillBanner(categoryId);
                AddcatMetaTag();
                productSetId = GetProductSetIdByCategoryId(categoryId);
                if (productSetId != 0)
                {
                    BindProductByXML(Convert.ToInt32(ViewState["startxmlRowSize"]), Convert.ToInt32(ViewState["endxmlRowSize"]), productSetId.ToString());
                }
                else
                {
                    BindProductsByCategory(Convert.ToInt32(drpCategory.SelectedValue));
                }
                drpCountry.Items.FindByValue(productType).Selected = true;
                drpCategory.Items.FindByValue(Convert.ToString(categoryId)).Selected = true;                   
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        #endregion
    }
}
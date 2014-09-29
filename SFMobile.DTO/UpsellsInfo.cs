using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMobile.DTO
{
    public class UpsellsInfo
    {
        #region Properties
        public string CategoryName { get; set; }
        public string CategoryInfo { get; set; }
        public List<ProductDTO> ProductInfo { get; set; }
        #endregion
    }
}

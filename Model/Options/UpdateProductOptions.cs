using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm.Model.Options
{
   public  class UpdateProductOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductCategory Category { get; set; }
    }
}

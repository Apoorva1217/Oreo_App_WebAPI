using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OreoAppCommonLayer.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public double ActualPrice { get; set; }
        [Required]
        public double DiscountedPrice { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        [Required]
        public string ProductImage { get; set; }  
    }
}

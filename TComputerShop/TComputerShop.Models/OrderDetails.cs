using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TComputerShop.Models
{
    public class OrderDetails
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        [Required]
        public double ProductPrice { get; set; }
    }
}

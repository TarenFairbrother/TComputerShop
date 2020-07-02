using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TComputerShop.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public string Comments { get; set; }

        public string Status { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public List<Product> Products { get; set; }

    }
}

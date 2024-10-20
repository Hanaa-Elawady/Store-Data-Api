using Store.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities.Order
{
    public class ShippingAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

    }
}

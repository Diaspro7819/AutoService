namespace AutoService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int OrderID { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        public DateTime OrderDeliveryDate { get; set; }

        public int OrderPickupPoint { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOrders { get; set; }

        [StringLength(100)]
        public string NameClient { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public virtual OrderProduct OrderProduct { get; set; }

        public virtual PickupPoint PickupPoint { get; set; }
    }
}

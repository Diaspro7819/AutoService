namespace AutoService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderProduct")]
    public partial class OrderProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }
    
        [Required]
        [StringLength(100)]
        public string ProductArticleNumber { get; set; }

        [StringLength(50)]
        public string Count { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}

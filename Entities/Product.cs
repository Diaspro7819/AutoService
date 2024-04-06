namespace AutoService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        [Key]
        [StringLength(100)]
        public string ProductArticleNumber { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public string ProductCategory { get; set; }

        [StringLength(50)]
        public string ProductPhoto { get; set; }

        [Required]
        public string ProductManufacturer { get; set; }

        public decimal ProductCost { get; set; }

        public int? ProductDiscountAmount { get; set; }

        public int? ProductQuantityInStock { get; set; }

        public string ProductStatus { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [StringLength(50)]
        public string MaxDiscountAmount { get; set; }

        [StringLength(50)]
        public string Supplier { get; set; }

        [StringLength(50)]
        public string CountInPack { get; set; }

        [StringLength(50)]
        public string MinCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

        public string Background
        {
            get
            {
                if (this.ProductDiscountAmount > 15){
                    return "#7fff00";

                }else 
                    return "#fff";
            }
        }


        public string CostWithDiscount
        {
            get
            { // System.FormatException: "Входная строка имела неверный формат."
                if (int.Parse(this.MaxDiscountAmount) > 0)
                {
                    var costWithDiscount = Convert.ToDouble(this.ProductCost) - Convert.ToDouble(this.ProductCost) * Convert.ToDouble(this.ProductDiscountAmount / 100.00);
                    return costWithDiscount.ToString();
                }else return this.ProductCost.ToString();
            }
        }
        public string ImgPath
        {
            get
            {
                //var path = "C://Users/79069/Desktop/3 курс/РПМ/AutoService/Resources/" + this.ProductPhoto;
                //var path = "C:\\Users\\79069\\Desktop\\3 курс\\РПМ\\AutoService\\Resources\\" + this.ProductPhoto;
                var path = "C:/Users/79069/Desktop/3 курс/РПМ/AutoService/Resources/" + this.ProductPhoto;

                return path;
            }
        }
        public override string ToString()
        {
            return $"Product Article Number: {ProductArticleNumber}\n" +
                   $"Product Name: {ProductName}\n" +
                   $"Product Description: {ProductDescription}\n" +
                   $"Product Category: {ProductCategory}\n" +
                   $"Product Photo: {ProductPhoto}\n" +
                   $"Product Manufacturer: {ProductManufacturer}\n" +
                   $"Product Cost: {ProductCost}\n" +
                   $"Product Discount Amount: {ProductDiscountAmount}\n" +
                   $"Product Quantity In Stock: {ProductQuantityInStock}\n" +
                   $"Product Status: {ProductStatus}\n" +
                   $"Unit: {Unit}\n" +
                   $"Max Discount Amount: {MaxDiscountAmount}\n" +
                   $"Supplier: {Supplier}\n" +
                   $"Count In Pack: {CountInPack}\n" +
                   $"Min Count: {MinCount}\n" +
                   $"Background: {Background}\n" +
                   $"Cost With Discount: {CostWithDiscount}\n" +
                   $"Image Path: {ImgPath}";
        }

    }
}

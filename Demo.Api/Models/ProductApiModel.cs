﻿namespace Demo.Api.Models
{
    public class ProductApiModel : BaseApiModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public int? CategoryId { get; set; }
        public CategoryApiModel Category { get; set; }
    }
}

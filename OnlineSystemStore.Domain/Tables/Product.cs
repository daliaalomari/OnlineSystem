using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace OnlineSystemStore.Domain.Tables
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public double Price { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public int CategoryRef { get; set; }
    }
}

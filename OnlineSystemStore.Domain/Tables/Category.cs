using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace OnlineSystemStore.Domain.Tables
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
      
    }
}

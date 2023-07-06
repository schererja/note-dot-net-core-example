

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eden.Core.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonPropertyName("customerNumber")]
        public string? cust_num { get; set; }
        [JsonPropertyName("customerName")]
        public string? cust_name { get; set; }
    }
}

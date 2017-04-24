using Newtonsoft.Json;
using ZangAPI.Helpers;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Usage
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Usage : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [JsonProperty(PropertyName = "product")]
        [JsonConverter(typeof(ProductConverter))]

        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [JsonProperty(PropertyName = "product_id")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        [JsonProperty(PropertyName = "month")]
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the average cost.
        /// </summary>
        /// <value>
        /// The average cost.
        /// </value>
        [JsonProperty(PropertyName = "average_cost")]
        public decimal AverageCost { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        /// <value>
        /// The total cost.
        /// </value>
        [JsonProperty(PropertyName = "total_cost")]
        public decimal TotalCost { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieManager.BLL.Entities
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }

        public string Director { get; set; }

        public string MovieKind { get; set; }

        public string Description { get; set; }

        [Range(1, 360, ErrorMessage = "Movie must be between 1 to 260 minutes")]
        public int Duration { get; set; }

        public int AgeGroup { get; set; }

        [JsonIgnore]
        public virtual Rental Rentals { get; set; }

        public double RentalValue { get; set; }
    }
}

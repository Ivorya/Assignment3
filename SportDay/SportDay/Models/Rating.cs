namespace SportDay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        public int RatingId { get; set; }

        [Required]
        public string common { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int EventId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Event Event { get; set; }
    }
}

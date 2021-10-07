namespace SportDay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public string Info { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int EventId { get; set; }

        public DateTime Date { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Event Event { get; set; }
    }
}

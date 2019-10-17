using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtTracker.Entities
{
    public class Debt
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Concept { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime PreviousDate { get; set; }

        public DateTime NextDate { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        public Guid PersonId { get; set; }
    }
}

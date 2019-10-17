using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Entities
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<Debt> Debts { get; set; } 
            = new List<Debt>();
    }
}

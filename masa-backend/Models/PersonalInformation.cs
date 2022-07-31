﻿using System.ComponentModel.DataAnnotations;

namespace masa_backend.Models
{
    public class PersonalInformation : BaseEntity
    {
        private string nationalCode = default!;
        private readonly string lastName = default!;
        [Required]
        [StringLength(10)]
        public string NationalCode { get => nationalCode; set => nationalCode = value; }
        public string? FatherFirstName { get; set; }
        public string? FatherLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? FirstName { get; set; }
        [Required]
        public string LastName { get => lastName; set => nationalCode = value; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public City? City { get; set; }
        public Province? Province { get; set; }
        public Country? Country { get; set; }
        [Required]
        [StringLength(11)]
        public string Mobile { get; set; } = default!;
        public User User { get; set; }
        public PersonalInformation()
        {
            City = new City();
            Country = new Country();
            Province = new Province();
            User = new User();
        }
    }
}

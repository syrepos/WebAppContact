using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactWebApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(ContactWebConstants.MAX_FIRST_NAME_LENGTH)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        [StringLength(ContactWebConstants.MAX_LAST_NAME_LENGTH)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Adress")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone is required")]
        [Phone(ErrorMessage ="Invalid Phone number")]
        [StringLength(ContactWebConstants.MAX_PHONE_LENGTH)]
        public string PhonePrimary { get; set; }

        [Phone(ErrorMessage = "Invalid Phone number")]
        [StringLength(ContactWebConstants.MAX_PHONE_LENGTH)]
        public string PhoneSecondary { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [StringLength(ContactWebConstants.MAX_STREETADRESS_LENGTH)]
        public string StreetAdress1 { get; set; }
        [StringLength(ContactWebConstants.MAX_STREETADRESS_LENGTH)]
        public string StreetAdress2 { get; set; }

        [Required(ErrorMessage ="City is required")]
        [StringLength(ContactWebConstants.MAX_CITY_LENGTH)]
        public string City { get; set; }

        [Required(ErrorMessage ="State is required")]
        public int StateId { get; set; }

        public virtual State State { get; set; }

        [Required(ErrorMessage ="Zip code is required")]
        [Display(Name ="Zip Code")]
        [StringLength(ContactWebConstants.MAX_ZIPCODE_LENGTH, MinimumLength =ContactWebConstants.MIN_ZIPCODE_LENGTH)]

        public string Zip { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser  User { get; set; }

        [Display(Name ="Full Name")]
        public string FriendlyName => $"{FirstName} {LastName}"; 
        [Display(Name ="Full Address")]
        public string FreindlyAdress => string.IsNullOrWhiteSpace(StreetAdress2) ?
            $"{StreetAdress1},{City},{State.Abbreviation}, {Zip}"
            :$"{StreetAdress1}, {StreetAdress2},{City}, {State.Abbreviation}"
            ; 

    }
}

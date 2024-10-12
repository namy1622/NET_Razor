using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace razorwebapp_sql.Models{
    public class AppUser : IdentityUser{
        [Column(TypeName= "nvarchar")]
        [StringLength(400)]
        public string? HomeAddress {set; get;}

       // [Required]
        [DataType(DataType.Date)]
        public DateTime? BirthDate {get; set;}

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentorship.Models;

public partial class User
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }


    [Required]
    [StringLength(100)]
    [Display(Name = "Username")]
    public string? Username { get; set; }

    [Required]
    [StringLength(100)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }



    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 50 characters")]
    [Display(Name = "First Name")]
    public string? first_name { get; set; }


    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 50 characters")]
    [Display(Name = "Last Name")]
    public string? last_name { get; set; }

    [Required]
    [StringLength(100)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    public string? ProfilePicture { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Mentee> Mentees { get; set; } = new List<Mentee>();

    public virtual ICollection<Mentor> Mentors { get; set; } = new List<Mentor>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
}

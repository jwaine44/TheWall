#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TheWall.Models;

public class User
{
    [Key]
    public int UserId {get;set;}

    [Required(ErrorMessage = "First name is required!")]
    [Display(Name = "First Name:")]
    public string FirstName {get;set;}

    [Required(ErrorMessage = "Last name is required!")]
    [Display(Name = "Last Name:")]
    public string LastName {get;set;}

    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress]
    public string Email {get;set;}

    [Required(ErrorMessage = "Password is required!")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters!")]
    [DataType(DataType.Password)]
    [Display(Name = "Password:")]
    public string Password {get;set;}

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Doesn't match password!")]
    [Display(Name = "Confirm Password:")]
    public string ConfirmPassword {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    // Navigation property for related Message objects
    public List<Message> CreatedMessages {get;set;} = new List<Message>();

    public List<UserMessageComment> Comments {get;set;} = new List<UserMessageComment>();

    public string FullName()
    {
        return FirstName + " " + LastName;
    }
}
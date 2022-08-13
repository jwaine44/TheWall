#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class Message
{
    [Key]
    public int MessageId {get;set;}

    [Required(ErrorMessage = "Message can't be empty!")]
    [Display(Name = "Post a message")]
    public string Msg {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    // Foreign key to User
    public int UserId {get;set;}

    // Navigation property to User
    public User? Poster {get;set;}

    // Navigation property to Comment
    public List<UserMessageComment> Comments {get;set;} = new List<UserMessageComment>();
}
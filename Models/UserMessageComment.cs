#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class UserMessageComment
{
    [Key]
    public int UserMessageCommentId {get;set;}

    [Display(Name = "Post a comment")]
    public string Comment {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;

    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    // Navigation properties

    public int UserId {get;set;}

    public User? User {get;set;}

    public int MessageId {get;set;}

    public Message? Message {get;set;}
}
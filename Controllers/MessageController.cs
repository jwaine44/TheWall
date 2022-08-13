using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;

namespace TheWall.Controllers;

public class MessageController : Controller
{
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("uid");
        }
    }

    private bool loggedIn
    {
        get
        {
            return uid != null;
        }
    }

    private TheWallContext _context;

    public MessageController(TheWallContext context)
    {
        _context = context;
    }

    [HttpGet("/wall")]
    public IActionResult ShowWall()
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "User");
        }

        List<Message> AllMessages = _context.Messages
            .Include(msg => msg.Poster)
            .Include(msg => msg.Comments)
            .OrderByDescending(msg => msg.UpdatedAt)
            .ToList();

        return View("ShowWall", AllMessages);
    }

    [HttpPost("/message/create")]
    public IActionResult Create(Message newMessage)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "User");
        }

        if(ModelState.IsValid == false)
        {
            return ShowWall();
        }

        if(uid != null)
        {
            newMessage.UserId = (int)uid;
        }

        _context.Messages.Add(newMessage);
        _context.SaveChanges();

        return RedirectToAction("ShowWall");
    }

    [HttpPost("/message/delete/{deletedMessageId}")]
    public IActionResult Delete(int deletedMessageId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "User");
        }

        Message? msgToDelete = _context.Messages.FirstOrDefault(msg => msg.MessageId == deletedMessageId);

        if(msgToDelete != null)
        {
            if(msgToDelete.UserId == uid)
            {
                _context.Messages.Remove(msgToDelete);
                _context.SaveChanges();
            }
        }

        return RedirectToAction("ShowWall");
    }

    [HttpPost("/message/{msgId}/comment")]
    public IActionResult Comment(int msgId, string comment)
    {
        if(!loggedIn || uid == null)
        {
            return RedirectToAction("Index", "User");
        }

        UserMessageComment newComment = new UserMessageComment(){
            MessageId = msgId,
            Comment = comment,
            UserId = (int)uid
        };

        _context.UserMessageComments.Add(newComment);
        _context.SaveChanges();
        return RedirectToAction("ShowWall");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

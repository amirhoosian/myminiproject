using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using newminiproject2.Models;
using newminiproject2.ViewModle;

namespace newminiproject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : Controller
    {
        private readonly myminiprojectContext _context;

        public MessagesController(myminiprojectContext context)
        {
            _context = context;
        }

        // POST 
        [HttpPost]
        public void Post(MessagesViewModle ViewModle)
        {
            Message newMessage = new()
            {
                SenderPageId = ViewModle.SenderPageId,
                Message1 = ViewModle.Message1
            };
            _context.Add(newMessage);
            _context.SaveChanges();
        }



        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var chat = _context.Messages.Find(id);
            _context.Remove(chat);
            _context.SaveChanges();
        }
    }
}
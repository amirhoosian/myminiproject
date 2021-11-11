using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newminiproject2.Interface;
using newminiproject2.Models;
using newminiproject2.ViewModle;

namespace newminiproject2.Services
{
    public class IChatServices : Ichat



    {

        private myminiprojectContext _context;

        public IChatServices(myminiprojectContext context)
        {
            _context = context;
        }

        public void Add(ChatViewModle vm)
        {
            Chat newChat = new()
            {
                SenderPageId = vm.SenderPageId,
                ContactPageId = vm.ContactPageId

            };

            _context.Add(newChat);
            _context.SaveChanges();


        }

        public void Delete(int id)
        {
            _context.Messages.RemoveRange(_context.Messages.Where(x => x.ChatId == id));
            _context.Chats.Remove(_context.Chats.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
        }
    }
}
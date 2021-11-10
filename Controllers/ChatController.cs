using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using newminiproject2.Interface;
using newminiproject2.ViewModle;

namespace newminiproject2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private Ichat chat;

        public ChatController(Ichat chat)
        {
            this.chat = chat;
        }


        [HttpPost]
        public IActionResult Add(ChatViewModle vm)
        {

            chat.Add(vm);

            return Ok(vm);

        }






    }
}
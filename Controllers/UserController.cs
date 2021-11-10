using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using newminiproject2.Models;
using newminiproject2.ViewModle;

namespace newminiproject2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private myminiprojectContext _context;

        public UserController(myminiprojectContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(UserViewModle vm)
        {

            if (vm.Password != vm.RePassword)
            {
                return BadRequest("something has gone wrong!");
            }

            var UserName = _context.Users.SingleOrDefault(u => u.UserName == vm.UserName);
            if (UserName != null)
            {
                return BadRequest("the user name you send is allraedy exits");
            }


            User newUser = new()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName,
                Password = vm.Password,
                RePassword = vm.RePassword
            };

            _context.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }



    }
}
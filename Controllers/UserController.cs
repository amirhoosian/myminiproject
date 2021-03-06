using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using newminiproject2.Models;
using newminiproject2.ViewModle;
using Microsoft.AspNetCore.Authorization;

namespace newminiproject2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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

        // [HttpPost]
        // public bool Login(UserViewModle vm)
        // {
        //     var usr = _context.Users.SingleOrDefault(u => u.UserName == vm.UserName && u.Password == vm.Password);
        //     if (usr != null)
        //     {
        //         var tokenHandler = new JwtSecurityTokenHandler();
        //         var tokenKey = Encoding.ASCII.GetBytes(_key);


        //         var tokenDescriptor = new SecurityTokenDescriptor()
        //         {
        //             Subject = new ClaimsIdentity(
        //                 new Claim[]
        //                 {
        //                 new Claim(ClaimTypes.Name, vm.UserName)
        //                 }),
        //             Expires = DateTime.UtcNow.AddHours(1),
        //             SigningCredentials = new SigningCredentials(
        //                 new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        //         };

        //         var token = tokenHandler.CreateToken(tokenDescriptor);

        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }


        [HttpPost]
        public IActionResult Login(UserViewModle vm)
        {
            var usr = _context.Users.SingleOrDefault(u => u.UserName == vm.UserName && u.Password == vm.Password);
            if (usr != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(Authentication(usr.UserName, usr.Password, usr.Id));


                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                        new Claim(ClaimTypes.Name, vm.UserName)
                     }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return BadRequest("User name or password is wrong");
            }



        }
        // private string Authentication(string username, string password)
        // {
        //     if (!(username.Equals(username) || password.Equals(password)))
        //     {
        //         return null;
        //     }

        //     // 1. Create Security Token Handler
        //     var tokenHandler = new JwtSecurityTokenHandler();

        //     // 2. Create Private Key to Encrypted
        //     var tokenKey = Encoding.ASCII.GetBytes("asaaaaaaaaaaaaaa");

        //     //3. Create JETdescriptor
        //     var tokenDescriptor = new SecurityTokenDescriptor()
        //     {
        //         Subject = new ClaimsIdentity(
        //             new Claim[]
        //             {
        //                 new Claim(ClaimTypes.Name, username),
        //                 // new Claim(ClaimTypes.NameIdentifier, userid)
        //                 //  new Claim(ClaimTypes.NameIdentifier, User.id.Tostring())

        //             }),
        //         Expires = DateTime.UtcNow.AddHours(1),
        //         SigningCredentials = new SigningCredentials(
        //             new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     //4. Create Token
        //     var token = tokenHandler.CreateToken(tokenDescriptor);

        //     // 5. Return Token from method
        //     return tokenHandler.WriteToken(token);
        // }


        private string Authentication(string username, string password, int Id)
        {
            if (!(username.Equals(username) || password.Equals(password)))
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes("asaaaaaaaaaaaaaa");

            //Find User For Login
            var user = _context.Users.FirstOrDefault(c => c.UserName == username);
            if (user == null)
            {
                //Return Error in Api ... You Can retrun Null
                return null;
            }

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.NameIdentifier, Id.ToString())

                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }


        // [HttpPut]

        // public IActionResult ChangePassword(UserViewModle vim)
        // {
        //     // var identity = HttpContext.User.Identity as ClaimsIdentity;
        //     // if (identity != null)
        //     // {
        //     //     IEnumerable<Claim> claims = identity.Claims;
        //     //     // or
        //     //     identity.FindFirst(ClaimsType.NameIdentifire).Value;

        //     // }
        //     var userId = 0;
        //     var identity = HttpContext.User.Identity as ClaimsIdentity;
        //     if (identity != null)
        //     {
        //         userId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
        //     }
        //     // //Return string
        //     // var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //     // var userId = int.Parse( User.FindFirst(ClaimsType.NameIdentifire));
        //     var user = _context.Users.SingleOrDefault(c => c.Id == userId);

        //     if (user.Password == vim.Password)
        //     {
        //         user.Password = vim.NewPassword;
        //         _context.SaveChanges();


        //     }
        //     return Ok();
        // }


        // [Authorize]
        [HttpPut]

        public IActionResult ChangePassword(UserViewModle vim)
        {
            //var UserName = User.FindFirstValue(ClaimTypes.Name);
            // var user = _context.Users.SingleOrDefault(c => c.UserName == UserName);
            var user = _context.Users.FirstOrDefault(c => c.UserName == vim.UserName);

            if (user.Password == vim.Password)
            {
                user.Password = vim.NewPassword;
                _context.SaveChanges();


            }
            return Ok();
        }

        // [HttpPut]

        // public IActionResult ChangePassword(UserViewModle vim)
        // {
        //     var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //     var user = _context.Users.FirstOrDefault(c => c.Id == userId);
        //     return Ok();


        // }





    }
}
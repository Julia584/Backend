using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FarmerScheme.Models;

namespace TpConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly ProjectGladiatorContext _context;

        public Authentication(ProjectGladiatorContext context)
        {
            _context = context;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(Admin userdetails)
        {
            Admin ud = _context.Admins.Where(u => u.EmailId == userdetails.EmailId).FirstOrDefault();
            // Admin ud = await _context.Admins.FindAsync(userdetails.EmailId);

            Dictionary<string, string> status = new Dictionary<string, string>();

            if (ud == null)
            {
                status.Add("LoginMessage", "UserDoesNotExist");
                return Ok(status);
            }
            else
            {
                if (ud.Password == ComputeSha256Hash(userdetails.Password))
                {
                    status.Add("LoginMessage", "Success");
                }
                else
                {
                    status.Add("LoginMessage", "InvalidPassword");
                }
                return Ok(status);
            }


        }


        [HttpPost("Login1")]
        public async Task<IActionResult> Login1(FarmerBidder farmer)
        {
            FarmerBidder ud = _context.FarmerBidders.Where(u => u.EmailId == farmer.EmailId).FirstOrDefault();
            //Customer ud = await context.Customer.FindAsync(userdetails.Email);

            Dictionary<string, string> status = new Dictionary<string, string>();

            if (ud == null)
            {
                status.Add("LoginMessage", "UserDoesNotExist");
                return Ok(status);
            }
            else
            {
                if (ud.Password == ComputeSha256Hash(farmer.Password))
                {
                    status.Add("LoginMessage", "Success");
                }
                else
                {
                    status.Add("LoginMessage", "InvalidPassword");
                }
                return Ok(status);
            }
        }


        [NonAction]
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256  
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string  
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

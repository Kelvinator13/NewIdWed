using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Models.Services
{
    public class GuestModel
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        guest.IdentityUserId = userId;
        _context.Add(guest);
        _context.SaveChanges();

        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var guest = _context.Guests.Where(c => c.IdentityUserId == userId).SingleOrDefault();
    }
}

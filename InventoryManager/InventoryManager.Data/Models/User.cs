namespace InventoryManager.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public List<Clothes> Clothes { get; set; } = new List<Clothes>();
    }
}

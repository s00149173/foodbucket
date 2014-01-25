

namespace FoodBucket
{
    using System;
    using System.Collections.Generic;
    
    public partial class System_Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.DateTime RegDate { get; set; }
        public string Email { get; set; }
    }
}

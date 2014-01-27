
namespace FoodBucket
{
    using System;
    using System.Collections.Generic;
    
    public partial class System_Users
    {
        public System_Users()
        {
           this.UserDatas = new HashSet<UserData>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.DateTime RegDate { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<UserData> UserDatas { get; set; }
    }
}

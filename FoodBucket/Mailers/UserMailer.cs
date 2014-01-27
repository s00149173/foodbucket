using FoodBucket.Models;
using Mvc.Mailer;

namespace FoodBucket.Mailers
{ 
    public sealed class UserMailer : MailerBase, IUserMailer
    {
        public RegisterModel User;
        public UserMailer(RegisterModel newUser)
		{
			MasterName="_Layout";
            User = newUser;
		}
		
		public MvcMailMessage Welcome()
		{
			ViewBag.Data = User;
			return Populate(x =>
			{
				x.Subject = "Welcome to the Foodbucket";
				x.ViewName = "Welcome";
				x.To.Add(User.Email);
			});
		}

      
    }
}
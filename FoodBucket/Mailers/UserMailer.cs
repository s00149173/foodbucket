using FoodBucket.Models;
using Mvc.Mailer;

namespace FoodBucket.Mailers
{ 
    public sealed class UserMailer : MailerBase, IUserMailer
    {
        public RegisterModel User;
        public Recipies recipie;
        public string email;

        public UserMailer(RegisterModel newUser)
		{
			MasterName="_Layout";
            User = newUser;
		}

        public UserMailer(Recipies rec, string mail)
        {
            recipie = rec;
            email = mail;
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

        public MvcMailMessage newRecipie()
        {
            ViewBag.rec = recipie;
                return Populate(x =>
            {
                x.Subject = "A new recipie was added in FoodBucket";
                x.ViewName = "newRecipie";
                x.To.Add(email);
            });
        }
      
    }
}
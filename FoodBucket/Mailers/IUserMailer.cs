using Mvc.Mailer;

namespace FoodBucket.Mailers
{
    public interface IUserMailer
    {
        MvcMailMessage Welcome();
        MvcMailMessage newRecipie();
    }
}
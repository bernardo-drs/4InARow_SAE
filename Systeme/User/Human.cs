using Systeme.User;

namespace Systeme
{
    public class Human : Player
    {

        public Human(string userName) : base(userName)
        {
            Console.WriteLine($"User: {UserName} successfully created !");
        }

    }
}

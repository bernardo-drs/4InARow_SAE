namespace Systeme.User
{
    public class Player
    {
        private string _userName;

        public string UserName
        {
            set { this._userName = value; }
            get { return this._userName; }
        }
        public Player(string username)
        {
            this._userName = username;
        }
    }
}

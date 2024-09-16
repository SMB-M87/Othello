using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PlayerRequest
    {
        public string Username { get; set; }
        public string Sender { get; set; }

        public PlayerRequest(string username, string sender)
        {
            Username = username;
            Sender = sender;
        }
    }
}

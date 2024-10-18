namespace API.Models
{
    public class PlayerRequest
    {
        public string Receiver { get; set; }
        public string Sender { get; set; }

        public PlayerRequest(string receiver, string sender)
        {
            Receiver = receiver;
            Sender = sender;
        }
    }
}

namespace API.Models
{
    public class PlayerRequest
    {
        public string ReceiverUsername { get; set; }
        public string SenderToken { get; set; }

        public PlayerRequest()
        {
            ReceiverUsername = string.Empty;
            SenderToken = string.Empty;
        }

        public PlayerRequest(string receiver_username, string sender_token)
        {
            ReceiverUsername = receiver_username;
            SenderToken = sender_token;
        }
    }
}

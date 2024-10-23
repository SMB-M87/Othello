namespace API.Models
{
    public enum Inquiry { Friend = 0, Game = 1, None = 2 }

    public class Request
    {
        public Inquiry Type { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }

        public Request()
        {
            Type = Inquiry.None;
            Username = string.Empty;
            Date = DateTime.MinValue;
        }

        public Request(Inquiry type, string username)
        {
            Type = type;
            Username = username;
            Date = DateTime.UtcNow;
        }
    }
}

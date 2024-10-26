namespace API.Models
{
    public class ID
    {
        public string Token { get; set; }

        public ID()
        {
            Token = string.Empty;
        }

        public ID(string token)
        {
            Token = token;
        }
    }
}

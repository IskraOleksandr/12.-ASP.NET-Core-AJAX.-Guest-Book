namespace ASP.NET_Core_и_AJAX._Guest_Book.Models
{
    public class Message
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public string MessageText { get; set; }

        public DateTime MessageDate { get; set; }
    }
}

namespace Financify_Api.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Balance Balance { get; set; }
        public User User { get; set; }
    }
}

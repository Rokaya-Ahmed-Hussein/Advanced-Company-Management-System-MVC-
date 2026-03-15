namespace BL.ViewModels.User
{
    public class AccountReadVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? FullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}

namespace Tranning.Models
{
    public class LoginModel
    {
        public string UserID {  get; set; }
        public int RoleID { get; set; } 
        public string Username { get; set; }
        public string EmailUser {  get; set; }
        public string PhoneUser { get; set; }
        public string ExtraCode { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
    }
}

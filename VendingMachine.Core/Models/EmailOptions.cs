namespace VendingMachine.Core.Models
{
    public class EmailOptions
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Smtp { get; set; }
        public string Adress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string[] AdminsAddresses { get; set; }
    }
}

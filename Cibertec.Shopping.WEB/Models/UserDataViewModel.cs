namespace Cibertec.Shopping.WEB.Models
{
    public class UserDataViewModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}

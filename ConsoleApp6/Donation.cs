using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Donation
    {
        public Donation() { }

        #region Model
        //[Key]
        public int Id { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public string Password { get; set; }
        public string BloodGroup { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsLogin { get; set; } = false;
        //public string Role { get; set; } 
        #endregion


    }
}

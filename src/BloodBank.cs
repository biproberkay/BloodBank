using BloodBankb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank
{
    public class BloodBank
    {
        List<Donation> donations;
        public Donation AppUser ;
        public BloodBank()
        {
            donations = new List<Donation>();
            if (IsEmpty())
            {
                Seed();
            }
            else
            {
                ReadDb();
            }
        }
        void DonationInsert()
        {
            Donation donation = new Donation();
            Console.WriteLine("\t\t Donorün");
            donation.Id = donations.Count()+1;
            Console.Write("Adı: ");
            Console.ReadLine();
            donation.DonorFirstName = Console.ReadLine().ToUpper();
            Console.Write("SoyAdı: ");
            donation.DonorLastName = Console.ReadLine().ToUpper();
            Console.Write("Parola: ");
            donation.Password = Console.ReadLine();
            Console.Write("Kan Grubu: ");
            donation.BloodGroup = Console.ReadLine().ToUpper();
            donation.IsLogin = true;
            donations.Add(donation);
            AppUser = donation;
            SaveDonation(); 
        }
        void DonationUpdate()
        {
            Donation donation;
            Console.ReadLine();
            View("Güncellemek istediğiniz Kan bağışının Kayıt numarasını Giriniz.");
            int id = Convert.ToInt32( Console.ReadLine());
            donation = donations.Find(d => d.Id == id);
            View("Adı: "+donation.DonorFirstName + " \n" +
                "SoyAdı: " + donation.DonorLastName + " \n" +
                "Kan Grubu: " + donation.BloodGroup + " ");
            Console.Write("Adı: ");
            donation.DonorFirstName = Console.ReadLine().ToUpper();
            Console.Write("SoyAdı: ");
            donation.DonorLastName = Console.ReadLine().ToUpper();
            Console.Write("Kan Grubu: ");
            donation.BloodGroup = Console.ReadLine().ToUpper();
            SaveDonation();
        }
        void DonationDelete()
        {
            Donation donation;
            Console.ReadLine();
            View("Silmek istediğiniz Kan bağışının \n Kayıt numarasını Giriniz.");
            Console.Write("Değer gir: ");
            int id = Convert.ToInt32(Console.ReadLine());
            donation = donations.Find(d => d.Id == id);
            Console.Write("silmek istediğine emin misin e/h?");
            char choose;
            choose = (char)Console.Read();
            switch (choose)
            {
                case 'e':
                case'E':
                    donations.Remove(donation);
                    File.Delete("BloodBank.txt");
                    SaveDonation();
                    break;
                case 'h':
                case 'H':
                    Menu();
                    break;
                default:
                    Menu();
                    break;
            }
        }
        void BloodSearch()
        {
            Console.Clear();
            View("aramak istediğiniz kan grubunu Listeden seçin " +
                "\n 1.A RH(+) " +
                "\n 2.A RH(-) " +
                "\n 3.B RH(+) " +
                "\n 4.B RH(-) " +
                "\n 5.AB RH(+) " +
                "\n 6.AB RH(-) " +
                "\n 7.0 RH(+) " +
                "\n 8.0 RH(-)");
            Console.ReadLine();
            Console.Write("Bir değer girin:");
#if true
            string bloodSearch = Console.ReadLine().ToUpper();
            List<Donation> searchResults = donations.ToList().FindAll(b => b.BloodGroup == bloodSearch);
#else
            char bloodSearch = (char)Console.Read();
            switch (bloodSearch)
            {
                case '1':
                    break;
                case '2':
                    break;
                case '3':
                    break;
                case '4':
                    break;
                case '5':
                    break;
                case '6':
                    break;
                case '7':
                    break;
                case '8':
                    break;
                default:
                    break;
            }
            string[] groups = new string[]{ "A+", "B+", "AB+", "0+", "A-", "B-", "AB-", "0-" };
            List<Donation> searchResults = donations.ToList().FindAll(b => b.BloodGroup == groups);
#endif
            foreach (var item in searchResults)
            {
                Console.WriteLine(
                    item.Id + " "+
                    item.DonorFirstName + " " +
                    item.DonorLastName + " " +
                    item.BloodGroup + "\n" 
                    );
            }
            Console.WriteLine("Bir tuşa basın");
            Console.ReadKey();
        }
        void DonationGetAll() 
        { 

        }
        void ReadDb() // db yi okuyup ram e atar
        {
            using (FileStream fileStream = new FileStream("BloodBank.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                string[] lines = new string[100]; // boş bi array - satır aracı
                StreamReader reader = new StreamReader(fileStream); //tabloyu okuyan tarayıcı
                int i = 0;
                while (!reader.EndOfStream) // veritabanı dosyasını sonuna kadar satır satır oku!
                {
                    lines[i] = reader.ReadLine(); // burdaki array ile tabloyu satır satır okutuyoruz. satır okuyucu - tablo satırları dizisi elde ediyoruz.
                    string[] col = lines[i].Split(' '); //bunlar sütunlar - burda satırı sütun sütun okutuyoruz. sütun okuyucu - satır aracının sütün alt aracı
                    donations.Add(
                        new Donation()
                        {
                            Id = Convert.ToInt32(col[0]),
                            DonorFirstName = col[1],
                            DonorLastName = col[2],
                            Password = col[3],
                            IsAdmin = Convert.ToBoolean(col[4]),
                            IsLogin = Convert.ToBoolean(col[5]),
                            BloodGroup = col[6]
                        }
                    );
                    i++;
                }
                reader.Close();
            }
        }
        bool IsEmpty()
        {
            using (FileStream fileStream = new FileStream("BloodBank.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamReader reader = new StreamReader(fileStream); //tabloyu okuyan tarayıcı
                bool result = reader.EndOfStream;
                reader.Close();
                return result;                
            }
        }
        void SaveDonation() // bu aslında save() metodu
        {
            FileStream fileStream = new FileStream("BloodBank.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(fileStream);
            foreach (var item in donations)
            {
                if (item!=null)
                {
                    writer.WriteLine( /* dosyada bir satıra yazıyor */
                                item.Id + " " +
                                item.DonorFirstName + " " +
                                item.DonorLastName + " " +
                                item.Password + " " +
                                item.IsAdmin + " " +
                                item.IsLogin + " " +
                                item.BloodGroup
                                );
                }
            }
            writer.Close();

        }
        void Seed()
        {
            donations.Add( new Donation{
                Id = 1,
                DonorFirstName = "Şükrü",
                DonorLastName = "Berkay",
                IsAdmin = true,
                Password = "12345",
                IsLogin = false,
                BloodGroup = "A+"
            });
            SaveDonation();
        }
        void Login()
        {
            Donation donor;
            View("Kullanıcı Girişi: " +
                "\n admin kullanıcı numarası: 1 şifresi: 12345");
            Console.Write("Kullanıcı Numaranı Gir: ");
            Console.ReadLine();
            int iId = Convert.ToInt32(Console.ReadLine());
            donor = donations.Find(d => d.Id == iId);
            Console.Write("Parolanı Gir:  ");
            string inputPassword = Console.ReadLine();   
            
            if (donor==null)
            {
                View("Sisteme kayıtlı değilsin! sisteme kan vererek kayıt ol! \n e/h ?");
                char choose;
                Console.Write("Lütfen Seçiminizi Giriniz:");
                choose = (char)Console.Read();
                switch (choose)
                {
                    case 'e':
                    case 'E':
                        DonationInsert();
                        break;
                    case 'h':
                    case 'H':
                        GuestMenu();
                        break;
                    default:
                        GuestMenu();
                        break;
                }
                AppUser = donor;
            }
            else
            {
                if (inputPassword == donor.Password)
                {
                    if (donor.Id == 1)
                    {
                            donor.IsLogin=true;
                        //Console.ForegroundColor = ConsoleColor.Green;
                        View(" Buyur Admin! \n Menüye devam etmek için\n bir tuşa bas ");
                        Console.ReadKey();
                        AppUser = donor;
                    }

                    else
                    {
                            donor.IsLogin=true;
                        //Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        View(" Buyur Kullanıcı! \n Menüye devam etmek için\n bir tuşa bas ");
                        Console.ReadKey();
                        AppUser = donor;
                    }
                }
                else
                {
                        donor.IsLogin=false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("yeniden girmeye çabala...");
                    Console.WriteLine("\nAna Menü İçin Bir Tuşa Basın...");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        void View(string v)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             Yıldız Teknik Kan Bankasına Hoşgeldiniz     x.Çıkış║");
            Console.WriteLine("║_______________________________**_______________________________║");
            Console.WriteLine("║                                                 1.Giriş 2.Kayıt║");
            Console.WriteLine("║________________________________________________________________║");
            Console.WriteLine("║                                                                ║");
            string border =   "╔════════════════════════════════════════════════════════════════╗";
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] lines = v.Split('\n');
            foreach (var line in lines)
            {
                string space = new string(' ', (border.Length - 2 - line.Length) / 2);
                string line1 = $"║{space}{line}{space} ║";
                string line2 = $"║{space}{line}{space}║";
                if (line.Length % 2 != 0)
                {
                    Console.WriteLine(line1);
                }
                else
                {
                    Console.WriteLine(line2);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║_______________________________**__________Evet: E Hayır: H_____║");
            Console.WriteLine("║                            7.Hakkımızda 8.Gizlilik 9.İletişim  ║");
            Console.WriteLine("║________________________________________________________________║");

        }
        public void GuestMenu() 
        {
            View("Misafir MENUsü\n" + "Giriş yap!\n ya da\n"+"Kayıt ol. Kan ver!");
            char secim;
            Console.Write("Lütfen Seçiminizi Giriniz:");

            secim = (char)Console.Read();

            switch (secim)
            {
                case '1':
                    Login();
                    break;
                case '2':
                    View("kanbağışı ekleyince kaydolmuş oluyorsun zaten.");
                    Console.WriteLine("\nAna Menü İçin Bir Tuşa Basın...");
                    Console.ReadKey();
                    DonationInsert();
                    break;
                case '7':
                    About();
                    break;
                case '8':
                    Privacy();
                    break;
                case '9':
                    Contact();
                    break;
                case 'e':
                case 'E':
                    break;
                case 'h':
                case 'H':
                    break;
                case 'x':
                case 'X':
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        public void Menu()
        {
            View("MENU\n"+ $"Merhaba :{AppUser.DonorFirstName} \n"
                +
                            " ║                                                          ║\n" + 
                            " ║     3.Kan Bağışı Getir                                   ║\n" + 
                            " ║     4.Kan Bağışı Yap!                                    ║\n" + 
                            " ║     5.Kan Bağışı Güncelle                                ║\n" + 
                            " ║     6.Kan Bağışı Sil                                     ║\n" 
                );
            char secim;
            Console.Write("Lütfen Seçiminizi Giriniz:");
            //Console.ReadLine();
            secim = (char)Console.Read();

            if (AppUser.IsAdmin)
            {

                switch (secim)
                {
                    case '1':
                        Login();
                        break;
                    case '2':
                        View("kaydolmak için kan bağışı yapınız!");
                        Console.WriteLine("\nAna Menü İçin Bir Tuşa Basın...");
                        Console.ReadKey();
                        break;
                    case '3':
                        BloodSearch();
                        break;
                    case '4':
                        DonationInsert();
                        Console.WriteLine("\nAna Menü İçin Bir Tuşa Basın...");
                        Console.ReadKey();
                        break;
                    case '5':
                        DonationUpdate();
                        break;
                    case '6':
                        DonationDelete();
                        break;
                    case '7':
                        About();
                        break;
                    case '8':
                        Privacy();
                        break;
                    case '9':
                        Contact();
                        break;
                    case 'e':
                    case 'E':
                        break;
                    case 'h':
                    case 'H':
                        break;
                    case 'x':
                    case 'X':
                        AppUser.IsLogin = false;
                        SaveDonation();
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
            else if (AppUser.IsLogin)
            {
                switch (secim)
                {
                    case '1':
                        Login();
                        break;
                    case '2':
                        View("kaydolmak için kan bağışı yapınız!");
                        break;
                    case '3':
                        BloodSearch();
                        break;
                    case '4':
                        DonationInsert();
                        Console.WriteLine("\nAna Menü İçin Bir Tuşa Basın...");
                        Console.ReadKey();
                        break;
                    case '5':
                        DonationUpdate();
                        break;
                    case '6':
                        View("Admin değilsin \n Menüye dönmek için bir tuşa bas!");
                        Console.ReadKey();
                        
                        break;
                    case '7':
                        About();
                        break;
                    case '8':
                        Privacy();
                        break;
                    case '9':
                        Contact();
                        break;
                    case 'e':
                    case 'E':
                        break;
                    case 'h':
                    case 'H':
                        break;
                    case 'x':
                    case 'X':
                        AppUser.IsLogin = false;
                        SaveDonation();
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
            else if(AppUser==null)
            {
                Console.WriteLine("Bişeyler ters gitti");
                Console.ReadKey();
            }
        }
        void About()
        {
            View("Şükrü Berkay 20091703");
            Console.WriteLine("Ana Sayfaya gitmek için bir tuşa bas!");
            Console.ReadKey();
        }
        void Privacy()
        {
            View("KVKK");
            Console.WriteLine("Ana Sayfaya gitmek için bir tuşa bas!");
            Console.ReadKey();
        }
        void Contact()
        {
            View("https//github.com/biproberkay");
            Console.WriteLine("Ana Sayfaya gitmek için bir tuşa bas!");
            Console.ReadKey();

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class BloodBank
    {
        List<Donation> donations;
        Donation AppUser ;
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
            AppUser = Login();
        }
        void DonationInsert()
        {
            Donation donation = new Donation();
            Console.ReadLine();
            Console.WriteLine("\t\t Donorün");
            donation.Id = donations.Count()+1;
            Console.Write("Adı: ");
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
            string bloodSearch = Console.ReadLine().ToUpper();
            List<Donation> searchResults = donations.ToList().FindAll(b => b.BloodGroup == bloodSearch);
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
        void DonationGetAll() { }
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
        Donation Login()
        {
            Donation donor;
            View("Kullanıcı Girişi: ");
            Console.Write("Kullanıcı Numaranı Gir: ");
            int id = Convert.ToInt32(Console.ReadLine());
            donor = donations.Find(d => d.Id == id);
            Console.Write("Parolanı Gir:  ");
            string inputPassword = Console.ReadLine();   
            
            if (donor==null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("boş kabul etmiyoruz !! sisteme kan ver kayıt ol!");
                DonationInsert();
                return AppUser;
            }
            else
            {
                if (inputPassword == donor.Password)
                {
                    if (donor.Id == 1)
                    {
                            donor.IsLogin=true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        View(" Buyur Admin Menüye devam etmek için\n bir tuşa bas ");
                        Console.ReadKey();
                        return donor;
                    }

                    else
                    {
                            donor.IsLogin=true;
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        View(" Buyur Kullanıcı Menüye devam etmek için\n bir tuşa bas ");
                        Console.ReadKey();
                        return donor;
                    }
                }
                else
                {
                        donor.IsLogin=false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("yeniden girmeye çabala...");
                    return donor;
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
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║                                                                ║");
            Console.WriteLine("║_______________________________**__________Evet: E Hayır: H_____║");
            Console.WriteLine("║                            7.Hakkımızda 8.Gizlilik 9.İletişim  ║");
            Console.WriteLine("║________________________________________________________________║");

        }

        public void Menu()
        {
            View("MENU\n"+
                            " ║                                                          ║\n" + 
                            " ║     3.Kan Ara                                            ║\n" + 
                            " ║     4.Kan Bağışı Ekle                                    ║\n" + 
                            " ║     5.Kan Bağışı Güncelle                                ║\n" + 
                            " ║     6.Kan Bağışı Sil                                     ║\n" 
                );
            char secim;
            Console.Write("Lütfen Seçiminizi Giriniz:");
            
            secim = (char)Console.Read();

            if (AppUser.IsAdmin)
            {

                switch (secim)
                {
                    case '1':
                        Login();
                        break;
                    case '2':
                        View("kanbağışı ekleyince kaydolmuş oluyorsun zaten.");
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
                        About("biz iyi bi takımız");
                        break;
                    case '8':
                        Privacy("kvkk");
                        break;
                    case '9':
                        Contact("Kayıt ekranına gidiyorsun");
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
                        View("kanbağışı ekleyince kaydolmuş oluyorsun zaten.");
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
                        About("biz iyi bi takımız");
                        break;
                    case '8':
                        Privacy("kvkk");
                        break;
                    case '9':
                        Contact("Kayıt ekranına gidiyorsun");
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

        void About(string message)
        {
            View(message);
        }
        void Privacy(string message)
        {

            View(message);
        }
        void Contact(string message)
        {
            View(message);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Packaging;

namespace YapayZekaProjesi
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        String[] gift_types = { "Spor","Giyim","Sanat","Edebiyat","Teknoloji","Ofis&Kırtasiye","Mutfak","Dekor"};
        String[] gifts = {"Suluk",
            "Spor Çantası",
            "Telefon Kolluğu",
            "Basketbol Topu",
            "Voleybol Topu",
            "Futbol Topu",
            "Forma",
            "Spor Kıyafetleri",
            "Mat",
            "Dumbell",
            "Tshirt",
            "Ayakkabı",
            "Ceket",
            "Pantalon",
            "Şapka",
            "Atkı",
            "Çanta",
            "Biblo",
            "Tablo",
            "Heykel",
            "Fırça",
            "Boya",
            "Boya Kalemi Seti",
            "Karakalem Seti",
            "Sketchbook",
            "Tuval",
            "Şövale",
            "Fantastik Romanlar",
            "Şiir Kitapları",
            "Tarihi Romanlar",
            "Dünya Edebiyatı Romanları",
            "Polisiye Romanları",
            "Dergi",
            "Eğlence&Mizah Kitapları",
            "Turizm&Gezi Kitapları",
            "Akıllı bileklik",
            "Kulaklık",
            "Mp3Çalar",
            "Oyun Konsolu",
            "Dijital Çerçeve",
            "Hoparlör",
            "Kamera",
            "Radyo",
            "Kalem",
            "Defter",
            "Dosya",
            "Masa Lambası",
            "Planlayıcı",
            "Ajanda",
            "Tava",
            "Tencere",
            "Bıçak Seti",
            "Tabak",
            "Bardak",
            "Mikser",
            "Meyve Suyu Sıkacağı",
            "Mum",
            "Çerçeve",
            "Ayna",
            "Çiçek",
            "Tablo",
            "Biblo",
        };
        String[] sales_types = { "Online Satış", "Magaza Satışı" };
        String[] Spor = {
            "Suluk",
            "Spor Çantası",
            "Telefon Kolluğu",
            "Basketbol Topu",
            "Voleybol Topu",
            "Futbol Topu",
            "Forma",
            "Spor Kıyafetleri",
            "Mat",
            "Dumbell"
        };

        String[] Kiyafet = {
            "Tshirt",
            "Ayakkabı",
            "Ceket",
            "Pantalon",
            "Şapka",
            "Atkı",
            "Çanta"
        };

        String[] Sanat = {
            "Biblo",
            "Tablo",
            "Heykel",
            "Fırça",
            "Boya",
            "Boya Kalemi Seti",
            "Karakalem Seti",
            "Sketchbook",
            "Tuval",
            "Şövale"
        };

        String[] Edebiyat = {
            "Fantastik Romanlar",
            "Şiir Kitapları",
            "Tarihi Romanlar",
            "Dünya Edebiyatı Romanları",
            "Polisiye Romanları",
            "Dergi",
            "Eğlence&Mizah Kitapları",
            "Turizm&Gezi Kitapları"
        };

        String[] Teknoloji = {
            "Akıllı bileklik",
            "Kulaklık",
            "Mp3Çalar",
            "Oyun Konsolu",
            "Dijital Çerçeve",
            "Hoparlör",
            "Kamera",
            "Radyo"
        };

        String[] Ofis_Kirtasiye = {
            "Kalem",
            "Defter",
            "Dosya",
            "Masa Lambası",
            "Planlayıcı",
            "Ajanda"
        };

        String[] Mutfak = {
            "Tava",
            "Tencere",
            "Bıçak Seti",
            "Tabak",
            "Bardak",
            "Mikser",
            "Meyve Suyu Sıkacağı"
        };

        String[] Dekor = {
            "Mum",
            "Çerçeve",
            "Ayna",
            "Çiçek",
            "Tablo",
            "Biblo"
        };

        public int store_quality = 2;
        public string type="Spor";
        public int price = 550;
        public string sales_type = "Online Satış";


        public Gift[] Population;
        int Population_Count = 50;
        int Generation_Count = 50;

        public MainWindow()
        {
            InitializeComponent();

            Gift[] gifts = new Gift[7440];
            var lines = File.ReadLines(@"C:\Users\User\source\repos\YapayZekaProjesi\YapayZekaProjesi\data.txt");
            int i = 0;
            foreach (var line in lines)
            {
                string[] gift_variables = line.Split('-');
                gifts[i] = new Gift((int)double.Parse(gift_variables[0]), gift_variables[1], gift_variables[2], (int)double.Parse(gift_variables[3]), gift_variables[4]);
                i++;
            }
            Generate_Initial_Population(gifts);
            Calculate_Fitness();
            Sort();
            Console.WriteLine("--------------------------- initial kromozom ------------------------------");
            for (int k = 0; k < Population.Length; k++)
            {
                Console.WriteLine(Population[k].fitness + "  |  " +Population[k].store_quality + " - " + Population[k].type + " - " + Population[k].name + " - " + Population[k].price + " - " + Population[k].sales_type);
            }
            for (int j = 0; j < Generation_Count; j++)
            {
                Cross_Over();
                Calculate_Fitness();
                Mutation();
                Sort();
                Console.WriteLine("--------------------------- " + j + ". jenerasyon ------------------------------");
                for (int k = 0; k < Population.Length; k++)
                {
                    Console.WriteLine(Population[k].fitness + "  |  " + Population[k].store_quality + " - " + Population[k].type + " - " + Population[k].name + " - " + Population[k].price + " - " + Population[k].sales_type);
                }
            }
        }

        public void Generate_Initial_Population(Gift[] gifts)
        {
            Random rnd = new Random();
            Population = new Gift[Population_Count];
            for (int i = 0; i < Population_Count; i++)
            {
                int random = rnd.Next(7439);
                Population[i] = gifts[random];
            }
        }

        public void Calculate_Fitness()
        {
            for (int i = 0; i < Population.Length; i++)
            {
                int m = Population[i].store_quality - store_quality;
                int t = Population[i].type.Equals(type) ? 1 : 0;
                int f = price > Population[i].price ? Population[i].price : 400 + 2 * (price - Population[i].price);
                int s = Population[i].sales_type.Equals(sales_type) ? 1 : 0;
                Population[i].fitness = 100 * m + 600 * t + f + 50 * s + 2000 * tutarliMi(Population[i]);
            }
        }

        private int tutarliMi(Gift gift)
        {
            switch (gift.type)
            {

                case "Spor":
                    for (int i = 0; i < Spor.Length; i++)
                        if (Spor[i].Equals(gift.name))
                            return 0;
                    break;
                case "Giyim":
                    for (int i = 0; i < Kiyafet.Length; i++)
                        if (Kiyafet[i].Equals(gift.name))
                            return 0;
                    break;
                case "Sanat":
                    for (int i = 0; i < Sanat.Length; i++)
                        if (Sanat[i].Equals(gift.name))
                            return 0;
                    break;
                case "Edebiyat":
                    for (int i = 0; i < Edebiyat.Length; i++)
                        if (Edebiyat[i].Equals(gift.name))
                            return 0;
                    break;
                case "Teknoloji":
                    for (int i = 0; i < Teknoloji.Length; i++)
                        if (Teknoloji[i].Equals(gift.name))
                            return 0;
                    break;
                case "Ofis&Kırtasiye":
                    for (int i = 0; i < Ofis_Kirtasiye.Length; i++)
                        if (Ofis_Kirtasiye[i].Equals(gift.name))
                            return 0;
                    break;
                case "Mutfak":
                    for (int i = 0; i < Mutfak.Length; i++)
                        if (Mutfak[i].Equals(gift.name))
                            return 0;
                    break;
                case "Dekor":
                    for (int i = 0; i < Dekor.Length; i++)
                        if (Dekor[i].Equals(gift.name))
                            return 0;
                    break;
            }
            return -1;
        }

        public void Sort()
        {
            Population = Population.OrderByDescending(c => c.fitness).ToArray();
        }

        public void Cross_Over()
        {
            Random rnd = new Random();
            for (int i = Population_Count * 10 / 100; i < Population.Length - Population_Count * 10 / 100+1; i++)
            {
                int random = rnd.Next(1, 4);
                Gift g2 = Population[i + 1];
                switch (random)
                {
                    case 1:
                        Population[i + 1].store_quality = Population[i].store_quality;
                        Population[i].store_quality = g2.store_quality;
                        break;
                    case 2:
                        Population[i + 1].type = Population[i].type;
                        Population[i].type = g2.type;
                        Population[i + 1].name = Population[i].name;
                        Population[i].name = g2.name;
                        break;
                    case 3:
                        Population[i + 1].price = Population[i].price;
                        Population[i].price = g2.price;
                        Population[i + 1].sales_type = Population[i].sales_type;
                        Population[i].sales_type = g2.sales_type;
                        break;
                    case 4:
                        Population[i + 1].sales_type = Population[i].sales_type;
                        Population[i].sales_type = g2.sales_type;
                        break;
                }
            }
        }

        public void Mutation()
        {
            Random rnd = new Random();

            for (int i = Population.Length- (Population_Count * 10 / 100+1); i < Population.Length; i++)
            {
                int random = rnd.Next(1, 5);
                switch (random)
                {
                    case 1:
                        int rand1 = rnd.Next(1, 3);
                        Population[i].store_quality = rand1;
                        break;
                    case 2:
                        int rand2 = rnd.Next(0, gift_types.Length - 1);
                        Population[i].type = gift_types[rand2];
                        break;
                    case 3:
                        int rand3 = rnd.Next(0, gifts.Length - 1);
                        Population[i].name = gifts[rand3];
                        break;
                    case 4:
                        int rand4 = rnd.Next(1, 20) * 50;
                        Population[i].price = rand4;
                        break;
                    case 5:
                        int rand5 = rnd.Next(0, 1);
                        Population[i].sales_type = sales_types[rand5];
                        break;
                }
                if (tutarliMi(Population[i]) == -1)
                {
                    i--;
                }
            }
        }
    }

    public class Gift
    {
        public int store_quality;
        public string type;
        public string name;
        public int price;
        public string sales_type;
        public int fitness;


        public Gift(int store_quality, string type, string name, int price, string sales_type)
        {
            this.store_quality = store_quality;
            this.type = type;
            this.name = name;
            this.price = price;
            this.sales_type = sales_type;
        }
    }
}

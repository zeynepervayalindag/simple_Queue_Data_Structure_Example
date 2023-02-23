using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class SimpleQueue
    {
        public Musteri [] musteri1;
        private int rear;
        private readonly int capacity;

        public SimpleQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new IndexOutOfRangeException("Dizinin kapasitesi pozitif bir tam sayı olmalıdır.");
            }
            this.capacity = capacity;
            musteri1 = new Musteri[capacity];
             rear = -1;
        }
        public void Enqueue(Musteri m)
        {
            if (IsFull())
            {
                throw new Exception("Banka kuyruğu doludur.");
            }
            musteri1[++rear] = m;
        }
        public Musteri Dequeue()
        {
            if (IsEmpty())
            {
                throw new Exception("Banka kuyruğu boştur.");
            }
            Musteri item = musteri1[0];
            for(int i=0;i<rear;i++)
            {
                musteri1[i] = musteri1[i+1];
            }
            --rear;
            return item;
        }
        public Musteri Peek()
        {
            if (IsEmpty())
            {
                throw new Exception("Banka kuyruğu boştur.");
            }
            return (Musteri)musteri1[0];
        }
        public int Size()
        {
            return rear + 1;
        }
        public bool IsFull()
        {
        return rear == capacity - 1;
        }
        public bool IsEmpty()
        {
        return rear == -1;
        }
        public void Clear()
        {
        rear = -1;
        }
        }

    class Musteri
    {
        private string tckimlikno;
        private string paramiktari;
        public Musteri(string tckimlikno,string paramiktari)
        {
            this.tckimlikno = tckimlikno;
            this.paramiktari = paramiktari;
        }
        public string Tckimlikno
        {
            set
                {
                tckimlikno = value;
            }
            get
                {
                return tckimlikno;
            }
        }

    public string Paramiktari
    {
        set
        {
            paramiktari = value;
        }
        get
        {
            return paramiktari;
        }
    }
}

internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bankamıza Hoşgeldiniz...");
            Console.Write("Lütfen bankamızın müşteri kapasitesini giriniz:");
            int girilenkapasite = Convert.ToInt32(Console.ReadLine());
            SimpleQueue nesne = new SimpleQueue(girilenkapasite);
            for (int i=0; ;i++ )
            {
                if ( girilenkapasite>i)
                {
                    Console.WriteLine("Yeni bir kayıt eklemek ister misiniz ?");
                    string cevap = Console.ReadLine();

                    if (cevap == "Evet")
                    {
                        Console.Write("Lütfen T.C. kimlik numaranızı giriniz:");
                        string tckimlikno = Console.ReadLine();
                        Console.Write("Lütfen hesabınızdaki para miktarını giriniz:");
                        string paramiktari = Console.ReadLine();
                        Musteri m = new Musteri(tckimlikno, paramiktari);
                        m.Tckimlikno = tckimlikno;
                        m.Paramiktari = paramiktari;
                        nesne.Enqueue(m);
                        Console.WriteLine("Herhangi bir işlem yapmak ister misiniz :");
                        string cevap2 = Console.ReadLine();
                        if (cevap2 == "Evet")
                        {
                            Console.WriteLine("Yapabileceğiniz işlemler aşağıda listelenmiştir.");
                            Console.WriteLine("1.Para Çekme");
                            Console.WriteLine("2.Para Yatırma");
                            Console.WriteLine("3.Eklenen ilk hesabın bilgilerini görüntülemek");
                            Console.WriteLine("4.Hesapları silmek");
                            Console.WriteLine("5.Oluşturulan hesap sayısını öğrenmek");
                            Console.Write("Hangi işlemi yapmak istersiniz:");
                            string cevap4 = Console.ReadLine();
                            try
                            {
                                if (cevap4 == "1")
                                {
                                    Console.Write("Lütfen çekilecek para miktarını giriniz:");
                                    double cekilecekmiktar = Convert.ToDouble(Console.ReadLine());
                                    if (Convert.ToDouble(m.Paramiktari) >= cekilecekmiktar)
                                    {
                                        Console.WriteLine("Hesabınızda kalan para miktarı {0} TL'dir.", Convert.ToDouble(m.Paramiktari) - cekilecekmiktar);
                                    }
                                    else
                                    {
                                        Console.Write("! Çekmek istediğiniz para miktarı hesabınızdaki para miktarından fazladır.Lütfen tekrar giriniz:");
                                        double cekilecekmiktar2 = Convert.ToDouble(Console.ReadLine());
                                        if (Convert.ToDouble(m.Paramiktari) >= cekilecekmiktar)
                                        {
                                            Console.WriteLine("Hesabınızda kalan para miktarı {0} TL'dir.", Convert.ToDouble(m.Paramiktari) - cekilecekmiktar2);
                                        }
                                        else
                                        {
                                            Console.WriteLine("! Çok sayıda hatalı giriş yaptınız.İyi günlerde tekrardan bankamıza bekleriz ...");
                                            break;
                                        }
                                    }
                                }
                                else if (cevap4 == "2")
                                {

                                    Console.Write("Lütfen yatırılacak para miktarını giriniz:");
                                    double yatırılacakmiktar = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("Hesabınızdaki güncel para miktarınız {0} TL'dir.", Convert.ToDouble(m.Paramiktari) + yatırılacakmiktar);

                                }
                                else if (cevap4 == "3")
                                {
                                    Console.WriteLine("Eklenen ilk hesabın T.C. kimlik numarası {0}, para miktarı {1}.", Convert.ToString(nesne.Peek().Tckimlikno), Convert.ToString(nesne.Peek().Paramiktari));
                                }
                                else if (cevap4 == "4")
                                {
                                    nesne.Clear();
                                }
                                else if (cevap4 == "5")
                                {
                                    Console.WriteLine("Oluşturulan hesap sayısı: {0}.", nesne.Size());
                                }
                            }
                            catch
                            {
                                Console.Write("Yanlış bir değer girdiniz.Lütfen tekrardan giriniz:");
                                cevap4 = Console.ReadLine();
                                if (cevap4 == "1")
                                {
                                    Console.Write("Lütfen çekilecek para miktarını giriniz:");
                                    double cekilecekmiktar = Convert.ToDouble(Console.ReadLine());
                                    if (Convert.ToDouble(m.Paramiktari) >= cekilecekmiktar)
                                    {
                                        Console.WriteLine("Hesabınızda kalan para miktarı {0} TL'dir.", Convert.ToDouble(m.Paramiktari) - cekilecekmiktar);
                                    }
                                    else
                                    {
                                        Console.Write("! Çekmek istediğiniz para miktarı hesabınızdaki para miktarından fazladır.Lütfen tekrar giriniz:");
                                        double cekilecekmiktar2 = Convert.ToDouble(Console.ReadLine());
                                        if (Convert.ToDouble(m.Paramiktari) >= cekilecekmiktar)
                                        {
                                            Console.WriteLine("Hesabınızda kalan para miktarı {0} TL'dir.", Convert.ToDouble(m.Paramiktari) - cekilecekmiktar2);
                                        }
                                        else
                                        {
                                            Console.WriteLine("! Çok sayıda hatalı giriş yaptınız.İyi günlerde tekrardan bankamıza bekleriz ...");
                                            break;
                                        }
                                    }
                                }
                                else if (cevap4 == "2")
                                {

                                    Console.Write("Lütfen yatırılacak para miktarını giriniz:");
                                    double yatırılacakmiktar = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("Hesabınızdaki güncel para miktarınız {0} TL'dir.", Convert.ToDouble(m.Paramiktari) + yatırılacakmiktar);
                                }
                                else if (cevap4 == "3")
                                {
                                    Console.WriteLine("Eklenen ilk hesabın T.C. kimlik numarası {0}, para miktarı {1}.", Convert.ToString(nesne.Peek().Tckimlikno), Convert.ToString(nesne.Peek().Paramiktari));
                                }
                                else if (cevap4 == "4")
                                {
                                    nesne.Clear();
                                }
                                else if (cevap4 == "5")
                                {
                                    Console.WriteLine("Oluşturulan hesap sayısı: {0}.", nesne.Size());
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("İyi günlerde tekrardan bankamıza bekleriz ...");
                            break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Kayıt silmek ister misiniz ?");
                        if (cevap == "Evet")
                        {
                            Console.WriteLine("Silinen kayıttaki bilgiler :{0} {1}", nesne.Dequeue().Tckimlikno, nesne.Dequeue().Paramiktari);

                        }
                        else
                        {
                            Console.WriteLine("İyi günlerde tekrardan bankamıza bekleriz ...");
                            break;
                        }
                    }
                }
                }
        }
    }
}

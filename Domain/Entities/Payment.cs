using Domain.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }

        public ICollection<Purchase> Purchases { get; set; }


        //Non-Generic
        //static int Topla(int sayi1, int sayi2)
        //{
        //    return sayi1 + sayi2;
        //}

        ////Non-Generic
        //static double Topla(double sayi1, double sayi2)
        //{
        //    return sayi1 + sayi2;
        //}

        //Type : T
        //static T Topla<T>(T sayi1, T sayi2) where T : INumber<T>
        //{
        //    return sayi1 + sayi2;
        //}

        //static void main()
        //{
        //    //Non-Generic
        //    int toplam = Topla<int>(5, 8);


        //    //Generic
        //    double sonuc = Topla<double>(14.8, 3.4);

        //    float sonuc2 = Topla<float>(1.5f, 6.2f);

        //    List<string> sehirler = new List<string>();


        //    sehirler.Where( (x) => x.StartsWith("A") );




        //}
    }
}

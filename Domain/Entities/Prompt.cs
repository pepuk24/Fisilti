using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Prompt : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } //KategoriId

        //Navigation Property
        public ICollection<Favourite> Favourites { get; set; }

        //[ForeignKey("KategoriId")] : Kolon Eğer TabloAdıId şeklinde isimlendirilmemişse bu Attribute(Özellik) eklenir içerisinde ise kolon ismi tanımlanır.
        public Category Category { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSignUp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Published_On { get; set; }
        public int Author_Id { get; set; }
    }
}

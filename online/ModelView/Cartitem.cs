using online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online.ModelView
{
    public class Cartitem
    {
        public Product sanpham { get; set; }

        public int amount { get; set; }

        public double Totalmoney => amount * sanpham.Price.Value;
    }
}

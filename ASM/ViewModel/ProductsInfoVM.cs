using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModel
{
    class ProductsInfoVM
    {
        public int Pro_id { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public string Weight { get; set; }
        public string pure_weight { get; set; }
        public string Karat { get; set; }
        public string Serial_number { get; set; }
        public string Classification { get; set; }
        public string Cateqorize { get; set; }
        public int Cateqorize_id { get; set; }
        public int Classification_id { get; set; }
        public string made_in { get; set; }
        public int made_in_id { get; set; }
        public string Imported_Quntity { get; set; }
        public string ImageUrl { get; set; }
    }
}

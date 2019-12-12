using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppBootcamp32.Models;

namespace WebAppBootcamp32.ViewModel
{
    public class AddSupplierVM
    {
        public IEnumerable<TB_M_Supplier> SuppliersList { get; set; }
        public TB_M_Supplier Suppliers { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
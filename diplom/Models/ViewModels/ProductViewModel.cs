using System.Collections.Generic;

namespace diplom.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set;}
        public int ProductCount { get; set; }
        public bool HasResults { get; set; }
        public IEnumerable<_User> Users { get; set; }
        //public string SelectedProductName { get; set; }
        public string PackageNames { get; set; }
        public int SelectedProductId { get; set; }
        public string SelectedPackage { get; set; }
        public string ProviderNames { get; set; }

    }
}

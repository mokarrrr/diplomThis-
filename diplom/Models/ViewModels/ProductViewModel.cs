using System.Collections.Generic;

namespace diplom.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set;}
        public int ProductCount { get; set; }
        public bool HasResults { get; set; }
        public IEnumerable<User> Users { get; set; }

        public string PackageNames { get; set; }
        public int SelectedProductId { get; set; }
        //public string SelectedPackage { get; set; }
        public string ProviderNames { get; set; }

        public Dictionary<int, double> ProductAverageRates { get; set; }

        public IEnumerable<Rate> Rates { get; set; }

        public List<_order> _Orders { get; set; }

        public List<UserBasket> Users_Baskets { get; set; }

        public List<_Provider> providers { get; set; }

        public List<Package> packages { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SearchCriterias
{
    public abstract class SearchCriteria
    {

    }

    public class ProductSearchCriteria : SearchCriteria
    {
        public decimal From { get; set; }
        public decimal To { get; set; }
        public string Color { get; set; }

    }
}

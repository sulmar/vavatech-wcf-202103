using System;
using System.Collections.Generic;
using System.Text;

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

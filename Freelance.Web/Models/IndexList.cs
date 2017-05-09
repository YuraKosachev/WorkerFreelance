using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Web.Models
{
    public class IndexList
    {
        public string Error { get; set; }
        public List<CategoryViewModel> List { get; set; }
        public static IndexList Empty(string error)
        {
            return new IndexList {
                List = new List<CategoryViewModel>(),
                Error = error
            };
        }
    }
}
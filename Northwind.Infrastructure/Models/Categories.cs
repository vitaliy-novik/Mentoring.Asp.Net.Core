﻿using System.Collections.Generic;

namespace Northwind.Infrastructure.Models
{
	public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public virtual byte[] Picture { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}

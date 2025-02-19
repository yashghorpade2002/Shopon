using System;
using System.Collections.Generic;

namespace Shopon.EF.Models;

internal class Company
{
    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

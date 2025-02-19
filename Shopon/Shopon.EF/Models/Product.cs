using Shopon.Common.Models;
using System;
using System.Collections.Generic;

namespace Shopon.EF.Models;

internal class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public int? CompanyId { get; set; }

    public int? CategoryId { get; set; }

    public string? Availablestatus { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsDeleted { get; set; }

    internal virtual Category? Category { get; set; }

    internal virtual Company? Company { get; set; }

    internal virtual ProductRatings? Ratings { get; set; }
}

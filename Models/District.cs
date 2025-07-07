using System;
using System.Collections.Generic;
namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;
    //foreign key
    public int? StateId { get; set; }
    //navigation property
    //one state can have multiple district
    public virtual State? State { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

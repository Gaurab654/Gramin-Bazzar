using System;
using System.Collections.Generic;
namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;

public partial class State
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;
    //Navigation property 
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}

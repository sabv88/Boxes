using System.Collections.Generic;

public class BoxJson : Box
{
    public List<Bottle> Bottles { get; set; }
    public BoxJson(string Code, int Box_id, List<Bottle> Bottles) : base(Code, Box_id)
    {
        this.Bottles = Bottles;
    }
}


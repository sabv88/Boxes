using System.Collections.Generic;

public class PalletJson : Pallet
{
    public List<BoxJson> Boxes { get; set; }

    public PalletJson(string Code, List<BoxJson> Boxes) : base(Code)
    {
        this.Boxes = Boxes;
    }

}


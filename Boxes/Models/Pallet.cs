using System.Collections.Generic;

public class Pallet
{
    public int Id { get; set; }
    public string Code { get; set; }
    public Pallet(string Code)
    {
        this.Code = Code;
    }
}


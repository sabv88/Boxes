
using System.Collections.Generic;

public class Box
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int Box_id { get; set; }

    public Box(string Code, int Box_id) 
    {
        this.Code = Code;
        this.Box_id = Box_id;
    }
}


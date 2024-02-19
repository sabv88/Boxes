
public class Bottle
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int Box_id { get; set; }
    public Bottle(string Code, int Box_id)
    {
        this.Code = Code;
        this.Box_id = Box_id;
    }
}


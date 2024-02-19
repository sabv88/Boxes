public class Mission
{
    public string Name { get; set; }
    public string GTIN { get; set; }
    public string Volume { get; set; }
    public int BoxFormat { get; set; }
    public int PalletFormat { get; set; }

   public Mission(string name, string gtin, string volume, int boxFormat, int palletFormat) 
    {
        Name = name;
        GTIN = gtin;
        Volume = volume;
        BoxFormat = boxFormat;
        PalletFormat = palletFormat;
    }
}

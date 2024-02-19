
using System.Collections.Generic;

public class Card
{
    public string ProductName { get; set; }

    public string Gtin { get; set; }

    public int BoxFormat { get; set; }

    public int PalletFormat { get; set; }

    public List<PalletJson> Pallets { get; set; }

    public Card(string productName, string gtin, int boxFormat, int palletFormat, List<PalletJson> pallets)
    {
        ProductName = productName;
        Gtin = gtin;
        BoxFormat = boxFormat;
        PalletFormat = palletFormat;
        Pallets = pallets;
    }

    public Card() { }
}

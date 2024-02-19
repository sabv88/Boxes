
using System.Collections.Generic;
using System.Threading.Tasks;

interface ICardService
{
    public Task<Mission> GetMission();
    public Task<List<string>> GetItemCodes(string code);
    public void InsertItemCodes(List<string> codes, int boxeSize, int palletSize);

    public void MakeCard(List<Bottle> bottles, List<Box> boxes, List<Pallet> pallets, string ProductName, string Gtin, int BoxFormat, int PalletFormat);
}


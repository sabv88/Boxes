using Boxes.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Boxes.logic
{
    internal class CardService : ICardService
    {
        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://promark94.marking.by"),
        };

        ApplicationDbContext db = new ApplicationDbContext();

        public async Task<Mission> GetMission()
        {
            var response = await httpClient.GetAsync("client/api/get/task/");
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject data = JObject.Parse(responseBody);
            var a = new Mission(data["mission"]["lot"]["product"]["name"].ToString(), data["mission"]["lot"]["product"]["gtin"].ToString(), data["mission"]["lot"]["package"]["volume"].ToString(), (int)data["mission"]["lot"]["package"]["boxFormat"], (int)data["mission"]["lot"]["package"]["palletFormat"]);
            return a;
        }
        public async Task<List<string>> GetItemCodes(string code)
        {
            List<string> codes = new List<string>();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                using (StreamReader reader = new StreamReader(dlg.FileName))
                {
                    string? line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        codes.Add(line.Trim());
                    }
                }
            }

            List<string> resultList = new List<string>();

            foreach (string tempCode in codes)
            {
                if (tempCode[2..16] == code)
                {
                    resultList.Add(tempCode);
                }
            }

            return resultList;
        }
        public async void InsertItemCodes(List<string> codes, int boxeSize, int palletSize)
        {
            int bottleCount = 0;
            int boxId = 1;
            int boxCount = 0;
            int palletId = 1;

            db.Database.EnsureCreated();
            db.Bottles.Load();
            db.Boxes.Load();
            db.Pallets.Load();

            foreach (string tempCode in codes)
            {
                if (bottleCount < boxeSize)
                {
                    bottleCount++;
                }
                else
                {
                    db.Boxes.Add(new Box(tempCode[..16] + "37" + bottleCount + "21" + boxId, palletId));
                    bottleCount = 1;
                    boxId++;
                    boxCount++;
                }

                if (boxCount >= palletSize)
                {
                    db.Pallets.Add(new Pallet(tempCode[..16] + "37" + boxCount + "21" + palletId));
                    boxCount = 0;
                    palletId++;
                }

                db.Bottles.Add(new Bottle(tempCode, boxId));
            }

            db.SaveChanges();
        }

        public async void MakeCard(List<Bottle> bottles, List<Box> boxes, List<Pallet> pallets, string ProductName, string Gtin, int BoxFormat, int PalletFormat)
        {
            List<BoxJson> boxesJson = new();
            List<PalletJson> palletJson = new();

            foreach (var box in boxes)
            {
                boxesJson.Add(new BoxJson(box.Code, box.Box_id, (from b in bottles
                                                                 where b.Box_id == box.Id
                                                                 select b).ToList()));
            }

            foreach (var pallet in pallets)
            {
                palletJson.Add(new PalletJson(pallet.Code, (from b in boxesJson
                                                            where b.Box_id == pallet.Id
                                                            select b).ToList()));
            }

            Card card = new(ProductName, Gtin, BoxFormat, PalletFormat, palletJson);

            using (FileStream fs = new FileStream(Gtin + "_result_file_" + DateTime.Now.ToString("ddMMyy") + "_" + DateTime.Now.ToString("HHmm")+".json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, card);
            }
        }

    }
}

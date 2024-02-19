using Boxes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Boxes.ViewModels
{
    internal class MissionViewModel : INotifyPropertyChanged, IMissionViewModel
    {
        private string name = string.Empty;
        private string gtin = string.Empty;
        private string volume = string.Empty;
        private int boxFormat = 0;
        private int palletFormat = 0;
        private Command importCodesCommand;
        private Command createCardCommand;
        ICardService cardService;
        public Card card { get; set; }


        ApplicationDbContext db = new ApplicationDbContext();

        public List<Bottle> bottles { get; set; }
        public List<Box> boxes { get; set; }
        public List<Pallet> pallets { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Bottle> Bottles
        {
            get { return bottles; }
            set
            {
                bottles = value;
                OnPropertyChanged(nameof(Bottles));
            }
        }
        public List<Box> Boxes
        {
            get { return boxes; }
            set
            {
                boxes = value;
                OnPropertyChanged(nameof(Boxes));
            }
        }
        public List<Pallet> Pallets
        {
            get { return pallets; }
            set
            {
                pallets = value;
                OnPropertyChanged(nameof(Pallets));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string GTIN
        {
            get { return gtin; }
            set
            {
                gtin = value;
                OnPropertyChanged(nameof(GTIN));
            }
        }

        public string Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }
        public int BoxFormat
        {
            get { return boxFormat; }
            set
            {
                boxFormat = value;
                OnPropertyChanged(nameof(BoxFormat));
            }
        }
        public int PalletFormat
        {
            get { return palletFormat; }
            set
            {
                palletFormat = value;
                OnPropertyChanged(nameof(PalletFormat));
            }
        }
        public Command ImportCodesCommand
        {
            get
            {
                return importCodesCommand ??
                  (importCodesCommand = new Command(obj =>
                  {
                      ImportCodes();
                  }));
            }
        }
        public Command CreateCardCommand
        {
            get
            {
                return createCardCommand ??
                  (createCardCommand = new Command(obj =>
                  {
                      cardService.MakeCard(Bottles, Boxes, Pallets, Name, GTIN, BoxFormat, PalletFormat);
                  }));
            }
        }

        public MissionViewModel(ICardService cardService)
        {
            this.cardService = cardService;
            SetMission();
        }

        async void SetMission()
        {
            var mission = await cardService.GetMission();
            Name = mission.Name;
            GTIN = mission.GTIN;
            Volume = mission.Volume;
            BoxFormat = mission.BoxFormat;
            PalletFormat = mission.PalletFormat;

        }
        private async void ImportCodes()
        {
           var a = await cardService.GetItemCodes(GTIN);
           cardService.InsertItemCodes(a, BoxFormat, PalletFormat);
           Bottles = await db.Bottles.ToListAsync();
           Pallets = await db.Pallets.ToListAsync();
           Boxes = await db.Boxes.ToListAsync();
        }

        public MissionViewModel(string name, string gtin, string volume, int boxFormat, int palletFormat)
        {
            Name = name;
            GTIN = gtin;
            Volume = volume;
            BoxFormat = boxFormat;
            PalletFormat = palletFormat;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

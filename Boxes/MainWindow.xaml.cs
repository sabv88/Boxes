using Boxes.ViewModels;
using System.Windows;


namespace Boxes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMissionViewModel missionViewModel)
        {
            InitializeComponent();
            MissionGrid.DataContext = missionViewModel;
            BottleGrid.DataContext = missionViewModel;
            BoxGrid.DataContext = missionViewModel;
            PalletGrid.DataContext = missionViewModel;

        }
    }
}

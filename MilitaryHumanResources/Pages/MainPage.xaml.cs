using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MilitaryHumanResources.Pages.ManageSoldiers;

namespace MilitaryHumanResources.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        // Manage Soldiers
        private ManageSoldiersPage _manageSoldiersPage;

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainContainerF.NavigationService.Navigate(SplitPageContainer.Instance as SplitPageContainer);
        }

        private void MenuButtonClicked_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            Page page = null;
            switch (button.Name)
            {
                case "CreateNewSoldierB":
                    if (_manageSoldiersPage == null)
                        _manageSoldiersPage = new ManageSoldiersPage();
                    page = _manageSoldiersPage;
                    break;
            }
            SplitPageContainer.Instance.LoadMainPage(page);
        }


    }
}

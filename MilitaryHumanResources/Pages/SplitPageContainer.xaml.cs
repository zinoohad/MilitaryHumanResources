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
using MilitaryHumanResources.Common;
using MilitaryHumanResources.Interface;

namespace MilitaryHumanResources.Pages
{
    /// <summary>
    /// Interaction logic for SplitPageContainer.xaml
    /// </summary>
    public partial class SplitPageContainer : Page, ISplitPageController
    {
        private static ISplitPageController _instance;

        public static ISplitPageController Instance => _instance ?? (_instance = new SplitPageContainer());

        private object _currentDisplayPage;

        private SubMenuState _subMenuState = SubMenuState.CLOSE;

        private SplitPageContainer()
        {
            InitializeComponent();
        }

        #region ISplitPageController

        public void LoadMainPage(object page)
        {
            _currentDisplayPage = page;
            MainPageF.NavigationService.Navigate((page as IPageBind)?.GetPage());
        }

        public void OpenSubMenu<T>(ISubPageBind<T> bindPage, T data)
        {
            if (!(((Page) SecondaryPageF.Content) is ISubPageBind<T>) ||
                (SecondaryPageF.Content.GetType() != bindPage.GetType())
            ) // Check if the frame is already contains the loaded page
            {
                bindPage.BindPage(_currentDisplayPage as IPageBind);
                SecondaryPageF.NavigationService.Navigate(bindPage.LoadPage(data));
            }

            SubFrameWidth.Width = new GridLength(250, GridUnitType.Pixel);
            _subMenuState = SubMenuState.OPEN;
        }

        public void CloseSubMenu()
        {
            SubFrameWidth.Width = new GridLength(0);
            _subMenuState = SubMenuState.CLOSE;
        }

        #endregion

        #region Events

        private void OpenCloseSecondaryPageB_OnClick(object sender, RoutedEventArgs e)
        {
            if (_subMenuState == SubMenuState.OPEN)
            {
                // Close the sub menu
                CloseSubMenu();
                var imageBrush = new ImageBrush(
                    new BitmapImage(
                        new Uri("pack://siteoforigin:,,,/Resources/left_arrow_button_white.png")
                    )
                ) {Stretch = Stretch.Uniform};
                OpenCloseSecondaryPageB.Background = imageBrush;
            }
            else
            {
                // Open the sub menu
                SubFrameWidth.Width = new GridLength(250, GridUnitType.Pixel);
                _subMenuState = SubMenuState.OPEN;
                var imageBrush = new ImageBrush(
                    new BitmapImage(
                        new Uri("pack://siteoforigin:,,,/Resources/right_arrow_button_white.png")
                    )
                ) {Stretch = Stretch.Uniform};
                OpenCloseSecondaryPageB.Background = imageBrush;
            }
        }

        #endregion
    }
}
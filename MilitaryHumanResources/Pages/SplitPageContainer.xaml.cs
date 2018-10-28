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

        private IPageBind<dynamic> _currentDisplayPage;

        private SplitPageContainer()
        {
            InitializeComponent();
        }

        #region ISplitPageController

        public void LoadMainPage(Page page)
        {
            _currentDisplayPage = page as IPageBind<dynamic>;
            MainPageF.NavigationService.Navigate(page);
        }

        public void OpenSubMenu<T>(ISubPageBind<T> bindPage, T data)
        {
            if (!(((Page)SecondaryPageF.Content) is ISubPageBind<T>))   // Check if the frame is already contains the loaded page
            {
                bindPage.BindPage(_currentDisplayPage as IPageBind<T>);
                SecondaryPageF.NavigationService.Navigate(bindPage.LoadPage(data));
            }
            SubFrameWidth.Width = new GridLength(1, GridUnitType.Auto);
        }

        public void CloseSubMenu()
        {
            SubFrameWidth.Width = new GridLength(0);
        }

        #endregion

    }
}

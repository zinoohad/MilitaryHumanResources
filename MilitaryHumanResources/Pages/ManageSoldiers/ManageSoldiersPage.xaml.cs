using MilitaryHumanResources.Interface;
using MilitaryHumanResources.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MilitaryHumanResources.Database.SQLite;

namespace MilitaryHumanResources.Pages.ManageSoldiers
{
    /// <summary>
    /// Interaction logic for ManageSoldiersPage.xaml
    /// </summary>
    public partial class ManageSoldiersPage : Page, IPageBind
    {

        private SQLiteHelper _sql = new SQLiteHelper();

        private Lazy<CreateNewSoldier> _createNewSoldirPage = new Lazy<CreateNewSoldier>();

        private List<Soldier> _soldiers;

        public ManageSoldiersPage()
        {
            InitializeComponent();
        }

        #region IPageBind

        public void Close()
        {
            
        }

        public void SubmitPageResult(object data)
        {
            var soldier = data as Soldier;
        }

        public Page GetPage() => this;

        #endregion

        #region Events

        private void CreateNewSoldir_Click(object sender, RoutedEventArgs e)
        {
            SplitPageContainer.Instance.OpenSubMenu<Soldier>(_createNewSoldirPage.Value as ISubPageBind<Soldier>, null);
        }

        private void ManageSoldiersPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            _soldiers = _sql.GetListOfSoldiers();
            UsersLV.DataContext = _soldiers;
        }

        #endregion
    }
}

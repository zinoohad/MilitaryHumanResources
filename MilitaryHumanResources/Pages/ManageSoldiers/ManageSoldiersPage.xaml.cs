using MilitaryHumanResources.Interface;
using MilitaryHumanResources.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MilitaryHumanResources.Pages.ManageSoldiers
{
    /// <summary>
    /// Interaction logic for ManageSoldiersPage.xaml
    /// </summary>
    public partial class ManageSoldiersPage : Page, IPageBind<Soldier>
    {

        private Lazy<CreateNewSoldier> _createNewSoldirPage = new Lazy<CreateNewSoldier>();

        public ManageSoldiersPage()
        {
            InitializeComponent();
        }

        #region IPageBind

        public void Close()
        {
            
        }

        public void SubmitPageResult(Soldier data)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void CreateNewSoldir_Click(object sender, RoutedEventArgs e)
        {
            SplitPageContainer.Instance.OpenSubMenu<Soldier>(_createNewSoldirPage.Value as ISubPageBind<Soldier>, null);
        }
    }
}

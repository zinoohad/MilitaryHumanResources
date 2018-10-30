using Microsoft.Win32;
using MilitaryHumanResources.Interface;
using MilitaryHumanResources.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MilitaryHumanResources.Database.SQLite;

namespace MilitaryHumanResources.Pages.ManageSoldiers
{
    /// <summary>
    /// Interaction logic for CreateNewSoldier.xaml
    /// </summary>
    public partial class CreateNewSoldier : Page, ISubPageBind<Soldier>
    {

        private SQLiteHelper _sql = new SQLiteHelper();

        public Soldier Solder;

        private IPageBind _mainPage;
         
        public List<Profession> Professions;
        public List<Role> Roles;
        public List<Rank> Ranks;
        public List<ArmoredVessels> ArmoredVesselses;
        public List<Subunit> Subunits;

        public CreateNewSoldier()
        {
            InitializeComponent();
        }

        #region ISubPageBind

        public void BindPage(IPageBind page)
        {
            _mainPage = page;
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public Page LoadPage(Soldier data)
        {
            LoadUiCombos();
            Solder = data ?? new Soldier();
            FormG.DataContext = Solder;
            return this;
        }

        #endregion

        #region Events

        private void SubmitNewSoldir_Click(object sender, RoutedEventArgs e)
        {
            _mainPage?.SubmitPageResult(Solder);
        }

        private void SoldirAvatarI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.png;*.jpeg;*.jpg;*.bmp";
            dialog.Title = "Select Avatar";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dialog.ShowDialog() == false)
                return;

            SoldirAvatarI.Source = new BitmapImage(new Uri(dialog.FileName));
            //TODO: Copy this image to application local folder
        }

        #endregion

        #region Function

        private void LoadUiCombos()
        {
            // Set professions
            Professions = _sql.GetProfessions();
            MainProfessionCB.ItemsSource = Professions;
            SecondaryProfessionCB.ItemsSource = Professions;

            // Set Roles
            Roles = _sql.GetRoles();
            RoleCB.ItemsSource = Roles;

            // Set ranks
            Ranks = _sql.GetRanks();
            RankTB.ItemsSource = Ranks;

            // Set Combat InlayCB
            ArmoredVesselses = _sql.GetArmoredVessels();
            CombatInlayCB.ItemsSource = ArmoredVesselses;

            // Set Subunits
            Subunits = _sql.GetSubunits();
            SubunitCB.ItemsSource = Subunits;
        }

        #endregion
    }
}

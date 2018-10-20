using Microsoft.Win32;
using MilitaryHumanResources.Interface;
using MilitaryHumanResources.Model;
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

namespace MilitaryHumanResources.Pages.ManageSoldiers
{
    /// <summary>
    /// Interaction logic for CreateNewSoldier.xaml
    /// </summary>
    public partial class CreateNewSoldier : Page, ISubPageBind<Soldier>
    {

        private Soldier _solder;

        private IPageBind<Soldier> _mainPage;

        public CreateNewSoldier()
        {
            InitializeComponent();
        }

        #region ISubPageBind

        public void BindPage(IPageBind<Soldier> page)
        {
            _mainPage = page;
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public Page LoadPage(Soldier data)
        {
            _solder = data ?? new Soldier();
            FormG.DataContext = _solder;
            return this;
        }


        #endregion

        private void SubmitNewSoldir_Click(object sender, RoutedEventArgs e)
        {
            _mainPage?.SubmitPageResult(_solder);
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
    }
}

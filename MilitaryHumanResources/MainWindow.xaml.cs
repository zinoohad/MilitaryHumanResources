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

namespace MilitaryHumanResources
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void ExitB_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeWindowB_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindowB_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    this.Height = WindowHeight;
                    this.Width = WindowWidth;
                    b.Content = "⬜";
                    break;
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    b.Content = "❐";

                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowHeight = this.Height;
            WindowWidth = this.Width;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowHeight = this.Height;
            WindowWidth = this.Width;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}

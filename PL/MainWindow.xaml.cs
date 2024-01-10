using DalApi;
using PL.Engineer;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {/// <summary>
     /// Initialize main window
     /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        ///open a window to display a list of engineers
        /// </summary>
        /// <param name="sender">The control for which the action is intended</param>
        /// <param name="e">Event handlers at the source of the event</param>
        private void ShowEngineerList(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        /// <summary>
        /// Engineer data initialization in DB
        /// </summary>
        /// <param name="sender">The control for which the action is intended</param>
        /// <param name="e">Event handlers at the source of the event</param>
        private void initEngineerDB(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to initialize data?",
                     "initialize DB ",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do();
            }
        }

    }
}

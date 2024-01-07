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
using System.Windows.Shapes;

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.Engineer? CurrentEngineer { get; set; } =new BO.Engineer();
    public string EngineerName { get; set; } = "";
    int CurrentEngineerId = 0;
    public EngineerWindow()
    {
        InitializeComponent();
        try {
         CurrentEngineer = CurrentEngineerId == 0 ? new() : s_bl.Engineer.Read(CurrentEngineerId);
       }catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {

    }

    private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
    {

    }
}

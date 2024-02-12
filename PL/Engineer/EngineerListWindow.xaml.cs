using DalApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{
    /// <summary>
    /// object access to BL
    /// </summary>
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// Initialize the search condition variable
    /// </summary>
    public BO.EngineerExperience? EngineerExperience { get; set; } = BO.EngineerExperience.None;
    /// <summary>
    /// Initialize a window displaying a list of engineers
    /// </summary>
    public EngineerListWindow()
    {
        InitializeComponent();
        var temp = (from BO.Engineer boEngineer in s_bl.Engineer.ReadAll()
                    select new BO.EngineerInList()
                    {
                        Id = boEngineer.Id,
                        Name = boEngineer.Name,
                        Level = boEngineer.Level
                    });
        EngineerList = temp == null ? new() : new(temp!);
    }
    /// <summary>
    /// Initialize and set a dependency ObservableCollection EngineerList
    /// </summary>
    public ObservableCollection<BO.EngineerInList> EngineerList
    {
        get { return (ObservableCollection<BO.EngineerInList>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }
    /// <summary>
    ///  dependency object of the ObservableCollection Engineers List
    /// </summary>
    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));

    /// <summary>
    /// Filter engineers by levels
    /// </summary>
    /// <param name="sender">The control for which the action is intended</param>
    /// <param name="e">Event handlers at the source of the event</param>
    private void FilteringEngineers(object sender, SelectionChangedEventArgs e)
    {
        var temp = EngineerExperience == BO.EngineerExperience.None ?
                (from BO.Engineer boEngineer in s_bl.Engineer.ReadAll()
                 select new BO.EngineerInList()
                 {
                     Id = boEngineer.Id,
                     Name = boEngineer.Name,
                     Level = boEngineer.Level
                 }) :
                 (from BO.Engineer boEngineer in s_bl.Engineer.ReadAll(item => (int?)item!.Level == (int?)EngineerExperience)
                  select new BO.EngineerInList()
                  {
                      Id = boEngineer.Id,
                      Name = boEngineer.Name,
                      Level = boEngineer.Level
                  });
        EngineerList = temp == null ? new() : new(temp!);

    }
    /// <summary>
    /// Opening a window to add an engineer
    /// </summary>
    /// <param name="sender">The control for which the action is intended</param>
    /// <param name="e">Event handlers at the source of the event</param>
    private void AddEngineer(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().ShowDialog();
        
        var temp = (from BO.Engineer boEngineer in s_bl.Engineer.ReadAll()
                    select new BO.EngineerInList()
                    {
                        Id = boEngineer.Id,
                        Name = boEngineer.Name,
                        Level = boEngineer.Level
                    });
        EngineerList = temp == null ? new() : new(temp!);
    }
    /// <summary>
    /// Opening a window to  Update an engineer
    /// </summary>
    /// <param name="sender">The control for which the action is intended</param>
    /// <param name="e">Event handlers at the source of the event.</param>
    private void UpdateEngineer(object sender, RoutedEventArgs e)
    {
        BO.EngineerInList? updateEngineer=(sender as ListView)?.SelectedItem as BO.EngineerInList;

        new EngineerWindow(updateEngineer!.Id).ShowDialog();
      
        var temp = (from BO.Engineer boEngineer in s_bl.Engineer.ReadAll()
                    select new BO.EngineerInList()
                    {
                        Id = boEngineer.Id,
                        Name = boEngineer.Name,
                        Level = boEngineer.Level
                    });
        EngineerList = temp == null ? new() : new(temp!);

    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}

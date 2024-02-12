
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{/// <summary>
/// object access to BL
/// </summary>
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// Initialize and set a dependency object Engineer
    /// </summary>
    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer) GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }
    /// <summary>
    /// A dependency object of the current engineer
    /// </summary>
    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer),
            typeof(EngineerWindow), new PropertyMetadata(null));
    /// <summary>
    /// Initialize a single engineer display window
    /// </summary>
    /// <param name="CurrentEngineerId">ID of the required engineer</param>
    public EngineerWindow(int CurrentEngineerId = 0)
    {
        InitializeComponent();
        try {

            CurrentEngineer = CurrentEngineerId == 0 ? new BO.Engineer
            {
                Id = 0,
                Name = "",
                Email = "",
                Level = 0,
                Cost = 0,
               Task = new BO.TaskInEngineer { Id = 0, Alias = "" }
            }
           : s_bl.Engineer.Read(CurrentEngineerId)!;
            if (CurrentEngineer.Task == null)
            {
                CurrentEngineer.Task = new BO.TaskInEngineer { Id = 0, Alias = "" };
            }
        }

        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    /// <summary>
    ///Sending the engineer to update or add in the layer below
    /// </summary>
    /// <param name="sender">The control for which the action is intended</param>
    /// <param name="e">Event handlers at the source of the event.</param>
    private void AddOrUpdateEngineer(object sender, RoutedEventArgs e)
    {
        try
        {
            if ((sender as Button).Content.ToString()=="Add" ) { 
                s_bl.Engineer.Create(CurrentEngineer);
                MessageBox.Show($"The engineer with id={CurrentEngineer.Id} was successfully added");
                
            }
            else
            {

                s_bl.Engineer.Update(CurrentEngineer);
                MessageBox.Show($"The engineer with id={CurrentEngineer.Id} was successfully updated");

            }
            this.Close();
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.BlInvalidValuesException ex) { MessageBox.Show(ex.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
}

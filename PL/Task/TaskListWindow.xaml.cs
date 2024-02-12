using BO;
using PL.Engineer;
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
using System.Windows.Shapes;

namespace PL.Task;
/// <summary>
/// Interaction logic for TaskListWindow.xaml
/// </summary>
public partial class TaskListWindow : Window
{
    /// <summary>
    /// object access to BL
    /// </summary>
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// Initialize the search condition variable
    /// </summary>
    public BO.EngineerExperience? taskCopmlexity { get; set; } = BO.EngineerExperience.None;
    public TaskListWindow()
    {
        InitializeComponent();
        /* var temp = (from BO.Task boTask in s_bl.Task.ReadAll()
                     select new BO.TaskInList()
                     {
                         Id = boTask.Id,
                         Description = boTask.Description,
                         Alias = boTask.Alias,
                        Status = boTask.Status,
                     });
         TaskList = temp == null ? new() : new(temp!);*/
        var temp = (from BO.Task boTask in s_bl.Task.ReadAll()
                    select new BO.TaskInList()
                    {
                        Id = boTask.Id,
                        Description = boTask.Description,
                        Alias = boTask.Alias,
                        Status = boTask.Status
                    });
        TaskList = temp == null ? new() : new(temp!);
    }
    public ObservableCollection<BO.TaskInList> TaskList
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }
    /// <summary>
    ///  dependency object of the ObservableCollection Task List
    /// </summary>
    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    private void FilteringTask(object sender, SelectionChangedEventArgs e)
    {
        var temp = taskCopmlexity == BO.EngineerExperience.None ?
               (from BO.Task boTask in s_bl.Task.ReadAll()
                select new BO.TaskInList()
                {
                    Id = boTask.Id,
                    Description = boTask.Description,
                    Alias = boTask.Alias,
                    Status = boTask.Status
                }) :
                (from BO.Task boTask in s_bl.Task.ReadAll(item => (int?)item!.ComplexityLevel == (int?)taskCopmlexity)
                 select new BO.TaskInList()
                 {
                     Id = boTask.Id,
                     Description = boTask.Description,
                     Alias = boTask.Alias,
                     Status = boTask.Status
                 });
        TaskList = temp == null ? new() : new(temp!);
    }
    /// <summary>
    /// Opening a window to add an task
    /// </summary>
    /// <param name="sender">The control for which the action is intended</param>
    /// <param name="e">Event handlers at the source of the event</param>
    private void AddTask(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();

        var temp = (from BO.Task boTask in s_bl.Task.ReadAll()
                    select new BO.TaskInList()
                    {
                        Id = boTask.Id,
                        Description = boTask.Description,
                        Alias = boTask.Alias,
                        Status = boTask.Status
                    });
        TaskList = temp == null ? new() : new(temp!);
    }
    /*private void UpdateTask(object sender, RoutedEventArgs e)
    {
        BO.TaskInList? updateTask = (sender as ListView)?.SelectedItem as BO.TaskInList;

        new TaskWindow(updateTask!.Id).ShowDialog();

        var temp = (from BO.Task boTask in s_bl.Task.ReadAll()
                    select new BO.TaskInList()
                    {
                        Id = boTask.Id,
                        Description = boTask.Description,
                        Alias = boTask.Alias,
                        Status = boTask.Status
                    });
        TaskList = temp == null ? new() : new(temp!);

    }*/
}

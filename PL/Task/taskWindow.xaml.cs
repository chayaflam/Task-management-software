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
using System.Windows.Shapes;

namespace PL.Task;

/// <summary>
/// Interaction logic for taskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    /// <summary>
    /// object access to BL
    /// </summary>
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// Initialize and set a dependency object Task
    /// </summary>
    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }
    /// <summary>
    /// A dependency object of the current Task
    /// </summary>
    public static readonly DependencyProperty CurrentTaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task),
            typeof(TaskWindow), new PropertyMetadata(null));
    public TaskWindow(int CurrentTaskId = 0)
    {
        InitializeComponent();
        try
        {
            CurrentTask = CurrentTaskId == 0 ? new BO.Task
            {
                Id = 0,
                Description = "",
                Alias = "",
                CreatedAtDate = new(),
                Status = 0,
                Dependencies = null,
                RequiredEffortTime = new(),
                StartDate = new(),
                ScheduledDate = new(),
                ForecastDate = new(),
                DeadlineDate = new(),
                CompleteDate = new(),
                Deliverables = "",
                Remarks = "",
                Engineer = new BO.EngineerInTask { Id = 0, Name = "" },
                Copmlexity = (BO.EngineerExperience)6,//None
            }
           : s_bl.Task.Read(CurrentTaskId)!;
            if (CurrentTask.Engineer == null)
            {
                CurrentTask.Engineer = new BO.EngineerInTask { Id = 0, Name = "" };
            }
        }
       
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

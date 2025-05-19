using Syncfusion.UI.Xaml.Scheduler;

namespace utad.ToDo.WpfSln
{
    public class Appointment: ScheduleAppointment
    {
        System.Windows.Media.Brush color;

        /// <summary>
        /// Gets or sets the value indicating whether the reminder is dismissed or not. 
        /// </summary>
        public bool Dismissed { get; set; }

        /// <summary>
        /// Gets or sets the value to display reminder alert before appointment start time.
        /// </summary>
        public TimeSpan TimeInterval { get; set; }

        public System.Windows.Media.Brush Color
        {
            get { return color; }
            set{
                color = value;
                RaisePropertyChanged("Color");
            }
        }
    }
}

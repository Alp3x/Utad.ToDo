using Syncfusion.PMML;
using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using utad.ToDo.Wpf;
using utad.ToDo.Wpf.Views;

namespace utad.ToDo.WpfSln
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        
        private App _app;
        public List<ScheduleAppointment> meetingsOrdered;
        public MainWindow()
        {
            InitializeComponent();

            _app = (App)App.Current as App;
            _app.ViewModel.PerfilFotoGuardada += ViewModel_PerfilFotoGuardada;
            _app.ViewModel.PerfilFotoCarregada += ViewModel_PerfilFotoCarregada;
            LoadSideBar();
        }

        private void LoadSideBar()
        {
            if (DataContext is ScheduleViewModel viewModel)
            {
                ListViewApp.Items.Clear();
                meetingsOrdered = viewModel.Events.OrderBy(list => list.StartTime).ToList();
                foreach (var x in meetingsOrdered)
                {
                    if(x.IsAllDay == true)
                    {
                        if(x.StartTime < DateTime.Now)
                        {
                            if ((x.EndTime - x.StartTime).TotalDays <= 1)
                            {
                                string l = (x.StartTime.ToString()).Split()[0];
                                ListViewApp.Items.Add(String.Concat("\n", x.Subject, "->Done", "\n   ", l));
                            }
                            else
                            {
                                string l = (x.StartTime.ToString()).Split()[0];
                                ListViewApp.Items.Add(String.Concat("\n", x.Subject,"-> Done", "\n   ", l, " ", x.StartTime.Hour, "h", x.StartTime.Minute, "m", " -> ", x.EndTime.Hour, "h", x.EndTime.Minute, "m"));
                            }
                        }
                        else
                        {
                            if ((x.EndTime - x.StartTime).TotalDays <= 1)
                            {
                                string l = (x.StartTime.ToString()).Split()[0];
                                ListViewApp.Items.Add(String.Concat("\n", x.Subject, "\n   ", l," - AllDay"));
                            }
                            else
                            {
                                string l = (x.StartTime.ToString()).Split()[0];
                                string c = (x.EndTime.ToString()).Split()[0];
                                ListViewApp.Items.Add(String.Concat("\n", x.Subject,"\n   ", l, " -> ", c));
                            }
                        }
                    }
                    else
                    {

                        if(x.StartTime < DateTime.Now)
                        {
                            string l = (x.StartTime.ToString()).Split()[0];
                            string c = (x.EndTime.ToString()).Split()[0];
                            ListViewApp.Items.Add(String.Concat("\n", x.Subject, "\n   ", l, " ", x.StartTime.Hour, "h", x.StartTime.Minute, "m", " -> ",c," ", x.EndTime.Hour, "h", x.EndTime.Minute, "m"));
                        }
                        else
                        {
                            string l = (x.StartTime.ToString()).Split()[0];
                            ListViewApp.Items.Add(String.Concat("\n", x.Subject, "\n   ",l," ", x.StartTime.Hour,"h",x.StartTime.Minute,"m", " -> ", x.EndTime.Hour,"h", x.EndTime.Minute,"m"));
                        }
                    }    
    





                    //if (x.StartTime < DateTime.Now)
                    //{
                    //    if (x.IsAllDay == true)
                    //    {
                    //        string l = (x.StartTime.ToString()).Split()[0];
                    //        ListViewApp.Items.Add(String.Concat("\n", x.Subject, "->Done", "\n   ", l));
                    //    }
                    //    else
                    //    {
                    //        ListViewApp.Items.Add(String.Concat("\n", x.Subject,"->Done", "\n   ", x.StartTime," -> ", x.EndTime));
                    //    }
                    //}
                    //else
                    //{
                    //    ListViewApp.Items.Add(String.Concat("\n", x.Subject, "\n   ", x.StartTime," -> ",x.EndTime));
                    //}
                }
                int count = ListViewApp.Items.Count;
                labelAppointments.Content = String.Concat("Nº tarefas -> ",count);
            }
        }


        private void Schedule_AppointmentEditorClosing(object sender, AppointmentEditorClosingEventArgs e)
        {
            var appointment = e.Appointment as ScheduleAppointment;
            if (appointment != null)
            {
                LoadSideBar();
               /*e.Handled = true;*/
            }
        }
        private void ViewModel_PerfilFotoGuardada()
        {
            imgFotografia2.Source = _app.ViewModel.MyPerfilFoto.Fotografia;
        }

        private void ViewModel_PerfilFotoCarregada()
        {
            imgFotografia2.Source = _app.ViewModel.MyPerfilFoto.Fotografia;
        }



        private void MainWindow_LOADED(object sender, RoutedEventArgs e)
        {
            txtUtilizador.Text = System.Environment.UserName;
            txtEmail.Text = "exemplo@gmail.com";
        }

        /// <summary>
        ///     Responsavel por guardar os dados as atividades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a"></param>
        public void MainWindow_Closing(object sender, CancelEventArgs a)
        {
            if (DataContext is ScheduleViewModel viewModel)
            {
                viewModel.saveOnClosing();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Botao que fecha a app
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            //Botao que minimiza a app
        }

        private void btnMudar_Perfil_Click(object sender, RoutedEventArgs e)
        {
            Foto_Perfil janelaFoto = new Foto_Perfil(this.txtUtilizador, this.txtEmail);
            janelaFoto.Show();
        }

        private void LoadTask(object sender, CellDoubleTappedEventArgs e)
        {
            LoadSideBar();
        }

        private void LoadTask(object sender, CellTappedEventArgs e)
        {
            LoadSideBar();
        }
    }
}

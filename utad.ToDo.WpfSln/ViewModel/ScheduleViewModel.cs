using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Scheduler;

namespace utad.ToDo.WpfSln{
    public class ScheduleViewModel
    {

        public ObservableCollection<ScheduleAppointment> Events { get; set; }


        /// <summary>
        /// Apenas contem as meeting's que estiverem no ficheiro, já no formato correto
        /// </summary>
        private List<ScheduleAppointment> MeetingsFromJson;

        /// <summary>
        /// Contem todas as meeting's presentes no calendário
        /// </summary>
        public List<ScheduleAppointment> allMeetings;
        /// <summary>
        /// Inicializa o ViewModel e cria uma lista de eventos referentes ao viewmodel
        /// </summary>
        public ScheduleViewModel(){
            Events = new ObservableCollection<ScheduleAppointment>();
            allMeetings = new List<ScheduleAppointment>();
            IntializeAppoitments();
            
        }

        /// <summary>
        /// Metodo que grava as meetings num ficheiro json
        /// </summary>
        public void saveOnClosing() {
            foreach (var item in Events)
            {
                allMeetings.Add(item);
            }
            using (StreamWriter r = new StreamWriter("JsonInput.json"))
            {
                if (r is not null)
                {
                    r.Write(JsonConvert.SerializeObject(allMeetings, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    }));
                    r.Close();
                }
            }
        }

    /// <summary>
    /// Carrega todos os meeting guardado no ficheiro e coloca numa lista todos os meeting's
    /// </summary>
    public void LoadJson(){
            if (File.Exists("JsonInput.json"))
            {
                StreamReader r = new StreamReader("JsonInput.json");
                if (r is not null)
                {
                    string json = r.ReadToEnd();
                    if (json is not null)
                    {
                        MeetingsFromJson = JsonConvert.DeserializeObject<List<ScheduleAppointment>>(json);
                    }
                    r.Close();
                }
            }
        }

        /// <summary>
        /// Carrega todos os meetings do ficheiro para o calendário
        /// </summary>
        public void IntializeAppoitments(){
            LoadJson();
            if (MeetingsFromJson is not null)
            {
                foreach (var item in MeetingsFromJson)
                {
                    Events.Add(item);
                }
            }
        }
    }
}
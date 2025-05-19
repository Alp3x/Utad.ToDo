using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utad.ToDo.WpfSln;

namespace utad.ToDo.Wpf.ViewModel
{
    public class mineScheduleViewModel
    {
        public ObservableCollection<Appointment> Meetings { get; set; }
        public ObservableCollection<Appointment> Events { get; set; }

        /// <summary>
        /// Apenas contem as meeting's que estiverem no ficheiro, já no formato correto
        /// </summary>
        private List<Appointment> MeetingsFromJson;

        /// <summary>
        /// Contem todas as meeting's presentes no calendário
        /// </summary>
        private List<Appointment> allMeetings;

        /// <summary>
        /// Inicializa o ViewModel e cria uma lista de eventos referentes ao viewmodel
        /// </summary>
        public mineScheduleViewModel()
        {
            Events = new ObservableCollection<Appointment>();
            allMeetings = new List<Appointment>();
            IntializeAppoitments();
        }

        /// <summary>
        /// Metodo que grava as meetings num ficheiro json
        /// </summary>
        public void saveOnClosing()
        {
            foreach (var item in Events)
            {
                allMeetings.Add(item);
            }
            using (StreamWriter r = new StreamWriter("JsonInput.json"))
            {
                if (r is not null)
                {
                    r.Write(JsonConvert.SerializeObject(allMeetings));
                    r.Close();
                }
            }
        }


        /// <summary>
        /// Carrega todos os meeting guardado no ficheiro e coloca numa lista todos os meeting's
        /// </summary>
        public void LoadJson()
        {
            if (File.Exists("JsonInput.json"))
            {
                StreamReader r = new StreamReader("JsonInput.json");
                if (r is not null)
                {
                    string json = r.ReadToEnd();
                    if (json is not null)
                    {
                        MeetingsFromJson = JsonConvert.DeserializeObject<List<Appointment>>(json);
                    }
                    r.Close();
                }
            }
        }

        /// <summary>
        /// Carrega todos os meetings do ficheiro para o calendário
        /// </summary>
        public void IntializeAppoitments()
        {
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

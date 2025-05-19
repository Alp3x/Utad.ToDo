using utad.ToDo.Wpf.ViewModels;

namespace utad.ToDo.WpfSln
{
    public partial class App : System.Windows.Application
    {

        private App _app;

        //subcrever Models
        public ModelCompleto ViewModel {  get; set; }

        public ModelTexto ModelTexto { get; set; }

        public ModelFicheiro TratarFicheiro { get; set; }
        

        //subscrever views
        public App()
        {
            ViewModel = new ModelCompleto();
            ModelTexto = new ModelTexto();
            TratarFicheiro = new ModelFicheiro();

        }
    }
}

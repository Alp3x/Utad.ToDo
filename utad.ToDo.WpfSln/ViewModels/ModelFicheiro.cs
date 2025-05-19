using System.IO;

namespace utad.ToDo.Wpf.ViewModels
{
    public class ModelFicheiro
    {

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public event MetodosSemParametros? MudarUsername;
        public event MetodosSemParametros? MudarEmail;

        public ModelFicheiro() {
            LerFicheiro();
        }
        public void LerFicheiro()
        {
            try
            {
                StreamReader streamReader = new StreamReader("dados.txt");

                Email = streamReader.ReadLine();
                Username = streamReader.ReadLine();
            }
            catch
            {
                return;
            }
        }


        public void EscreverFicheiro()
        {
            StreamWriter streamWriter = new StreamWriter("dados.txt");

            streamWriter.WriteLine(Email);
            streamWriter.WriteLine(Username);

            streamWriter.Close();
        }
    }

}

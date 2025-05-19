using Microsoft.Win32;
using Syncfusion.Windows.Shared;
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
using utad.ToDo.Wpf.ViewModels;
using utad.ToDo.WpfSln;

namespace utad.ToDo.Wpf.Views
{
    /// <summary>
    /// Lógica interna para Foto_Perfil.xaml
    /// </summary>
    public partial class Foto_Perfil : Window
    {

        private App _app;

        private TextBlock _Username;

        private TextBlock _Email;


        public Foto_Perfil(TextBlock Username, TextBlock email)
        {
            InitializeComponent();

            _Username = Username;
            _Email = email;

            _app = (App)App.Current as App;
            _app.ViewModel.PerfilFotoCarregada += ViewModel_PerfilFotoCarregada;
            _app.ViewModel.PerfilFotoGuardada += ViewModel_PerfilFotoGuardada;
            _app.ModelTexto.TextoValido += ModelTexto_TextoValido;
            _app.ModelTexto.EmailValido += ModelTexto_EmailValido;
            //txtMudarNome.Text = _app.TratarFicheiro.Username;
            //txtEmail2.Text = _app.TratarFicheiro.Email;
            
        }

        private void ModelTexto_EmailValido(string email)
        {
            MessageBox.Show("Email guardado com Sucesso!",
                "Alteração de Email",
                MessageBoxButton.OK,
                MessageBoxImage.Question);
        }

        private void ModelTexto_TextoValido(string valor)
        {
            MessageBox.Show("Nome guardado com Sucesso!",
                "Alteração do Nome",
                MessageBoxButton.OK,
                MessageBoxImage.Question);

        }

        private void ViewModel_PerfilFotoGuardada()
        {
            MessageBox.Show("Perfil atualizado com sucesso", "Fotografia", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewModel_PerfilFotoCarregada()
        {
            //tbNomeFicheiro.Text = _app.ViewModel.MyPerfilFoto.Ficheiro;
            imgFotografia.Source = _app.ViewModel.MyPerfilFoto.Fotografia;
        }

        private void Foto_Perfil_LOADED(object sender, RoutedEventArgs e)
        {
            //_app.ViewModel.LoadFromTXT("dados.txt");
            _app.ViewModel.LoadFromXML("dados.xml");

        }

        private void btnMudarFoto_CLICK(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Imagens JPG|*.jpg|Imagens PNG|*.png|Todos os ficheiros|*.*";

            if (dlg.ShowDialog() == true)
            {
                //_app.ViewModel.SaveToTXT(dlg.FileName);
                _app.ViewModel.SaveToXML(dlg.FileName);
            }
        }

        private void btnGuardarFoto_CLICK(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnMudarNome_CLICK(object sender, RoutedEventArgs e)
        {
            _app.TratarFicheiro.Username = txtMudarNome.Text;
            _app.TratarFicheiro.EscreverFicheiro();
            _Username.Text = txtMudarNome.Text;

            try
            {
                _app.ModelTexto.TextoInseridoValido(txtMudarNome.Text);
            } catch(ModelException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //mexer com os botões de cima
             
            
            
        }

        private void btnGuardarEmail_CLICK(object sender, RoutedEventArgs e)
        {
            _app.TratarFicheiro.Email = txtEmail2.Text;
            _app.TratarFicheiro.EscreverFicheiro();
            _Email.Text = txtEmail2.Text;

            try
            {
                _app.ModelTexto.EmailInseridoValido(txtEmail2.Text);
            }
            catch (ModelException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnReset_CLICK(object sender, RoutedEventArgs e)
        {
            _Username.Text = System.Environment.UserName;
            _Email.Text = "exemplo@gmail.com";
            
            
            MessageBox.Show("Perfil Restaurdado", "Restaurar Perfil", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();
        }
    }
}

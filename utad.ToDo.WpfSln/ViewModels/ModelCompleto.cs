using System.Windows.Media.Imaging;
using System.Xml.Linq;
using utad.ToDo.Wpf.Models_New;
using System.IO;

namespace utad.ToDo.Wpf.ViewModels
{
    public class ModelCompleto
    {
            public PerfilFoto? MyPerfilFoto { get; private set; } = new PerfilFoto();

            public event MetodosSemParametros? PerfilFotoCarregada;
            public event MetodosSemParametros? PerfilFotoGuardada;

            private string _caminhoBase;
            private string _caminhoTotal;
            private string _caminhoFotos;

            public ModelCompleto()
            {
                _caminhoBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (Directory.Exists(System.IO.Path.Combine(_caminhoBase, "PL5_G03")) == false)
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(_caminhoBase, "PL5_G03"));
                }
                _caminhoTotal = System.IO.Path.Combine(_caminhoBase, "PL5_G03");

                if (Directory.Exists(System.IO.Path.Combine(_caminhoTotal, "Images")) == false)
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(_caminhoTotal, "Images"));
                }

                _caminhoFotos = System.IO.Path.Combine(_caminhoTotal, "Images");
            }

            public void LoadFromTXT(string ficheiro)
            {
                if (File.Exists(System.IO.Path.Combine(_caminhoTotal, ficheiro)) != false)
                {
                    string nomeFoto = File.ReadAllText(System.IO.Path.Combine(_caminhoTotal, "dados.txt"));
                    var uri = new Uri(System.IO.Path.Combine(_caminhoFotos, nomeFoto));
                    MyPerfilFoto.Fotografia = new BitmapImage(uri);
                    MyPerfilFoto.Fotografia.Freeze();

                    MyPerfilFoto.Ficheiro = ficheiro;
                }
                else
                {
                    _LoadSemFoto();
                }

                if (PerfilFotoCarregada != null)
                {
                    PerfilFotoCarregada();
                }
            }

            public void SaveToTXT(string ficheiro)
            {
                string NomeFoto = System.IO.Path.GetFileName(ficheiro);
                File.Copy(ficheiro, System.IO.Path.Combine(_caminhoFotos, NomeFoto), true);
                File.WriteAllText(System.IO.Path.Combine(_caminhoTotal, "dados.txt"), NomeFoto);

                LoadFromTXT("dados.txt");

                if (PerfilFotoGuardada != null)
                {
                    PerfilFotoGuardada();
                }
            }

            public void LoadFromXML(string ficheiro)
            {
                if (File.Exists(System.IO.Path.Combine(_caminhoTotal, ficheiro)) != false)
                {
                    XDocument doc = XDocument.Load(System.IO.Path.Combine(_caminhoTotal, "dados.xml"));
                    string nomeFoto = doc.Element("perfil").Attribute("fotografia").Value;

                    var uri = new Uri(System.IO.Path.Combine(_caminhoFotos, nomeFoto));
                    MyPerfilFoto.Fotografia = new BitmapImage(uri);
                    MyPerfilFoto.Fotografia.Freeze();

                    MyPerfilFoto.Ficheiro = ficheiro;
                }
                else
                {
                    _LoadSemFoto();
                }

                if (PerfilFotoCarregada != null)
                {
                    PerfilFotoCarregada();
                }
            }

            public void SaveToXML(string ficheiro)
            {
                string NomeFoto = System.IO.Path.GetFileName(ficheiro);
                File.Copy(ficheiro, System.IO.Path.Combine(_caminhoFotos, NomeFoto), true);

                XDocument doc = new XDocument();
                doc.Add(new XElement("perfil", new XAttribute("fotografia", NomeFoto)));
                doc.Save(System.IO.Path.Combine(_caminhoTotal, "dados.xml"));

                LoadFromXML("dados.xml");

                if (PerfilFotoGuardada != null)
                {
                    PerfilFotoGuardada();
                }
            }

            private void _LoadSemFoto()
            {

            string pathToIcoFile = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\utilizadorDefaultIcon.png";
            //var uri = new Uri("pack://application:,,\\Images\\NoPhoto.jpg");
            MyPerfilFoto.Fotografia = new BitmapImage(new Uri(pathToIcoFile));
            MyPerfilFoto.Fotografia.Freeze();
            MyPerfilFoto.Ficheiro = "[sem foto]";
        }
        }
    }


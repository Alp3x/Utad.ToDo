using System.Windows.Media.Imaging;
using utad.ToDo.Wpf.Models.Share;

namespace utad.ToDo.Wpf.Models
{
    public class Perfil : BaseModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public BitmapImage Fotografia { get; set; }

        public Perfil(BitmapImage fotografia)
        {
            Fotografia = fotografia;
        }
    }
}

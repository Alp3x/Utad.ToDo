namespace utad.ToDo.Wpf.ViewModels
{

    public delegate void T(string valor);

    public delegate void E(string email);
    public class ModelTexto
    {
        public event T? TextoValido;

        public event E? EmailValido;

        public void TextoInseridoValido(string texto)
        {
            if(texto.Length <= 0 ){
                throw new ModelException("O tamanho do nome é errado.");
            }

            TextoValido?.Invoke(texto);
        }

        public void EmailInseridoValido(string email)
        {
            if(email.Length <= 0)
            {
                throw new ModelException("O tamanho de email é errado.");
            }
            EmailValido?.Invoke(email);
        }

        
    }
}

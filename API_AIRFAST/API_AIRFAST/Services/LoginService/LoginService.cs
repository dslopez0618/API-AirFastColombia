using API_AIRFAST.Logic;

namespace API_AIRFAST.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly LoginLogic _loginLogic;

        // Inyectamos la lógica al servicio
        public LoginService()
        {
            _loginLogic = new LoginLogic();
        }

        public bool ValidarUsuario(string email, string contrasena)
        {
            // Utilizamos la lógica de validación de LoginLogic
            return _loginLogic.ValidarUsuario(email, contrasena);
        }
    }
}

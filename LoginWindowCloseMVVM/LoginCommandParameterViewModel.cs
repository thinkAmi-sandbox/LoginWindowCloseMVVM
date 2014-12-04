using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Windows.Input;             // ICommand
using System.Security.Principal;        // GenericIdentity, GenericPrincipal
using System.Threading;                 // Thread

namespace LoginWindowCloseMVVM
{
    public class LoginCommandParameterViewModel : ViewModelBase
    {
        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(ExecuteLoginCommand);
                }
                return _loginCommand;
            }
        }

        private void ExecuteLoginCommand(object x)
        {
            var identity = new GenericIdentity("CommandParamter");
            var principal = new GenericPrincipal(identity, new string[] { "fuga" });
            Thread.CurrentPrincipal = principal;


            if (x != null)
            {
                var window = (System.Windows.Window)x;

                // SystemCommandsとClose()メソッド、どちらを使っても動く
                System.Windows.SystemCommands.CloseWindow(window);
                //window.Close();
            }
        }
    }
}

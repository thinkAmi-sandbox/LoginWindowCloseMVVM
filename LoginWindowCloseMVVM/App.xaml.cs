using System.Windows;   // Application, ShutdownMode, etc
using System.Threading; // Thread

namespace LoginWindowCloseMVVM
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public App()
            : base()
        {
            // 使わないパターンはコメントアウトする
            // パターン1. XAMLに書いたEventTrigerを使用する方法
            //Startup += (s, e) => Login(new LoginEventTrigerView());

            // パターン2. コードビハインドでDataContextChangedイベントを使用する方法
            //Startup += (s, e) => Login(new LoginDataContextChangedView());

            // パターン3. CommandParameterでViewを渡して、SystemCommands.CloseWindowを使用する方法
            Startup += (s, e) => LoginByParameterCommand(new LoginCommandParameterView());
        }


        private void Login(Window loginView)
        {
            //  ログイン画面が閉じられた時にアプリケーションが終了しないよう、OnExplicitShutdownを設定しておく
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var isLoggedin = loginView.ShowDialog() ?? false;
            var isAuthenticated = Thread.CurrentPrincipal.Identity.IsAuthenticated;

            Execute(isLoggedin && isAuthenticated);
        }

        private void LoginByParameterCommand(Window loginView)
        {
            //  ログイン画面が閉じられた時にアプリケーションが終了しないよう、OnExplicitShutdownを設定しておく
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // ShowDialogしても、DialogResultをセットしてないので、この場合は判定に使えない
            loginView.ShowDialog();
            var isAuthenticated = Thread.CurrentPrincipal.Identity.IsAuthenticated;

            Execute(isAuthenticated);
        }


        private void Execute(bool canContinue)
        {
            if (canContinue)
            {
                //  Current.MainWindowが閉じられた時にアプリケーションが終了するように変更
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                var vm = new LoggedinViewModel();
                vm.UserName = Thread.CurrentPrincipal.Identity.Name;

                var loggedinView = new LoggedinView();
                loggedinView.DataContext = vm;

                Current.MainWindow = loggedinView;
                loggedinView.ShowDialog();
            }
            else
            {
                //  OnExplicitShutdownの場合、明示的なShutdown()呼び出しが必要
                Current.Shutdown(-1);
            }
        }
    }
}

﻿using System.Windows;   // Application, ShutdownMode, etc
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
            // 使わない方はコメントアウトする
            // パターン1. XAMLに書いたEventTrigerを使用する方法
            //Startup += (s, e) => Login(new LoginEventTrigerView());

            // パターン2. コードビハインドでDataContextChangedイベントを使用する方法
            Startup += (s, e) => Login(new LoginDataContextChangedView());
        }


        private void Login(Window loginView)
        {
            //  ログイン画面が閉じられた時にアプリケーションが終了しないよう、OnExplicitShutdownを設定しておく
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var isLoggedin = loginView.ShowDialog() ?? false;
            var isAuthenticated = Thread.CurrentPrincipal.Identity.IsAuthenticated;

            if (isLoggedin && isAuthenticated)
            {
                //  MainWindowが閉じられた時にアプリケーションが終了するように変更
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

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Runtime.CompilerServices;  // CallerMemberName属性

namespace LoginWindowCloseMVVM
{
    public class LoggedinViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        protected override void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
        }
    }
}

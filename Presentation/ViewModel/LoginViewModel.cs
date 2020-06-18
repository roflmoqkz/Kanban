using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string loginEmail = "";
        private string loginPassword = "";
        private string registerEmail = "";
        private string registerPassword = "";
        private string nickname = "";
        private bool join = false;
        private string hostEmail = "";

        public LoginViewModel()
        {
        }

        public string LoginEmail { get { return loginEmail; } set { loginEmail = value; RaisePropertyChanged("LoginEmail"); } }
        public string LoginPassword { get { return loginPassword; } set { loginPassword = value; RaisePropertyChanged("LoginPassword"); } }
        public string RegisterEmail { get { return registerEmail; } set { registerEmail = value; RaisePropertyChanged("RegisterEmail"); } }
        public string RegisterPassword { get { return registerPassword; } set { registerPassword = value; RaisePropertyChanged("RegisterPassword"); } }
        public string Nickname { get { return nickname; } set { nickname = value; RaisePropertyChanged("Nickname"); } }
        public bool Join { get { return join; } set { join = value; RaisePropertyChanged("Join"); } }
        public string HostEmail { get { return hostEmail; } set { hostEmail = value; RaisePropertyChanged("HostEmail"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}

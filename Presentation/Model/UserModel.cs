using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class UserModel : NotifiableModelObject
    {
        private string email;
        public string Email { get { return email;} set { email = value; RaisePropertyChanged("Email"); } }

        public BoardModel GetBoard()
        {
            return Controller.GetBoard(email);
        }
        public UserModel(BackendController controller,string email) : base(controller)
        {
            this.email = email;
        }
    }
}

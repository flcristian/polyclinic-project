using polyclinic_project.system.interfaces;
using polyclinic_project.user.model.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.model
{
    public class User : IUserBuilder, IPrototype<User>, IComparable<User>, IHasId
    {
        private Int32 id;
        private String name;
        private String email;
        private String phone;
        private UserType type;

        #region CONSTRUCTORS

        public User(Int32 id, String name, String email, String phone, UserType type)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.type = type;
        }

        public User(User user)
        {
            this.id = user.id;
            this.name = user.name;
            this.email = user.email;
            this.phone = user.phone;
            this.type = user.type;
        }

        public User()
        {
            this.id = -1;
            this.name = "name";
            this.email = "email";
            this.phone = "phone";
            this.type = UserType.NONE;
        }

        #endregion

        #region ACCESSORS

        public Int32 GetId() { return this.id; }

        public String GetName() { return this.name; }

        public String GetEmail() { return this.email; }

        public String GetPhone() { return this.phone; }

        public UserType GetType() { return this.type; }

        public void SetId(Int32 id) { this.id = id; }

        public void SetName(String name) { this.name = name; }

        public void SetEmail(String email) { this.email = email; }

        public void SetPhone(String phone) { this.phone = phone; }

        public void SetType(UserType type) { this.type = type; }

        #endregion

        #region BUILDER

        public User Id(Int32 id)
        {
            this.id = id;
            return this;
        }

        public User Name(String name)
        {
            this.name = name;
            return this;
        }

        public User Email(String email)
        {
            this.email = email;
            return this;
        }

        public User Phone(String phone)
        {
            this.phone = phone;
            return this;
        }

        public User Type(UserType type)
        {
            this.type = type;
            return this;
        }

        #endregion

        #region PUBLIC_METHODS

        public override Boolean Equals(object? obj)
        {
            User user = obj as User;
            return user.id == this.id && user.name == this.name && user.email == this.email && user.phone == this.phone && user.type == this.type;
        }

        public override String ToString()
        {
            String desc = $"{this.type.GetString().ToUpper()}\n=-=-=-=-=-=-=-=\n";
            desc += $"Id : {this.id}\n";
            desc += $"Name : {this.name}\n";
            desc += $"Email : {this.email}\n";
            desc += $"Phone : {this.phone}\n";
            return desc;
        }

        public override Int32 GetHashCode()
        {
            return (int)Math.Pow(this.id, this.email.Length / 2) + this.name.Length * 3;
        }

        public Int32 CompareTo(User? user)
        {
            if(this.id > user.id)
            {
                return 1;
            } 
            else if (this.id == user.id)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public User Clone()
        {
            return new User(this);
        }

        public String ToSave()
        {
            return $"{this.id}/{this.name}/{this.email}/{this.phone}/{this.type.GetString()}\n";
        }

        #endregion
    }
}

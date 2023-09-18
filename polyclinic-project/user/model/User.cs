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
    public class User : IUserBuilder, IPrototype<User>, IComparable<User>, ISaveable, IHasId
    {
        private Int32 _id;
        private String _name;
        private String _email;
        private String _phone;
        private UserType _type;

        #region CONSTRUCTORS

        public User(Int32 id, String name, String email, String phone, UserType type)
        {
            _id = id;
            _name = name;
            _email = email;
            _phone = phone;
            _type = type;
        }

        public User(User user)
        {
            _id = user._id;
            _name = user._name;
            _email = user._email;
            _phone = user._phone;
            _type = user._type;
        }

        public User()
        {
            _id = -1;
            _name = "name";
            _email = "email";
            _phone = "phone";
            _type = UserType.NONE;
        }

        #endregion

        #region ACCESSORS

        public Int32 GetId() { return _id; }

        public String GetName() { return _name; }

        public String GetEmail() { return _email; }

        public String GetPhone() { return _phone; }

        public UserType GetType() { return _type; }

        public void SetId(Int32 id) { _id = id; }

        public void SetName(String name) { _name = name; }

        public void SetEmail(String email) { _email = email; }

        public void SetPhone(String phone) { _phone = phone; }

        public void SetType(UserType type) { _type = type; }

        #endregion

        #region BUILDER

        public User Id(Int32 id)
        {
            _id = id;
            return this;
        }

        public User Name(String name)
        {
            _name = name;
            return this;
        }

        public User Email(String email)
        {
            _email = email;
            return this;
        }

        public User Phone(String phone)
        {
            _phone = phone;
            return this;
        }

        public User Type(UserType type)
        {
            _type = type;
            return this;
        }

        #endregion

        #region PUBLIC_METHODS

        public override Boolean Equals(object? obj)
        {
            User user = obj as User;
            return user._id == _id && user._name == _name && user._email == _email && user._phone == _phone && user._type == _type;
        }

        public override String ToString()
        {
            String desc = $"{_type.GetString().ToUpper()}\n=-=-=-=-=-=-=-=\n";
            desc += $"Id : {_id}\n";
            desc += $"Name : {_name}\n";
            desc += $"Email : {_email}\n";
            desc += $"Phone : {_phone}\n";
            return desc;
        }

        public override Int32 GetHashCode()
        {
            return (int)Math.Pow(_id, _email.Length / 2) + _name.Length * 3;
        }

        public Int32 CompareTo(User? user)
        {
            if(_id > user._id)
            {
                return 1;
            } 
            else if (_id == user._id)
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
            return $"{_id}/{_name}/{_email}/{_phone}/{_type.GetString()}\n";
        }

        #endregion
    }
}

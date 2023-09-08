using polyclinic_project.system.interfaces;
using polyclinic_project.user.model.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace polyclinic_project.user.model
{
    public class UserFactory : IFactory<User>
    {
        private const String DOCTOR = "doctor";
        private const String PACIENT = "pacient";

        public User CreateObject(String text)
        {
            String[] data = text.Split('/');

            String type = data[4];

            switch (data[4].ToLower())
            {
                case DOCTOR:
                    return IUserBuilder.BuildDoctor()
                        .Id(Int32.Parse(data[0]))
                        .Name(data[1])
                        .Email(data[2])
                        .Phone(data[3])
                        .Type(UserType.DOCTOR);
                case PACIENT:
                    return IUserBuilder.BuildPacient()
                        .Id(Int32.Parse(data[0]))
                        .Name(data[1])
                        .Email(data[2])
                        .Phone(data[3])
                        .Type(UserType.PACIENT);
                default:
                    return IUserBuilder.BuildUser()
                        .Id(Int32.Parse(data[0]))
                        .Name(data[1])
                        .Email(data[2])
                        .Phone(data[3]);
            }
        }
    }
}

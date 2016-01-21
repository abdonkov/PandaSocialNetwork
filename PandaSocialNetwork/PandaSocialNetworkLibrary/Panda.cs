using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace PandaSocialNetworkLibrary
{
    public enum Gender { Male, Female };

    public class Panda
    {
        public string PandaName { get; set; }
        public string PandaEmail { get; set; }
        public bool IsMale { get; set; }
        public bool IsFemale { get; set; }

        public Panda(string pandaName, string pandaEmail, Gender gender)
        {
            this.PandaName = pandaName;
            this.PandaEmail = pandaEmail;

            switch (gender)
            {
                case Gender.Male:
                    this.IsFemale = false;
                    this.IsMale = true;
                    break;
                case Gender.Female:
                    this.IsMale = false;
                    this.IsFemale = true;
                    break;
                default:
                    break;
            }
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("Panda name: {0}, Panda email: {1}, Gender: {2}", this.PandaName, this.PandaEmail,
                IsMale ? Gender.Male.ToString() : Gender.Female.ToString());
        }

        public override int GetHashCode()
        {
            int hash = 13;

            unchecked
    {
                hash = hash + this.PandaName.GetHashCode() * 17;
                hash = hash + this.PandaEmail.GetHashCode() * 17;
                hash = hash + this.IsFemale.GetHashCode() * 17;
                hash = hash + this.IsMale.GetHashCode() * 17;
            }

            return hash;
        }
    }
}

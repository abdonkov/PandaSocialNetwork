using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PandaSocialNetworkLibrary
{
    public enum GenderType
    {
        Male,
        Female
    };

    public class Panda
    {
        public string PandaName { get; private set; }
        public string PandaEmail { get; private set; }
        public bool IsMale { get; private set; }
        public bool IsFemale { get; private set; }

        public Panda(string pandaName, string pandaEmail, GenderType gender)
        {
            this.PandaName = pandaName;
            this.PandaEmail = pandaEmail;

            if (GenderType.Male == gender)
            {
                this.IsMale = true;
            }
            else
            {
                this.IsFemale = true;
            }

            if (!IsValidEmail(pandaEmail))
            {
                throw new ArgumentException("Invalid Email!");
            }

            if (!IsValidName(pandaName))
            {
                throw new ArgumentException("The name contains some invalid symbols!");
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

        private static bool IsValidName(string name)
        {
            bool isValid = true;

            if (!Regex.Match(name, "[a-zA-Z]+$").Success)
            {
                isValid = false;
            }

            return isValid;
        }

        public override bool Equals(object obj)
        {
            var panda = obj as Panda;

            if (panda != null)
            {
                if (this.PandaName.Equals((panda).PandaName) && this.PandaEmail.Equals((panda).PandaEmail) &&
                this.IsMale.Equals((panda).IsMale) && this.IsFemale.Equals((panda).IsFemale))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("Panda name: {0}, Panda email: {1}, Gender: {2}", this.PandaName, this.PandaEmail,
                IsMale ? GenderType.Male.ToString() : GenderType.Female.ToString());
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

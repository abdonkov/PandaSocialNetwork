using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    using PandaSocialNetworkLibrary;
    [TestClass]
    public class UnitTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InvalidEmail()
        {
            var invalidEmail = new Panda("Goshko","goshkomail.com", GenderType.Male);
            Assert.AreEqual(invalidEmail.PandaEmail, "goshkomail.com");
        }

        [TestMethod]
        public void IfValidName()
        {
            var validName = new Panda("Pesho", "123@abv.bg", GenderType.Male);
            Assert.AreEqual("Pesho", validName.PandaName);
        }

        [TestMethod, ExpectedException(typeof(PandaSocialNetwork.PandaAlreadyThereException))]
        public void IfThereIsPanda()
        {
            var thereIsPanda = new PandaSocialNetwork();
            var panda = new Panda("Kostadin", "kostaa@dsd.bg", GenderType.Male);
            thereIsPanda.AddPanda(panda);
            thereIsPanda.AddPanda(panda);
        }

        [TestMethod, ExpectedException(typeof(PandaSocialNetwork.PandasAlreadyFriendsException))]
        public void IfAlreadyFriends()
        {
            var alreadyFriends = new PandaSocialNetwork();
            var panda1 = new Panda("Orhan", "orhancho@dsd.bg", GenderType.Male);
            var panda2 = new Panda("Mumun", "mumun@dsd.bg", GenderType.Male);
            alreadyFriends.MakeFriends(panda1, panda2);
            alreadyFriends.MakeFriends(panda1, panda2);
        }

        [TestMethod]
        public void hasPandaInTheNetwork()
        {
            var panda = new Panda("Stefan", "stefan@dsd.bg", GenderType.Male);
            var network = new PandaSocialNetwork();
            bool hasPanda = false;

            network.AddPanda(panda);

            if (network.HasPanda(panda))
            {
                hasPanda = true;
            }

            Assert.IsTrue(hasPanda);
        }
    }
}

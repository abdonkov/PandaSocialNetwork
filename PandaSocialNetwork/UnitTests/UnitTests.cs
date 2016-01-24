using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandaSocialNetworkLibrary;
using System.Diagnostics;

namespace UnitTests
{
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

        [TestMethod, ExpectedException(typeof(PandaAlreadyThereException))]
        public void IfThereIsPanda()
        {
            var thereIsPanda = new PandaSocialNetwork();
            var panda = new Panda("Kostadin", "kostaa@dsd.bg", GenderType.Male);
            thereIsPanda.AddPanda(panda);
            thereIsPanda.AddPanda(panda);
        }

        [TestMethod, ExpectedException(typeof(PandasAlreadyFriendsException))]
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

        [TestMethod]
        public void NetworkSerializeAndDeserialize()
        {
            var savedNetwork = new PandaSocialNetwork();
            var ivo = new Panda("Ivo", "ivo@pandamail.com", GenderType.Male);
            var rado = new Panda("Rado", "rado@pandamail.com", GenderType.Male);
            var tony = new Panda("Tony", "tony@pandamail.com", GenderType.Female);

            savedNetwork.AddPanda(ivo);
            savedNetwork.AddPanda(rado);
            savedNetwork.AddPanda(tony);

            savedNetwork.MakeFriends(ivo, rado);
            savedNetwork.MakeFriends(rado, tony);

            var pandaSerializer = new PandaSocialNetworkBinarySerializer("unitTest.dat");
            pandaSerializer.Save(savedNetwork);
            var loadedNetwork = pandaSerializer.Load();

            Assert.IsTrue(
                savedNetwork.HasPanda(ivo)
                == loadedNetwork.HasPanda(ivo)
                && savedNetwork.HasPanda(rado)
                == loadedNetwork.HasPanda(rado)
                && savedNetwork.HasPanda(tony)
                == loadedNetwork.HasPanda(tony)
                && savedNetwork.HasPanda(new Panda("a", "a@abv.bg", GenderType.Male))
                == loadedNetwork.HasPanda(new Panda("a", "a@abv.bg", GenderType.Male))
                && savedNetwork.ConnectionLevel(ivo, rado)
                == loadedNetwork.ConnectionLevel(ivo, rado)
                && savedNetwork.ConnectionLevel(ivo, tony)
                == loadedNetwork.ConnectionLevel(ivo, tony)
                && savedNetwork.HowManyGenderInNetwork(2, ivo, GenderType.Female)
                == loadedNetwork.HowManyGenderInNetwork(2, ivo, GenderType.Female)
                );
        }
    }
}

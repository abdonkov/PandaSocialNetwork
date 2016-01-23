using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaSocialNetworkLibrary
{
    public class PandaSocialNetwork
    {
        Dictionary<Panda, Node> pandas;

        public PandaSocialNetwork()
        {
            pandas = new Dictionary<Panda, Node>();
        }

        public void AddPanda(Panda panda)
        {
            if (pandas.ContainsKey(panda))
            {
                throw new PandaAlreadyThereException();
            }
            else
            {
                pandas.Add(panda, new Node(panda));
            }
        }

        public bool HasPanda(Panda panda)
        {
            return pandas.ContainsKey(panda);
        }

        public void MakeFriends(Panda panda1, Panda panda2)
        {
            Node panda1Node, panda2Node;
            bool alredyFriends = true;

            if (!pandas.ContainsKey(panda1))
            {
                panda1Node = new Node(panda1);
                pandas.Add(panda1, panda1Node);
                alredyFriends = false;
            }
            else
            {
                panda1Node = pandas[panda1];
            }

            if (!pandas.ContainsKey(panda2))
            {
                panda2Node = new Node(panda2);
                pandas.Add(panda2, panda2Node);
                alredyFriends = false;
            }
            else
            {
                panda2Node = pandas[panda2];
            }

            if (alredyFriends)
            {
                throw new PandasAlreadyFriendsException();
            }
            else
            {
                panda1Node.Friends.Add(panda2Node);
                panda2Node.Friends.Add(panda1Node);
            }
        }

        class Node
        {
            public Panda Panda { get; internal set; }
            public HashSet<Node> Friends { get; internal set; }

            public Node(Panda panda)
            {
                Panda = panda;
                Friends = new HashSet<Node>();
            }
        }

        [Serializable]
        public class PandaAlreadyThereException : Exception
        {
            public PandaAlreadyThereException() : base("Panda already there!") { }
            public PandaAlreadyThereException(string message) : base(message) { }
            public PandaAlreadyThereException(string message, Exception inner) : base(message, inner) { }
            protected PandaAlreadyThereException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }

        [Serializable]
        public class PandasAlreadyFriendsException : Exception
        {
            public PandasAlreadyFriendsException() : base("Pandas are already friends!") { }
            public PandasAlreadyFriendsException(string message) : base(message) { }
            public PandasAlreadyFriendsException(string message, Exception inner) : base(message, inner) { }
            protected PandasAlreadyFriendsException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }
    }
}

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
            bool notInNetwork = false;

            if (!HasPanda(panda1))
            {
                panda1Node = new Node(panda1);
                pandas.Add(panda1, panda1Node);
                notInNetwork = true;
            }
            else
            {
                panda1Node = pandas[panda1];
            }

            if (!HasPanda(panda2))
            {
                panda2Node = new Node(panda2);
                pandas.Add(panda2, panda2Node);
                notInNetwork = true;
            }
            else
            {
                panda2Node = pandas[panda2];
            }

            if (notInNetwork)
            {
                panda1Node.Friends.Add(panda2Node);
                panda2Node.Friends.Add(panda1Node);
            }
            else
            {
                if (panda1Node.Friends.Contains(panda2Node))
                {
                    throw new PandasAlreadyFriendsException();
                }
                else
                {
                    panda1Node.Friends.Add(panda2Node);
                    panda2Node.Friends.Add(panda1Node);
                }
            }
        }

        public bool AreFriends(Panda panda1, Panda panda2)
        {
            if (HasPanda(panda1) && HasPanda(panda2))
            {
                Node panda1Node = pandas[panda1];
                Node panda2Node = pandas[panda2];

                if (panda1Node.Friends.Contains(panda2Node))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public List<Panda> FriendsOf(Panda panda)
        {
            if (HasPanda(panda))
            {
                return pandas[panda].Friends.Select(x => x.Panda).ToList();
            }
            else
            {
                return new List<Panda>();
            }
        }

        public int ConnectionLevel(Panda panda1, Panda panda2)
        {
            Node panda1Node, panda2Node;

            if (HasPanda(panda1) && HasPanda(panda2))
            {
                panda1Node = pandas[panda1];
                panda2Node = pandas[panda2];
            }
            else
            {
                return -1;
            }

            HashSet<Node> visited = new HashSet<Node>();
            Queue<Node> nodesQueue = new Queue<Node>();
            Queue<int> levelQueue = new Queue<int>();

            nodesQueue.Enqueue(panda1Node);
            levelQueue.Enqueue(0);

            while (nodesQueue.Count > 0)
            {
                Node curNode = nodesQueue.Dequeue();
                int curLevel = levelQueue.Dequeue();

                if (curNode.Equals(panda2Node))
                {
                    return curLevel;
                }

                foreach (var friend in curNode.Friends)
                {
                    if (!visited.Contains(friend))
                    {
                        visited.Add(friend);
                        nodesQueue.Enqueue(friend);
                        levelQueue.Enqueue(curLevel + 1);
                    }
                }
            }

            return -1;
        }

        public bool AreConnected(Panda panda1, Panda panda2)
        {
            return ConnectionLevel(panda1, panda2) != -1;
        }

        public int HowManyGenderInNetwork(int level, Panda panda, GenderType gender)
        {
            Node pandaNode;

            if (HasPanda(panda))
            {
                pandaNode = pandas[panda];
            }
            else
            {
                return -1;
            }

            HashSet<Node> visited = new HashSet<Node>();
            Queue<Node> nodesQueue = new Queue<Node>();
            Queue<int> levelQueue = new Queue<int>();
            int genderCount = 0;

            nodesQueue.Enqueue(pandaNode);
            levelQueue.Enqueue(0);

            while (nodesQueue.Count > 0)
            {
                Node curNode = nodesQueue.Dequeue();
                int curLevel = levelQueue.Dequeue();

                if (curLevel <= level)
                {
                    switch (gender)
                    {
                        case GenderType.Male:
                            if (curNode.Panda.IsMale) genderCount++;
                            break;
                        case GenderType.Female:
                            if (curNode.Panda.IsFemale) genderCount++;
                            break;
                        default:
                            break;
                    }
                }

                if (level == 2)
                {
                    continue;
                }

                foreach (var friend in curNode.Friends)
                {
                    if (!visited.Contains(friend))
                    {
                        visited.Add(friend);
                        nodesQueue.Enqueue(friend);
                        levelQueue.Enqueue(curLevel + 1);
                    }
                }
            }

            return genderCount;
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
            : base(info, context)
        { }
    }


    [Serializable]
    public class PandasAlreadyFriendsException : Exception
    {
        public PandasAlreadyFriendsException() : base("Pandas already friends!") { }
        public PandasAlreadyFriendsException(string message) : base(message) { }
        public PandasAlreadyFriendsException(string message, Exception inner) : base(message, inner) { }
        protected PandasAlreadyFriendsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

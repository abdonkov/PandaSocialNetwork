using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PandaSocialNetworkLibrary
{
    public class PandaSocialNetworkBinarySerializer : IPandaSocialNetworkStorageProvider
    {
        private string filename;

        public PandaSocialNetworkBinarySerializer(string filename)
        {
            this.filename = filename;
        }

        public void Save(PandaSocialNetwork network)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, network);
            }
        }

        public PandaSocialNetwork Load()
        {
            PandaSocialNetwork network = null;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                network = (PandaSocialNetwork)bf.Deserialize(fs);
            }
            return network;
        }
    }
}

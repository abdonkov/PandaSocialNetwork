using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaSocialNetworkLibrary
{
    public interface IPandaSocialNetworkStorageProvider
    {
        void Save(PandaSocialNetwork network);

        PandaSocialNetwork Load();
    }
}

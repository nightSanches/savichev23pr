using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace savichev23pr.Interfaces
{
    public interface IGift
    {
        void Save(bool Update = false);
        List<Classes.GiftContext> AllGifts();
        void Delete();
    }
}

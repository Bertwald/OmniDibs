using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Models {
    [Flags]
    public enum Privileges : byte {
        None        = 0B_0000_0000,
        User        = 0B_0000_0001,
        Admin       = 0B_0000_0010,
        Provider    = 0B_0000_0100,
        Owner       = 0B_0000_1000,
        Create      = 0B_0100_0000,
        Read        = 0B_0001_0000,
        Update      = 0B_0010_0000,
        Delete      = 0B_1000_0001
    }

}

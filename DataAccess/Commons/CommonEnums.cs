using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Commons
{
    public static class CommonEnums
    {
        public class CANDIDATE_STATUS
        {
            public const int ACCEPT = 1;
            public const int DENY = 2;
        }

        public class POST_STATUS {
            public const int AVAILABLE = 1;
            public const int CLOSE = 2;
            public const int REPORT = 3;
        }
        
    }
}

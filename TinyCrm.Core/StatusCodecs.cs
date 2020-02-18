using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core
{
    public enum StatusCodecs
    { 
        OK = 200,

        NotFOUND = 404,

        BADREQUEST = 400,

        InternalServerError = 500, 

        Conflict = 409
    }
}

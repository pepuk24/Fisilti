using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum LogType
    {
        Insert = 1,
        Update, 
        Delete,
        Error,
        Warning,
        NotFound,
        NonValidation
    }
}

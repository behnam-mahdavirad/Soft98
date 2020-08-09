using System;
using System.Collections.Generic;
using System.Text;

namespace Soft98.Core.Classes
{
    public class CodeGenerator
    {
        public static string ActiveCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        } // end method ActiveCode

    } // end public class CodeGenerator

} // end namespace Soft98.Core.Classes

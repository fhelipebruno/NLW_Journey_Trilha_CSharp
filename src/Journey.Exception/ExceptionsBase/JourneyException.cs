using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public abstract class JourneyException : SystemException
    {
        public JourneyException(string mensagem) : base(mensagem)
        {
            
        }

        public abstract HttpStatusCode GetStatusCode();

    }
}

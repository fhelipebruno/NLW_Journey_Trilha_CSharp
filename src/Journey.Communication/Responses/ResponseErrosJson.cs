﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Communication.Responses
{
    public class ResponseErrosJson
    {
        public IList<string> Errrors { get; set; } = [];

        public ResponseErrosJson(IList<string> errors)
        {
            Errrors = errors;
        }
    }
}

﻿using System;
using System.IO;
using System.Text;

namespace cscheme
{
    public class InPort
    {
        readonly TextReader reader;

        public InPort(TextReader textReader)
        {
            this.reader = textReader;
        }

        public object NextToken()
        {
            return null;
        }
    }
}

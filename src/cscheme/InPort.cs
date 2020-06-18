using System;
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
        }
    }
}

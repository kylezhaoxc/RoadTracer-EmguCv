using System;

namespace RoadTracer
{
    public class TextException : ApplicationException
    {
        public TextException(string message) : base(message)
        { }
    }
}

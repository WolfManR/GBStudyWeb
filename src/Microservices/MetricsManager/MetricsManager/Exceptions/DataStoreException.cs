using System;

namespace MetricsManager.Exceptions
{
    public class DataStoreException : Exception
    {
        public string Parameter { get; }
        
        public DataStoreException(string message, string parameter = null) : base(message) => Parameter = parameter;

        public DataStoreException(string message, Exception innerException) : base(message,innerException){ }
    }
}
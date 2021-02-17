using System;
using System.Collections.Generic;

namespace ConfigClient
{
    public class ConfigDeserializer
    {
        private const char LIST_DELIMITER = '|';
        private const string UNDEFINED_DATATYPE = "Undefined";

        public bool GetboolKey(IDictionary<string, ConfigKey> collection, string keyName)
        {
            var configkey = collection[keyName];

            if (configkey.DataType != "bool")
                throw new Exception($"'{keyName}' is a '{configkey.DataType ?? UNDEFINED_DATATYPE}', not a bool.");

            return bool.Parse(configkey.Value);
        }

        public int GetIntKey(IDictionary<string, ConfigKey> collection, string keyName)
        {
            var configkey = collection[keyName];

            if (configkey.DataType != "int")
                throw new Exception($"'{keyName}' is a '{configkey.DataType ?? UNDEFINED_DATATYPE}', not an int.");

            return int.Parse(configkey.Value);
        }

        public int[] GetIntListKey(IDictionary<string, ConfigKey> collection, string keyName)
        {
            var configkey = collection[keyName];

            if (configkey.DataType != "intlist")
                throw new Exception($"'{keyName}' is a '{configkey.DataType ?? UNDEFINED_DATATYPE}', not an intlist.");

            var buffer = configkey.Value.Split(LIST_DELIMITER);
            var output = new int[buffer.Length];

            for (int i = 0; i < buffer.Length; i++)
                output[i] = int.Parse(buffer[i]);

            return output;
        }

        public double GetDoubleKey(IDictionary<string, ConfigKey> collection, string keyName)
        {
            var configkey = collection[keyName];

            if (configkey.DataType != "double")
                throw new Exception($"'{keyName}' is a '{configkey.DataType ?? UNDEFINED_DATATYPE}', not a double.");

            return double.Parse(configkey.Value);
        }

        public double[] GetDoubleListKey(IDictionary<string, ConfigKey> collection, string keyName)
        {
            var configkey = collection[keyName];

            if (configkey.DataType != "doublelist")
                throw new Exception($"'{keyName}' is a '{configkey.DataType ?? UNDEFINED_DATATYPE}', not a doublelist.");

            var buffer = configkey.Value.Split(LIST_DELIMITER);
            var output = new double[buffer.Length];

            for (int i = 0; i < buffer.Length; i++)
                output[i] = double.Parse(buffer[i]);

            return output;
        }

        public string GetStringKey(IDictionary<string, ConfigKey> collection, string keyName)
        {
            var configkey = collection[keyName];

            if (configkey.DataType != "string")
                throw new Exception($"'{keyName}' is a '{configkey.DataType ?? UNDEFINED_DATATYPE}', not a string.");

            return configkey.Value;
        }

        public string[] GetStringListKey(IDictionary<string, ConfigKey> collection, string keyName)
        {
            var configkey = collection[keyName];

            if (configkey.DataType != "stringlist")
                throw new Exception($"'{keyName}' is a '{configkey.DataType ?? UNDEFINED_DATATYPE}', not a stringlist.");

            return configkey.Value.Split(LIST_DELIMITER);
        }
    }
}

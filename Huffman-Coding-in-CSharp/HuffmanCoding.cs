using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Huffman_Coding_in_CSharp
{
    [Serializable]
    public class HuffmanCoding
    {
        private readonly Dictionary<char, List<bool>> code;

        private readonly List<bool> data;

        public HuffmanCoding(Dictionary<char, List<bool>> code, List<bool> data)
        {
            this.code = code;
            this.data = data;
        }

        public Dictionary<char, List<bool>> Code
        {
            get { return code; }
        }

        public List<bool> Data
        {
            get { return data; }
        }

        public void save(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, this);
            stream.Close();
        }

        public static HuffmanCoding read(string path)
        {
            Stream stream = File.Open(path, FileMode.Open);
            BinaryFormatter bf2 = new BinaryFormatter();
            var hc = (HuffmanCoding) bf2.Deserialize(stream);
            return hc;
            stream.Close();
        }
    }
}

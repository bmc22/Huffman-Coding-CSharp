using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Huffman_Coding_in_CSharp.tree;
using Windows.Storage.Streams;

namespace Huffman_Coding_in_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var inputPath = "etc/text.txt";
            var outputPath = "etc/code.hc";


            try
            {
                var input = File.ReadAllText(inputPath);
                var hc = Huffman.encode(input);


                //save the data and code into a file
                hc.save(outputPath);

                //read the HuffmanCoding object to decode the text
                var hc2 = HuffmanCoding.read(outputPath);

                //decode the text and compare it to the original input 
                Console.WriteLine((Huffman.decode(hc2.Code, hc2.Data)).Equals(input)); //True

            }

            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }

}

     


using Huffman_Coding_in_CSharp.tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Coding_in_CSharp
{
    public class Huffman
    {

        //create a frequency table from a string, and return a dictionary mapping each character in the input to its frequency 
        public static Dictionary<char, int> freqTable(string input)
        {
            if (input == null || input == "") return null;

            var map = new Dictionary<char, int>();

            foreach (char c in input.ToCharArray())
            {
                map[c] = (!map.ContainsKey(c)) ? 1 : map[c] + 1;
            }

            return map;
        }

        //create the Huffman tree from the frequency table
        public static Node treeFromFreqTable(Dictionary<char, int> freqTable)
        {
            if (freqTable == null) return null;

            var pqueue = new PQueue();

            foreach (KeyValuePair<char, int> mapping in freqTable)
            {
                pqueue.enqueue(new Leaf(mapping.Key, mapping.Value));
            }

            Node n1, n2;
            while (pqueue.size() > 1)
            {
                n1 = pqueue.dequeue();
                n2 = pqueue.dequeue();
                pqueue.enqueue(new Branch(n1.Freq + n2.Freq, n1, n2));
            }

            return pqueue.dequeue();
        }

        //build the code from the tree 
        public static Dictionary<char, List<bool>> buildCode(Node tree)
        {
            return (tree == null) ? null : tree.traverse(new List<bool>());
        }

        //the encode method
        public static HuffmanCoding encode(string input)
        {
            var table = freqTable(input);
            var tree = treeFromFreqTable(table);
            var code = buildCode(tree);
            var data = new List<bool>();

            foreach (char c in input.ToCharArray())
            {
                data.AddRange(code[c]);
            }

            return new HuffmanCoding(code, data);
        }

        //using the code (i.e., characters mapped to their paths) to create a tree which has the same structure as the Huffman tree created by the treeFromFreqTable method 
        public static Node treeFromCode(Dictionary<char, List<bool>> code)
        {
            Node root = new Branch(0, null, null);

            //Create a path for each character in the code
            foreach (char c in code.Keys)
            {
                Node currentNode = root;

                for (int i = 0; i < code[c].Count; i++)
                {
                    //After reaching the last value in the list, create a leaf node labelled with the character, and move on to the next character    
                    if (i == code[c].Count - 1)
                    {
                        if (code[c][i]) ((Branch)currentNode).Right = new Leaf(c, 0);
                        else { ((Branch)currentNode).Left = new Leaf(c, 0); }
                        continue;
                    }

                    //Before, setting the currentNode to be equal to its left or right child (based on the boolean value), its left or right child will be initialised if it was null
                    if (code[c][i] == false && (Node)((Branch)currentNode).Left == null)
                    {
                        ((Branch)currentNode).Left = new Branch(0, null, null);
                    }

                    if (code[c][i] == true && (Node)((Branch)currentNode).Right == null)
                    {
                        ((Branch)currentNode).Right = new Branch(0, null, null);
                    }

                    if (code[c][i] == false)
                        currentNode = ((Branch)currentNode).Left;

                    if (code[c][i] == true)
                        currentNode = ((Branch)currentNode).Right;

                }
            }

            return root; //Return the tree that has the structure of the Huffman tree

        }

        //the decode method
        public static string decode(Dictionary<char, List<bool>> code, List<bool> data)
        {
            string result = "";
            Node root = (Branch)treeFromCode(code);
            Node currentNode = root;

            foreach (bool b in data)
            {
                //Move left or right based on the boolean value       	 
                currentNode = (b == false) ? ((Branch)currentNode).Left : ((Branch)currentNode).Right;

                //Add a new character to the string when we reach a leaf node, and go back to the root of the tree
                if (currentNode is Leaf)
                {
                    result += ((Leaf)currentNode).Label;
                    currentNode = root;
                }
            }
            return result; //Return the original input

        }
    }
}

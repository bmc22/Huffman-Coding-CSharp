using Microsoft.VisualStudio.TestTools.UnitTesting;
using Huffman_Coding_in_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huffman_Coding_in_CSharp.tree;

namespace Huffman_Coding_in_CSharp.Tests
{
    [TestClass()]
    public class HuffmanTests
    {
        [TestMethod()]
        public void freqTableTest()
        {
            Assert.IsNull(Huffman.freqTable(null));
            Assert.IsNull(Huffman.freqTable(""));

            var table = Huffman.freqTable("Hello");

            Assert.AreEqual(2, table['l']);
            Assert.AreEqual(1, table['H']);
            Assert.AreEqual(1, table['o']);
            Assert.AreEqual(1, table['e']);

            Assert.AreEqual(4, table.Count);
        }

        [TestMethod()]
        public void treeFromFreqTableTest()
        {
            var table = Huffman.freqTable("B");
            var tree1 = Huffman.treeFromFreqTable(table);

            Assert.AreEqual(1, tree1.Freq);
            Assert.IsTrue(tree1 is Leaf);

            var tree2 = Huffman.treeFromFreqTable(Huffman.freqTable("Hey, Hello, Hi!"));
            Assert.AreEqual(15, tree2.Freq);

            Assert.IsTrue(((Branch) tree2).Left.Freq < ((Branch)tree2).Right.Freq);
            
        }

        [TestMethod()]
        public void buildCodeTest()
        {
            string input = "We are testing the buildcode method";
            var code = Huffman.buildCode(Huffman.treeFromFreqTable(Huffman.freqTable(input)));
             
            foreach (char c in input.ToCharArray())
            {
                Assert.IsTrue(code.ContainsKey(c));
            }
        }

        [TestMethod()]
        public void encodeAndDecodeTest()
        {
            string input = "&, *, %, !!!!!, aaaaa .";
            
            var hc = Huffman.encode(input);

            Assert.AreEqual(input, Huffman.decode(hc.Code, hc.Data));
        }

        
    }
}
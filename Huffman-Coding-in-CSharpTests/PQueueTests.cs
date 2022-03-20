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
    public class PQueueTests
    {
        [TestMethod()]
        public void PQueueTest()
        {
            var pq = new PQueue();

            Assert.AreEqual(0, pq.size());

            pq.enqueue(new Leaf('a', 10));

            pq.enqueue(new Leaf('b', 1));

            pq.enqueue(new Leaf('c', -4));

            pq.enqueue(new Leaf('d', -10));

            Assert.AreEqual(-10, pq.dequeue().Freq);

            Assert.AreEqual(-4, pq.dequeue().Freq);

            Assert.AreEqual(1, pq.dequeue().Freq);

            Assert.AreEqual(10, pq.dequeue().Freq);

            Assert.AreEqual(0, pq.size());
        }
    }
}
using Huffman_Coding_in_CSharp.tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Coding_in_CSharp
{
    public class PQueue
    {
        private List<Node> queue;

        public PQueue()
        {
            queue = new List<Node>();
        }

        public void enqueue(Node newNode)
        {
            //If the queue is empty or the node n must be inserted in the last position, add it to the queue and return
            if (size() == 0 || queue[size() - 1].Freq <= newNode.Freq)
            {
                queue.Add(newNode);
                return;
            }

            //If iteration is needed to insert the node n, we need to add a null node, which will be overwritten, to the end of the queue, to allow a space for n to be inserted
            queue.Add(null);

            //All the nodes that have a greater frequency than n will be moved by one postion to the right in order to leave a location to insert n
            int i = size() - 1;
            while (i > 0 && queue[i - 1].Freq > newNode.Freq)
            {
                queue[i]=  queue[i - 1];
                i--;
            }

            queue[i] = newNode;

        }
        public Node dequeue()
        {
            if (size() == 0)
                return null;
            
            var item= queue[0];
            queue.RemoveAt(0);
            return item;
        }
        public int size()
        {
            return queue.Count;
        }
    }
}

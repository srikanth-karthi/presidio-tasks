using Day14_leetcode.Lettcode_Problems;

namespace Day14_leetcode
{
    internal class Program
    {
        static void Main(string[] args)
        {


            LinkedList list = new LinkedList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);


            list.head.next.next.next.next.next = list.head.next.next;

            Console.WriteLine("Has cycle: " + list.HasCycle());

            Binarytree binarytree = new Binarytree();

            binarytree.Addnode(5);
            binarytree.Addnode(3);
            binarytree.Addnode(7);
            binarytree.Addnode(2);
            binarytree.Addnode(4);
            binarytree.Addnode(6);
            binarytree.Addnode(8);

            int minDepth = binarytree.MinDepth(binarytree.root);
            Console.WriteLine("Minimum Depth: " + minDepth);


            Excellsheet excellsheet = new Excellsheet();
            Console.WriteLine(excellsheet.ConvertToTitle(26));
            Console.WriteLine(excellsheet.ConvertToTitle(1));
            Console.WriteLine(excellsheet.ConvertToTitle(701));





        }
    }
}

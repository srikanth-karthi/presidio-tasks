namespace Delegate
{
    internal class Program
    {

        public delegate int calc(int x, int y);

        public int  Claculate(calc calc,int x,int y)
        {
            return calc(x, y);
        }
        public int Sum(int n1,int n2)
        {
            return n1+n2;
        }
        static void Main(string[] args)
        {

            int a = new Program().Claculate(new Program().Sum,4,5);
        }
    }
}

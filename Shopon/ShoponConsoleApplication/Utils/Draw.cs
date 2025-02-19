namespace ShoponConsoleApplication.Utils
{
    public static class Draw
    {
        public static void DrawLine(string v1, int v2)
        {
            for (int i = 0; i < v2; i++)
            {
                Console.Write(v1);
            }
            Console.WriteLine();
        }
    }
}

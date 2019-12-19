using System;

namespace HTMLPreCompile
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.IO.Directory.CreateDirectory("./A/B/C");
            BF_FileReader_Checker fc = new BF_FileReader_Checker();
            if(args.Length>=1)
            {
                fc.m_OutPath = args[0];

            }
            else
            {
                fc.m_OutPath = ".\\DEWD_OUTPUT";
            }
            fc.ListFiles();
            //Console.WriteLine("Hello World!");
        }
    }
}

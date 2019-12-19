using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLPreCompile
{
    class BF_FileReader_Checker
    {
        public String m_CurPath;
        public String m_OutPath="";
        public string ListFiles()
        {
          String path=  System.IO.Directory.GetCurrentDirectory();
            m_CurPath = path;
            //m_OutPath = ".\\OUTPUT";
            m_OutPath = System.IO.Path.GetFullPath(m_OutPath); 
            var files=System.IO.Directory.EnumerateFiles(path);
            ProcDirectory(path);

            return "";
        }
        public void ProcDirectory(String dir)
        {
            var files = System.IO.Directory.EnumerateFiles(dir);
            var dirs = System.IO.Directory.EnumerateDirectories(dir);
            foreach (var f in files)
            {
                if(f.EndsWith(".html"))
                {
                    String file_content=CheckFile(dir, f);
                    String includedFilePath = f.Replace(m_CurPath,m_OutPath);
                    String includedFileDirectory = includedFilePath.Replace(System.IO.Path.GetFileName(includedFilePath), "");
                    String includedFileNamme = System.IO.Path.GetFileName(includedFilePath);
                    if(!System.IO.Directory.Exists(includedFileDirectory))
                    {
                        System.IO.Directory.CreateDirectory(includedFileDirectory);
                    }
                    System.IO.File.WriteAllText(includedFilePath, file_content);
                    //Console.WriteLine(file_content);

                }

            }
            
            foreach (var d in dirs)
            {
                if(!m_OutPath.Equals(System.IO.Path.GetFullPath(d)))
                {
                ProcDirectory(d);
                }
            }



        }
        public String CheckFile(String dir,String filename)
        {
            var f = System.IO.File.ReadAllText(System.IO.Path.Combine(dir,filename));
            String f_str = f;
            var res=System.Text.RegularExpressions.Regex.Matches(f_str, "#(\\s)*?include(\\s)*?<(\\w|\\W)*?>");
            for (int i=0;i<res.Count;i++)
            {
                var includedfilename = System.Text.RegularExpressions.Regex.Replace(res[i].Value, "(#(\\s)*?include(\\s)*?<(\\s)*)|(>)", "");
                String includedFilePath=System.IO.Path.Combine(dir, includedfilename);
                includedFilePath = System.IO.Path.GetFullPath(includedFilePath);
                String includedFileDirectory= includedFilePath.Replace(System.IO.Path.GetFileName(includedFilePath),"");
                String includedFileNamme = System.IO.Path.GetFileName(includedFilePath);
                Console.WriteLine(includedFileDirectory);
                Console.WriteLine(includedFileNamme);

                String Inc_content = "";
                try
                {
                    Inc_content = CheckFile(includedFileDirectory, includedFileNamme);
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                f_str = f_str.Replace(res[i].Value, Inc_content);
            }

            return f_str;
        }
    }
}

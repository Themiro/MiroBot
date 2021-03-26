using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace _23012021Rewrite.Modules
{
    public static class TXTFileIO
    {
        public static void Save(string Path, string Data)
        {
            File.Delete(Path);
            StreamWriter SR = new StreamWriter(Path);
            SR.Write(Data);
            SR.Close();
        }
        public static void Save(string Path, List<string> Data)
        {
            File.Delete(Path);
            StreamWriter SR = new StreamWriter(Path);
            foreach (string item in Data)
            {
                SR.WriteLine(item);
            }
            SR.Close();
        }
        public static void Save(string Path, string[] Data)
        {
            File.Delete(Path);
            StreamWriter SR = new StreamWriter(Path);
            foreach (string item in Data)
            {
                SR.WriteLine(item);
            }
            SR.Close();
        }
        public static string Open(string Path)
        {
            StreamReader SR = new StreamReader(Path);
            string returnstring = "";
            returnstring = SR.ReadToEnd();
            SR.Close();
            return returnstring;
        }
        public static string[] ArrayOpen(string Path)
        {
            string[] returnarray;
            StreamReader SR = new StreamReader(Path);
            string returnstring = "";
            returnstring = SR.ReadToEnd();
            returnarray = returnstring.Split('\n');
            SR.Close();
            return returnarray;
        }
        public static List<string> ListOpen(string Path)
        {
            List<string> returnarray;
            StreamReader SR = new StreamReader(Path);
            string returnstring = "";
            returnstring = SR.ReadToEnd();
            returnarray = new List<string>();
            foreach (string item in returnstring.Split('\n'))
            {
                returnarray.Add(item);
            }
            SR.Close();
            return returnarray;
        }
    }
}

using System;
using System.Runtime.InteropServices;
using System.IO;

namespace link
{
    public class symlink
    {
        [DllImport("kernel32.dll")]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);
      
        static void Main(string[] args)
        {
            string link = @"C:\Users\joshu\GitHub\mine\symlinker\.vscode\a.txt";
            string target = @"C:\Users\joshu\GitHub\mine\symlinker\.vscode\b.txt";

            if (Directory.Exists(link) || File.Exists(link)) {
                CreateSymbolicLink(target, link, 0);

                if (Directory.Exists(target) || File.Exists(target)) {
                    Console.WriteLine("SYMLINK created for {0} <------> {1}", link, target);
                } else {
                    Console.WriteLine("Symlink failed to be created.");
                }
            } else {
                Console.WriteLine("{0} does not exist.", link);
            }
        }
    }
}

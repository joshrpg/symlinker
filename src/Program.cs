using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace link
{
    public class symlink
    {
        [DllImport("kernel32.dll")]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        public static async Task<int> Main(params string[] args)
        {
            int flag;

            if (args.Length == 0) {
                //showHelp();
                return 1;
            } 
            
            RootCommand rootCommand = new RootCommand (
                description: "This is to give symlinking an actual exe rather than trying to use the embedded mklink command embedded into Windows CMD."
                //, treatUnmatchedTokensAsErrors: true
            ){
                new Option(
                    aliases: new [] {"--verbose", "-v"}
                    , description: "Shows debug information."
                ),
                new Option(
                    aliases: new [] {"--force", "-f"}
                    , description: "Forces symlinker to override an existing link, this will also override an existing file or folder. I'm too lazy to check if it is actaully a link. ^_^"
                ),
                new Option(
                    aliases: new string[] {"--link", "-l"}
                    , description: "Specifies the new symbolic link name."
                ) { Argument = new Argument<string>(), IsRequired = true },
                new Option(
                    aliases: new string[] {"--target", "-t"}
                    , description: "Specifies the path (relative or absolute) that the new link refers to."
                ) { Argument = new Argument<string>(), IsRequired = true },
            };

            rootCommand.TreatUnmatchedTokensAsErrors = true;

            rootCommand.Handler = CommandHandler.Create(
                (bool verbose, bool force, string target, string link) => {
                    if (target != null && link != null) {
                        if (File.Exists(target) || Directory.Exists(target)) {
                            if (File.Exists(target))
                                flag = 0;
                            else
                                flag = 1;

                            if (verbose) {
                                Console.WriteLine("Link: {0}", link);
                                Console.WriteLine("Target: {0}", target);
                                
                                if (flag == 0)
                                    Console.WriteLine("Target is a file.");
                                else
                                    Console.WriteLine("Target is a directory.");
                            }
                            
                            if (File.Exists(link) || Directory.Exists(link)) {
                                if (force) {
                                    if (flag == 0)
                                        File.Delete(link);
                                    else
                                        Directory.Delete(link);
                                    
                                    if (verbose) {
                                        Console.WriteLine("Deleting {0}", link);
                                    }
                                } else {
                                    Console.WriteLine("The link is already exists, use --force to over ride.");
                                }
                            }

                            CreateSymbolicLink(link, target, flag);
                        } else {
                            Console.WriteLine("{0} does not exist.", target);
                        }
                    } else {
                        // I'll find a way to call the --help command later.
                        Console.WriteLine("Both --target and --link are required.");
                    }
                }
            );

            await rootCommand.InvokeAsync(args);

            return 0;
        }
    }
}

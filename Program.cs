using System;
using System.IO;

namespace FileRename
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Numbering sequence:");
            Console.WriteLine("1. Odd (1,3,5 ... 99)");
            Console.WriteLine("2. Even (2,4,6 ... 100)");
            Console.Write("Your option: ");
            var sequnce = Console.ReadLine();
            var path = readFilePath();
            if (sequnce == "1")
            {
                RenameFiles(path, Sequence.Odd);
            }
            else
            {
                RenameFiles(path, Sequence.Even);
            }
        }

        private static string readFilePath()
        {
            Console.Write("Enter Folder path: ");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                return readFilePath();
            }
            return path;
        }

        private static void RenameFiles(string folderPath, Sequence sequence)
        {
            var fileList = Directory.GetFiles(folderPath);
            var index = sequence == Sequence.Odd ? 1 : 2;
            foreach (var file in fileList)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Extension == "exe")
                {
                    continue;
                }
                Console.WriteLine($"Renaming: {fileInfo.Name}");
                var newName = file.Replace(fileInfo.Name, "");
                var finalIndex = "";
                if (index < 10)
                {
                    finalIndex = $"00{index}";
                }
                else if (index >= 10 && index < 100)
                {
                    finalIndex = $"0{index}";
                }

                var newPath = Path.Join(newName, $"{index}{fileInfo.Extension}");
                if (File.Exists(newPath))
                {
                    continue;
                }

                File.Move(file, newPath);
                index += 2;
            }
        }
    }
}

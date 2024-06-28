
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string directoryPath = @"C:\YourDirectoryPath";
        List<string> txtFiles = new List<string>();
        GetTxtFiles(directoryPath, txtFiles);

        foreach (var file in txtFiles)
        {
            AppendTextToFile(file, "ASPEKT");
        }
    }

    static void GetTxtFiles(string directoryPath, List<string> txtFiles)
    {
        string[] files = Directory.GetFiles(directoryPath, "*.txt");
        txtFiles.AddRange(files);

        string[] subdirectories = Directory.GetDirectories(directoryPath);
        foreach (string subdirectory in subdirectories)
        {
            GetTxtFiles(subdirectory, txtFiles); // Bug 1 : directoryPath need to be replace with subdirectory for closing infinity loop
        }
    }

    // Bug 2: Disposing and closing writer missing
    static void AppendTextToFile(string filePath, string textToAppend)
    {
        using FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        using StreamWriter writer = new StreamWriter(fileStream);
        writer.WriteLine(textToAppend);

        writer.Close();
        fileStream.Close();
        writer.Dispose();
        fileStream.Dispose();
    }
}
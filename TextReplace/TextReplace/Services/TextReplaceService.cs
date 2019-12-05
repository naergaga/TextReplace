using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextReplace.Services
{
    public class TextReplaceService
    {
        const int bufferSize = 1024 * 1014;


        public void Replace(string path, string oldStr, string newStr)
        {
            var outPath = path.Replace(".txt", ".out.txt");
            Replace(path, outPath, oldStr, newStr);
            File.Delete(path);
            File.Move(outPath, path);
        }
        public void Replace(string path, string outPath, string oldStr, string newStr)
        {
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            if (reader.CurrentEncoding == Encoding.UTF8)
            {
                var charArray = new char[1024];
                reader.Read(charArray, 0, charArray.Length);
                var buffer1 = Encoding.UTF8.GetBytes(charArray);
                var buffer2 = new byte[buffer1.Length];
                stream.Position = 0;
                stream.Read(buffer2, 0, buffer2.Length);
                var same = true;
                for (int i = 0; i < buffer1.Length; i++)
                {
                    if (buffer1[i] != buffer2[i])
                    {
                        same = false;
                        break;
                    }
                }
                if (!same)
                {
                    stream.Position = 0;
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    reader = new StreamReader(stream, Encoding.GetEncoding("GBK"));
                }
            }
            var writer = new StreamWriter(outPath, false, reader.CurrentEncoding);

            char[] bufferText = new char[bufferSize];
            int size;
            while ((size = reader.Read(bufferText, 0, bufferSize)) > 0)
            {
                var str = new String(bufferText, 0, size);
                str = str.Replace(oldStr, newStr);
                writer.Write(str);
            }

            reader.Close();
            writer.Close();
        }
    }
}

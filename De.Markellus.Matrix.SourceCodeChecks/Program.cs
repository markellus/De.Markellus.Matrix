// <copyright file=Program>Copyright (c) 2018 Marcel Bulla</copyright>
// <author>Marcel Bulla</author>
// <date> 7/31/2018 6:25:03 PM</date>
// ****************************************************************************************
// Copyright (C) 2018 Marcel Bulla
// 
// Permission to use, copy, modify, and/or distribute this software for any purpose with or
// without fee is hereby granted, provided that the above copyright notice and this
// permission notice appear in all copies.
// 
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO
// THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS. IN NO
// EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL
// DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER
// IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN
// CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
// ****************************************************************************************
using System;
using System.Collections.Generic;
using System.IO;

namespace De.Markellus.Matrix.SourceCodeChecks
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strFiles = Directory.GetFiles("../../../", "*.cs", SearchOption.AllDirectories);
            string strHeader  = File.ReadAllText("../../../HEADER");

            foreach (string strFile in strFiles)
            {
                string strFileTmp = Path.GetFullPath(strFile) + ".tmp";

                using (StreamReader reader = new StreamReader(strFile))
                {
                    using (StreamWriter writer = new StreamWriter(strFileTmp))
                    {
                        writer.WriteLine(@"// <copyright file=" + Path.GetFileNameWithoutExtension(strFile) +
                                         @">Copyright (c) 2018 Marcel Bulla</copyright>
// <author>Marcel Bulla</author>
// <date> " + DateTime.Now + @"</date>");
                        writer.WriteLine(strHeader);

                        string strLine   = string.Empty;
                        bool bCommentEnd = false;
                        while ((strLine = reader.ReadLine()) != null)
                        {
                            if (!strLine.StartsWith("// ") || bCommentEnd)
                            {
                                bCommentEnd = true;
                                writer.WriteLine(strLine);
                            }
                        }
                    }
                }

                File.Delete(strFile);
                File.Move(strFileTmp, strFile);
            }
        }

    }
}

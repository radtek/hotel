using System;
using System.IO;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Common
{
    public class ZipClass
    {
        public string cutStr = "";

        public void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel, int BlockSize)
        {
            //如果文件没有找到则报错。
            if (!System.IO.File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("The specified file " + FileToZip + " could not be found. Zipping aborderd");
            }

            System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry("ZippedFile");
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);
            byte[] buffer = new byte[BlockSize];
            System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
            ZipStream.Write(buffer, 0, size);
            try
            {
                while (size < StreamToZip.Length)
                {
                    int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            ZipStream.Finish();
            ZipStream.Close();
            StreamToZip.Close();
        }

        /// <summary>
        /// Get all DirectoryInfo
        /// </summary>
        /// <param name="di"></param>
        /// <param name="s"></param>
        /// <param name="crc"></param>
        private void direct(DirectoryInfo di, ref ZipOutputStream s, Crc32 crc)
        {
            //DirectoryInfo di = new DirectoryInfo(filenames);
            DirectoryInfo[] dirs = di.GetDirectories("*");

            //遍历目录下面的所有的子目录
            foreach (DirectoryInfo dirNext in dirs)
            {
                //将该目录下的所有文件添加到 ZipOutputStream s 压缩流里面
                FileInfo[] a = dirNext.GetFiles();
                this.writeStream(ref s, a, crc);

                //递归调用直到把所有的目录遍历完成
                direct(dirNext, ref s, crc);
            }
        }

        private void writeStream(ref ZipOutputStream s, FileInfo[] a, Crc32 crc)
        {
            foreach (FileInfo fi in a)
            {
                FileStream fs = fi.OpenRead();

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                string file = fi.FullName;
                file = file.Replace(cutStr, "");

                ZipEntry entry = new ZipEntry(file);

                entry.DateTime = DateTime.Now;

                // set Size and the crc, because the information
                // about the size and crc should be stored in the header
                // if it is not set it is automatically written in the footer.
                // (in this case size == crc == -1 in the header)
                // Some ZIP programs have problems with zip files that don't store
                // the size and crc in the header.
                entry.Size = fs.Length;
                fs.Close();

                crc.Reset();
                crc.Update(buffer);

                entry.Crc = crc.Value;

                s.PutNextEntry(entry);

                s.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// 压缩指定目录下指定文件(包括子目录下的文件)
        /// </summary>
        /// <param name="zippath">args[0]为你要压缩的目录所在的路径 
        /// 例如：D:\\temp\\   (注意temp 后面加 \\ 但是你写程序的时候怎么修改都可以)</param>
        /// <param name="zipfilename">args[1]为压缩后的文件名及其路径
        /// 例如：D:\\temp.zip</param>
        /// <param name="fileFilter">文件过滤, 例如*.xml,这样只压缩.xml文件.</param>
        /// <returns></returns>
        public bool ZipFileMain(string zippath, string zipfilename, string fileFilter)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream s = new ZipOutputStream(File.Create(zipfilename));
            try
            {
                s.SetLevel(6); // 0 - store only to 9 - means best compression

                DirectoryInfo di = new DirectoryInfo(zippath);

                FileInfo[] a = di.GetFiles(fileFilter);

                cutStr = zippath.Trim();
                //压缩这个目录下的所有文件
                writeStream(ref s, a, crc);
                //压缩这个目录下子目录及其文件
                direct(di, ref s, crc);
            }
            catch
            {
                return false;
            }
            finally
            {
                s.Finish();
                s.Close();
            }
            return true;
        }


        /// <summary>
        /// 压缩身份证图片
        /// </summary>
        /// <param name="basePath">基础目录</param>
        /// <param name="filePath">文件列表</param>
        /// <returns></returns>
        public static byte[] Zip(string basePath, List<string> filePath)
        {
            byte[] b = new byte[1024 * 10];
            using (MemoryStream outmsStrm = new MemoryStream())
            {
                using (ZipOutputStream zipStrm = new ZipOutputStream(outmsStrm))
                {
                    int count = filePath.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (File.Exists(basePath + filePath[i]))
                        {
                            ZipEntry z = new ZipEntry(filePath[i]);
                            zipStrm.PutNextEntry(z);
                            FileStream f = File.OpenRead(basePath + filePath[i]);
                            int buffersize;
                            while ((buffersize = f.Read(b, 0, b.Length)) > 0)
                            {
                                zipStrm.Write(b, 0, buffersize);
                            }
                            f.Close();
                        }
                        else
                        {
                            ILog log = log4net.LogManager.GetLogger("Zip");
                            log.Error("图片路径：" + basePath + filePath[i] + "不存在。");
                        }
                    }
                }
                return outmsStrm.ToArray();
            }
        }

        /// <summary>
        /// 压缩所有图片+数据表格
        /// </summary>
        /// <param name="basePath">基础目录</param>
        /// <param name="filePath">文件列表</param>
        /// <param name="xlsBytes">数据表格字节数组</param>
        /// <returns></returns>
        public static byte[] Zip(string basePath, List<string> filePath,byte[] xlsBytes)
        {
            byte[] b = new byte[1024 * 10];
            using (MemoryStream outmsStrm = new MemoryStream())
            {
                using (ZipOutputStream zipStrm = new ZipOutputStream(outmsStrm))
                {
                    ZipEntry ze = new ZipEntry("upload/订单信息.xls");
                    zipStrm.PutNextEntry(ze);
                    zipStrm.Write(xlsBytes, 0, xlsBytes.Length);
                    int count = filePath.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (File.Exists(basePath + filePath[i]))
                        {
                            ZipEntry z = new ZipEntry(filePath[i]);
                            zipStrm.PutNextEntry(z);
                            FileStream f = File.OpenRead(basePath + filePath[i]);
                            int buffersize;
                            while ((buffersize = f.Read(b, 0, b.Length)) > 0)
                            {
                                zipStrm.Write(b, 0, buffersize);
                            }
                            f.Close();
                        }
                        else
                        {
                            ILog log = log4net.LogManager.GetLogger("Zip");
                            log.Error("图片路径：" + basePath + filePath[i] + "不存在。");
                        }
                    }
                }
                return outmsStrm.ToArray();
            }
        }
    }
}
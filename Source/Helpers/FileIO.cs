using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CableGuardian
{
    static class FileIO
    {
        public static void CreateNumberedBackup(string filePath, int storedBackupCount)
        {
            if (File.Exists(filePath))
            {
                FileInfo newFileInfo = new FileInfo(filePath);
                string dir = newFileInfo.DirectoryName;
                string newFileWithoutExtension = Path.GetFileNameWithoutExtension(newFileInfo.Name);

                // list the files with the same name (without extension)
                // and rename the earlier backups  .002--> .003, .001 -->  .002 jne.
                string[] filesWithSameBaseName = Directory.GetFiles(dir);
                filesWithSameBaseName = (from string fileName in filesWithSameBaseName
                                         where fileName.Contains(dir + "\\" + newFileWithoutExtension) && fileName != newFileInfo.FullName
                                         orderby fileName descending 
                                         select fileName).ToArray();
                foreach (string fileName in filesWithSameBaseName)
                {
                    FileInfo i = new FileInfo(fileName);

                    string fileNameWoExt = Path.GetFileNameWithoutExtension(i.Name);
                    int extNumber;
                    string ext = i.Extension.Replace(".", "");
                    if (int.TryParse(ext, out extNumber))
                    {
                        extNumber++;
                        if (extNumber <= (storedBackupCount)) 
                        {
                            string newExt = String.Format("{0:000}", extNumber);
                            File.Copy(i.FullName, dir + "\\" + fileNameWoExt + "." + newExt, true);
                        }
                        else
                        {
                            File.Delete(i.FullName);
                        }
                    }
                }
                if (storedBackupCount > 0)
                {
                    File.Copy(filePath, dir + "\\" + newFileWithoutExtension + ".001", true);
                }
            }
        }

        public static string GetLatestNumberedBackupFile(string filePath)
        {
            FileInfo newFileInfo = new FileInfo(filePath);
            string dir = newFileInfo.DirectoryName;
            string newFileWithoutExtension = Path.GetFileNameWithoutExtension(newFileInfo.Name);

            string[] filesWithSameBaseName = Directory.GetFiles(dir);
            filesWithSameBaseName = (from string fileName in filesWithSameBaseName
                                     where fileName.Contains(dir + "\\" + newFileWithoutExtension) && fileName != newFileInfo.FullName
                                     orderby fileName ascending
                                     select fileName).ToArray();

            if (filesWithSameBaseName?.Count() > 0)
            {
                return filesWithSameBaseName[0];
            }

            return null;
        }
    }
}

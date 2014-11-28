using System;
using System.IO;

namespace CleanCode
{
	public static class RefactorMethod
	{
	    private static void SaveTimeToFile(string fName)
	    {
            var fStream = new FileStream(fName, FileMode.OpenOrCreate);
            var t = BitConverter.GetBytes(DateTime.Now.Ticks);
            fStream.Write(t, 0, t.Length);
            fStream.Close();
	    }

	    private static void SaveDataToFile(string fName, byte[] dataBytes)
	    {
	        var fStream = new FileStream(fName, FileMode.OpenOrCreate);
	        fStream.Write(dataBytes, 0, dataBytes.Length);
	        fStream.Close();
	    }

        private static void SaveDataWithTimeAndBackup(string destFile, byte[] dataBytes)
        {
	        SaveDataToFile(destFile, dataBytes);
            //save again (for backup):
            SaveDataToFile(Path.ChangeExtension(destFile, "bkp"), dataBytes);
	        //save last-write time:
			SaveTimeToFile(destFile + ".time");
		}
	}
}
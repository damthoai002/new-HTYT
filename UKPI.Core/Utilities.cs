using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace UKPI.Core
{
    public class Utilities
    {
        public static Object ChangeType(Object value, Type type, string format)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }

            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType, format);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }

            if (type == typeof(DateTime) && !string.IsNullOrEmpty(value.ToString()))
            {
                if (string.IsNullOrEmpty(format))
                    return DateTime.Parse(value.ToString());
                DateTime date = DateTime.ParseExact(value.ToString(), format, null);
                return Convert.ChangeType(date, type);
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);

            return Convert.ChangeType(value, type);
        }

        public static void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            string folder = Path.GetDirectoryName(destinationFilePath);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            if (File.Exists(destinationFilePath))
            {
                TryRenameBackupFile(destinationFilePath);
            }
            File.Move(sourceFilePath, destinationFilePath);
        }

        public static string TryRenameBackupFile(string filePath)
        {
            int index = 0;
            string file = filePath;
            string folder = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            while (File.Exists(file))
            {
                string name = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".b" + index.ToString("00");
                file = Path.Combine(folder, name);
                index++;
                if (index > 100)
                    break;
            }

            if (File.Exists(file))
                return string.Empty;
            File.Move(filePath, file);
            return file;

        }
    }
}

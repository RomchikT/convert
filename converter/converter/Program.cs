using System;
using System.IO;
using System.Xml.Serialization;
namespace ConsoleTextEditor
{
    public class Program
    {
        public static string filePath;
        public static FileModel fileModel;
        public static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath).TrimStart('.');
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу:");
            filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                LoadFile();
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine(fileModel.Content);
            }
            else
            {
                fileModel = new FileModel();
            }

            Console.WriteLine("Текстовый редактор");
            Console.WriteLine("F1 - сохранить файл");
            Console.WriteLine("Esc - выход");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.F1)
                {
                    Console.WriteLine("Введите путь для сохранения файла:");
                    string savePath = Console.ReadLine();
                    SaveFile(savePath);
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    fileModel.Content += key.ToString();
                
                }
            }
        }

        private static void LoadFile()
        {
            string fileExtension = Path.GetExtension(filePath).TrimStart('.');
            string fileContent = File.ReadAllText(filePath);

            switch (fileExtension)
            {
                case "txt":
                    fileModel = new FileModel { Content = fileContent };
                    break;
                case "xml":
                    var serializer = new XmlSerializer(typeof(FileModel));
                    using (var reader = new StringReader(fileContent))
                    {
                        fileModel = (FileModel)serializer.Deserialize(reader);
                    }
                    break;
                case "js":
                    fileModel = new FileModel { Content = fileContent };
                    break;
                default:
                    Console.WriteLine("Неподдерживаемый формат файла");
                    break;
            }
        }

        public static void SaveFile(string savePath)
        {
            string fileExtension = GetFileExtension(savePath);

            switch (fileExtension)
            {
                case "txt":
                    File.WriteAllText(savePath, fileModel.Content);
                    break;
                case "xml":
                    var serializer = new XmlSerializer(typeof(FileModel));
                    using (var writer = new StreamWriter(savePath))
                    {
                        serializer.Serialize(writer, fileModel);
                    }
                    break;
                case "js":
                    File.WriteAllText(savePath, fileModel.Content);
                    break;
                default:
                    Console.WriteLine("Неподдерживаемый формат файла");
                    Environment.Exit(0);
                    break;
            }

            Console.WriteLine("Файл успешно сохранен");
        }
    }
}
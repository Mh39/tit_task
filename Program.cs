using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace tit_task
{
    class Program
    {
        public class person
        {
            string Name;
            int Age;
            string Birthday;
            public string Name_Proberty
            {
                get { return Name; }
                set { this.Name = value; }
            }
            public int Age_Proberty
            {
                get { return Age; }
                set { this.Age = value; }
            }
            public string BirthDay_Proberty
            {
                get { return Birthday; }
                set { this.Birthday = value; }
            }
            public person(string name, int age, string birthday)
            {
                this.Name = name;
                this.Age = age;
                this.Birthday = birthday;

            }
            public person() { }

        }
        static void Main(string[] args)
        {
            convertfile("C: /Users/Mohamed Hassan/OneDrive/Desktop/file1.txt", "C: /Users/Mohamed Hassan/OneDrive/Desktop/file2.json.txt");

            Console.ReadKey();
        }
        public static void convertfile(string in_file_path, string out_file_path)
        {
            if (!CheckFileExist(in_file_path))
                throw new Exception("File Not Found");
            if (!CheckCorrectFileName(in_file_path))
                throw new Exception("incorrect file name");
            var fileData = ReadFileLines(in_file_path);
            if (fileData.Length == 0)
                throw new Exception("File empty");
            if (!CheckFirstLine(fileData[0], fileData.Length))
                throw new Exception("invalid first line");
            for (int i = 1; i < fileData.Length; i++)
            {
                if (!IsLineValid(fileData[i]))
                    throw new Exception($"invalid line data {i}");
            }
            var persons = FillData(fileData);
            WriteDataToJsonFile(out_file_path, persons);
        }
        public static string WriteDataToJsonFile(string outPath, List<person> personsList)
        {
            var json = JsonConvert.SerializeObject(personsList);
            File.WriteAllText(outPath, json);
            //}
            public static List<person> FillData(string[] fileData)
        {
            var persons = new List<person>();
            for (int i = 1; i < fileData.Length; i++)
            {
                var data = fileData[i].Split(':');
                persons.Add(new person(data[0], Convert.ToInt32(data[1]), data[2]));
                //if (!CheackSecondLine())
                //    throw new Exception("incorrect age");

            }
            return persons;
        }


        /// <summary>
        /// check if file exist on hard disk
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns></returns>
        public static bool CheckFileExist(string filePath)
        {
            return File.Exists(filePath);

        }
        public static bool CheckCorrectFileName(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var fileNameToCompare = $"MK_{DateTime.Now:ddMMyyyy}.g";
            // var fileNameToCompare = "MK_"+DateTime.Now.GetDateTimeFormats(ddMMyyyy)+".g";

            return fileName == fileNameToCompare;
        }
        public static string[] ReadFileLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
        public static bool CheckFirstLine(string line, int count)
        {
            if (string.IsNullOrWhiteSpace(line))
                return false;
            ////first method
            //var data = line.Split('-');
            //if (data.Length != 2)
            //    return false;
            //if (data[0] != DateTime.Now.ToString("ddMMyyyy"))
            //    return false;
            //if (data[1] != count.ToString())
            //    return false;
            //return true;
            //second method
            var stringToCompare = $"{DateTime.Now:ddMMyyyy}-{count}";
            return stringToCompare == line;
        }
        public static bool IsLineValid(string line)
        {
            var data = line.Split(':');
            if (data.Length != 3)
                return false;
            if (!int.TryParse(data[1], out _))
                return false;
            //string nn = 
            int dt = Convert.ToInt32(Convert.ToDateTime(data[2]).Year);
            int date = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            return true;

        }
        public static bool CheackSecondLine(string line, DateTime birthday, string name, int age)
        {
            var data = line.Split(':');
            if (string.IsNullOrWhiteSpace(line))
                return false;

            if (data[2] != DateTime.Now.ToString("ddMMyyyy"))
                return false;
            if (age != DateTime.Now.Year - birthday.Year)
                //throw new Exception("incorrect age");
                return false;

            return true;
        }
    }
}

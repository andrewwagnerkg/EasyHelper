using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EasyHelper
{
    public static class RecordsManager
    {

        private static XmlSerializer ser = new XmlSerializer(typeof(List<Record>));

        public static List<Record> Records { get; set; }= new List<Record>();

        public static string Filename { get; private set; }= "db.xml";

        public static void Load()
        {
            Records.Clear();
            if (File.Exists(Filename))
            {
                using (FileStream fs = new FileStream(Filename, FileMode.OpenOrCreate))
                {
                    Records = (List<Record>)ser.Deserialize(fs);
                }
            }

        }

        public static void Save()
        {
            if (File.Exists(Filename))
            {
                File.Delete(Filename);
            }
            using (FileStream fs = new FileStream(Filename, FileMode.OpenOrCreate))
            {
                ser.Serialize(fs, Records);
            }
        }

        public static void Add(Record record)
        {
            Records.Add(record);
        }

        public static void Edit(Record record)
        {
            Records.RemoveAll(r => r.Id == record.Id);
            Records.Add(record);
        }

        public static void Remove(Record record)
        {
            Records.Remove(record);
        }
    }
}

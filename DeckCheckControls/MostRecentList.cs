using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DeckCheckControls
{
    [DataContract]
    public class MostRecentList
    {
        [DataMember(Name = "RecentItems")]
        private List<MostRecentItem> _items = new List<MostRecentItem>();

        [DataMember(Name = "MaxSize")]
        private int _maxSize = 5;

        public MostRecentList()
        {
            _maxSize = 5;
        }

        public int MaxSize
        {
            get
            {
                return _maxSize;
            }
            set
            {
                if (value < _maxSize && _items.Count > value)
                    _items.RemoveRange(value - 1, _items.Count - value);
                _maxSize = value;
            }
        }

        public MostRecentItem this[int k] => _items[k];

        public int Count => _items.Count;

        public void Add(MostRecentItem item)
        {
            int index = IndexOfByPath(item.Path);
            if (index != -1)
                _items.RemoveAt(index);
            _items.Insert(0, item);
            if (_items.Count > _maxSize)
                _items.RemoveAt(_maxSize);
        }

        private int IndexOfByPath(string path)
        {
            for (int k = 0; k < _items.Count; k++)
                if (_items[k].Path == path)
                    return k;
            return -1;
        }

        public static MostRecentList Read(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(MostRecentList));
            using (XmlReader reader = XmlReader.Create(stream))
                return (MostRecentList)serializer.ReadObject(reader);
        }


        public static MostRecentList Read(string filePath)
        {
            if (!File.Exists(filePath))
                return new MostRecentList();
            using (Stream stream = File.OpenRead(filePath))
                return Read(stream);
        }

        public static void Save(Stream stream, MostRecentList mostRecentList)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(MostRecentList));
            using (XmlWriter writer = XmlWriter.Create(stream))
                serializer.WriteObject(writer, mostRecentList);
        }

        public static void Save(string filePath, MostRecentList mostRecentList)
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                Save(stream, mostRecentList);
        }
    }
}

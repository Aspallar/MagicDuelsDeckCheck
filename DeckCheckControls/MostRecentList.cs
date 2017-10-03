using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Linq;

namespace DeckCheckControls
{
    [DataContract]
    public class MostRecentList
    {
        [DataMember(Name = "RecentItems")]
        private List<MostRecentItem> _items = new List<MostRecentItem>();

        [DataMember(Name = "MaxSize")]
        private int _maxSize;

        public MostRecentList()
        {
            _maxSize = 12;
        }

        public int MaxSize
        {
            get
            {
                return _maxSize;
            }
            set
            {
                if (value == 0)
                    _items.RemoveRange(0, _items.Count);
                else if (value < _maxSize && _items.Count > value)
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

        public MostRecentList Except(List<MostRecentItem> exceptItems)
        {
            MostRecentList list = new MostRecentList();
            list.MaxSize = MaxSize;
            list._items = _items.Where(x => !exceptItems.Any(y => y.Path == x.Path)).ToList();
            return list;
        }

        public static MostRecentList Read(string filePath)
        {
            return Serialization.Read<MostRecentList>(filePath);
        }

        public static void Save(string filePath, MostRecentList mostRecentList)
        {
            Serialization.Save(filePath, mostRecentList);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckCheckControls
{
    public class MostRecentList
    {
        private readonly List<MostRecentItem> _items = new List<MostRecentItem>();
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




    }
}

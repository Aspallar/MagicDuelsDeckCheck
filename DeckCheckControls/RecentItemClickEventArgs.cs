using System;

namespace DeckCheckControls
{
    public class RecentItemClickEventArgs : EventArgs
    {
        public MostRecentItem Item { get; private set; }

        public RecentItemClickEventArgs(MostRecentItem item)
        {
            Item = item;
        }
    }
}
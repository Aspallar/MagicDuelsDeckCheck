using System;

namespace DeckCheckControls
{
    public class RecentItemClickEventArgs : EventArgs
    {
        public string Path { get; private set; }

        public RecentItemClickEventArgs(string path)
        {
            Path = path;
        }
    }
}
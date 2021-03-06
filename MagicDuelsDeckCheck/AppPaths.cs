﻿using System;

namespace MagicDuelsDeckCheck
{
    internal static class AppPaths
    {
        public static readonly string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MagicDuelsDeckCheck\";
        public static readonly string RecentDecksFilePath = AppDataFolder + "RecentDecks.xml";
        public static readonly string FavouritessFilePath = AppDataFolder + "Favourites.xml";
        public static readonly string UserTemplatesFolder = AppDataFolder + "Templates";
    }
}

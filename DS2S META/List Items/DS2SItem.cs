﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DS2S_META
{
    public class DS2SItem : IComparable<DS2SItem>
    {
        public enum ItemType
        {
            Weapon = 0,
            Armor = 1,
            Item = 2,
            Ring = 3
        }
       
        private static Regex ItemEntryRx = new Regex(@"^\s*(?<id>\S+)\s+(?<name>.+)$");

        private bool ShowID;

        public string Name;
        public int ID;
        public int itemID => Type == ItemType.Armor ? ID - 10000000 : ID; // Interface fix for DS2SItem to normal itemID
        public ItemType Type;

        public static Dictionary<int, string> Items = new Dictionary<int, string>()
        {
            {3400000 ,"Fist"},
            {21001100 ,"Naked"},
            {21001101 ,"Naked"},
            {21001102 ,"Naked"},
            {21001103 ,"Naked"}
        };

        public DS2SItem(string config, int type, bool showID)
        {
            Match itemEntry = ItemEntryRx.Match(config);
            ID = Convert.ToInt32(itemEntry.Groups["id"].Value);
            Type = (ItemType)type;
            ShowID = showID;
            if (showID)
                Name = ID.ToString() + ": " + itemEntry.Groups["name"].Value;
            else
                Name = itemEntry.Groups["name"].Value;

            Items.Add(ID, itemEntry.Groups["name"].Value);
        }
        public override string ToString()
        {
            return Name;
        }
        public int CompareTo(DS2SItem? other)
        {
            if (ShowID)
                return ID.CompareTo(other?.ID);
            else
                return Name.CompareTo(other?.Name);
        }

        public bool NameContains(string txtfrag)
        {
            // Used for easier filtering
            return Name.ToLower().Contains(txtfrag.ToLower());
        }
    }
}

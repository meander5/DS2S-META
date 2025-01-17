﻿using DS2S_META.Randomizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DS2S_META.ViewModels
{
    public class ItemRestriction : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Fields/Properties:
        public string Name { get; set; }
        public List<int> ItemIDs { get; set; }
        public ITEMGROUP GroupType { get; set; }

        private RestrType _restrType = RestrType.Anywhere; // combo box
        public RestrType RestrType
        {
            get => _restrType;
            set
            {
                _restrType = value;
                OnPropertyChanged(nameof(RestrType));
                OnPropertyChanged(nameof(VisDistSettings));
            }
        }
        public Visibility VisDistSettings => RestrType == RestrType.Distance ? Visibility.Visible : Visibility.Collapsed;
        private int _distMin = 1;
        public int DistMin
        {
            get => _distMin;
            set
            {
                var limval = Math.Max(value, 1);
                _distMin = limval < DistMax ? limval : DistMax;
                OnPropertyChanged(nameof(DistMin));
            }
        }
        private int _distMax = 10;
        public int DistMax
        {
            get => _distMax;
            set
            {
                var limval = Math.Min(value, 20);
                _distMax = limval > DistMin ? limval : DistMin;
                OnPropertyChanged(nameof(DistMax));
            }
        }


        public static Dictionary<RestrType, string> TypeComboItems { get; set; } = new()
        {
            [RestrType.Anywhere] = "Anywhere",
            [RestrType.Vanilla] = "Vanilla",
            [RestrType.Distance] = "Distance"
        };

        // Constructors:
        public ItemRestriction()
        {
            // Necessary for deserialization
            ItemIDs = new();
            Name = string.Empty;
        }
        public ItemRestriction(string name, ITEMGROUP grp, RestrType restrType = RestrType.Anywhere)
        {
            Name = name;
            ItemIDs = new();
            GroupType = grp;
            RestrType = restrType;
        }
        public ItemRestriction(string name, ITEMID itemID, RestrType restrType = RestrType.Anywhere)
        {
            Name = name;
            ItemIDs = new List<int>() { (int)itemID };
            GroupType = ITEMGROUP.Specified;
            RestrType = restrType;
        }

    }
}

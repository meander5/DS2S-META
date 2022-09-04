﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Media;
using DS2S_META.Resources.Randomizer;

namespace DS2S_META
{
    /// <summary>
    /// Interaction logic for CovenantControl.xaml
    /// </summary>
    public partial class RandomizerControl : METAControl
    {
        private Color PURPLE = Color.FromArgb(0xFF, 0xB1, 0x59, 0xCC);
        private Color LIGHTRED = Color.FromArgb(0xFF, 0xDA, 0x4D, 0x4D);
        private Color LIGHTGREEN = Color.FromArgb(0xFF, 0x87, 0xCC, 0x59);

        Random RNG = new Random();

        private Dictionary<int, ItemLot> VanillaLots;
        internal RandoDicts RD = new RandoDicts();
        internal bool isRandomized = false;


        public RandomizerControl()
        {
            InitializeComponent();

            // TODO
            int seed = 1;
            RNG = new Random(seed);
        }



        //Handles the "Seeed..." label on the text box
        private void FixSeedVisibility()
        {
            if (txtSeed.Text == "")
                lblSeed.Visibility = Visibility.Visible;
            else
                lblSeed.Visibility = Visibility.Hidden;

        }

        private void txtSeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            FixSeedVisibility();
        }


        private void randomize()
        {
            // Need to get a list of the vanilla item lots C#.8 pleeeease :(
            if (VanillaLots==null)
                VanillaLots = Hook.GetVanillaLots();


            ItemSetBase CasInfo = new CasualItemSet();
            Dictionary<int, ItemLot> NewLots = new Dictionary<int, ItemLot>();

            // Write undefined paramIDs to file:
            //var test = VanillaLots.Where(lot => (!CasInfo.D.ContainsKey(lot.Key))).ToArray();
            //string[] missed_params = test.Select(kvp => $"{kvp.Key} (Offset {Hook.GetItemLotOffset(kvp.Key):X}) : {kvp.Value}").ToArray();
            //System.IO.File.WriteAllLines("missing_params.txt", missed_params);

            // Remove anything disregarded from settings:
            var BanKeyTypes = new List<PICKUPTYPE>()
            {
                PICKUPTYPE.NPC,
                PICKUPTYPE.VOLATILE,

                PICKUPTYPE.EXOTIC,
                PICKUPTYPE.COVENANT, // To split into cheap/annoying
                PICKUPTYPE.UNRESOLVED,
                PICKUPTYPE.REMOVED,
                PICKUPTYPE.NGPLUS,
            };
            var BanGeneralTypes = new List<PICKUPTYPE>()
            {
                PICKUPTYPE.EXOTIC,
                PICKUPTYPE.COVENANT, // To split into cheap/annoying
                PICKUPTYPE.UNRESOLVED,
                PICKUPTYPE.REMOVED,
                PICKUPTYPE.NGPLUS,
            };

            // Make into flat list of stuff:
            var flatlist = VanillaLots.SelectMany(kvp => kvp.Value.Lot).ToList();
            // Need to add shops here.

            var ShuffledLots = new Dictionary<int, ItemLot>();


            // List of any key related item:
            var allkeys = flatlist.Where(DI => Enum.IsDefined(typeof(KEYID), DI.ItemID)).ToList();
            var keysrem = new List<DropInfo>(allkeys); // clone

            // Go through and place the keys randomly:
            
            
            var test = allkeys.Where(DI => DI.ItemID == (int)KEYID.FRAGRANTBRANCH).Count();

            // Get list of places where keys can go:
            var validKeyPlaces = CasInfo.D.Where(kvp => IsValidKeyPickup(kvp, BanKeyTypes))
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var remKeyPlaces = new Dictionary<int, RandoInfo>(validKeyPlaces); // clone
            RD.ValidKeyPlaces = validKeyPlaces;
            RD.RemKeyPlaces = remKeyPlaces;

            var Nkeyrem = keysrem.Count();
            while (Nkeyrem > 0)
            {
                int keyid = RNG.Next(Nkeyrem);
                DropInfo item = keysrem[keyid]; // get key to place
                PlaceItem(item, RD);
                Nkeyrem--;
            }

            var testing = RD.ShuffledLots.Where(kvp => kvp.Value.NumDrops > 1).ToList();


            


            var debug = 1;
            //foreach (var kvp in VanillaLots)
            //{
            //    // unpack:
            //    int paramID = kvp.Key;
            //    ItemLot lot = kvp.Value;

            //    // At some point, should sort the lots to place them in a more sensible order...
            //    if (lot.NumDrops == 0)
            //        continue;

            //    List<DropInfo> dropList = new List<DropInfo>();
            //    for (int i = 0; i < lot.NumDrops; i++)
            //    {
            //        while (remainingitemindex)
            //        {
            //            int index = rng.Next(flatlist.Count());
            //            DropInfo randdrop = flatlist[index]; // get the item we want to place here

            //            // Ensure we don't softlock the game because of keys blocks
            //            if (CheckValidPlacement(paramID, randdrop))
            //            {
            //                dropList.Add(randdrop);
            //                flatlist.RemoveAt(index); // don't place twice
            //            }
            //            else
            //            {
            //                // TODO full softlock reset tests.
            //            }
            //        }
            //    }
            //}



            //// Make into new lots:
            //foreach (var kvp in VanillaLots)
            //{
            //    ItemLot IL = new ItemLot();
            //    for (int row = 0; row < kvp.Value.NumDrops; row++)
            //    {
            //        IL.AddDrop(flatlist[0]);
            //        flatlist.RemoveAt(0); // pop
            //    }
            //    ShuffledLots[kvp.Key] = IL; // add to new dictionary
            //}


            //Hook.WriteAllLots(ShuffledLots);
            //isRandomized = true;
        }
        private void PlaceItem(DropInfo item, RandoDicts RD)
        {
            bool keyPlaced = false;
            RD.SoftlockSpots = new Dictionary<int, RandoInfo>(); // resets each time a key is placed
            while (!keyPlaced)
            {
                int Nsr = RD.RemKeyPlaces.Count();
                if (Nsr == 0)
                    break; // hits exception below

                // Choose random place for key:
                int pindex = RNG.Next(Nsr);
                var place = RD.RemKeyPlaces.ElementAt(pindex);

                // Check viability:
                HandleSoftlockCheck(out bool isSoftLocked, place, RD);
                if (isSoftLocked)
                    continue; // handled inside function

                // Accept solution:
                RD.PlacedSoFar.Add(item.ItemID);
                AddToLots(RD.ShuffledLots, place, item);
                if (IsPlaceSaturated(RD.ShuffledLots, place.Key))
                    RD.RemKeyPlaces.Remove(place.Key);

                // Prepare for next item:
                keyPlaced = true;
                foreach (var kvp in RD.SoftlockSpots)
                    RD.RemKeyPlaces.Add(kvp.Key, kvp.Value);
            }


        }
        private void HandleSoftlockCheck(out bool isSoftLocked, KeyValuePair<int, RandoInfo> place, RandoDicts RD)
        {
            isSoftLocked = CheckIsSoftlockPlacement(place, RD.PlacedSoFar);
            if (!isSoftLocked)
                return;

            // This place is not valid for the current item.
            // Store it in temp array and restore later for next item checks.
            // This is for performance to avoid excessively drawing a bad place.
            RD.SoftlockSpots[place.Key] = place.Value;
            RD.RemKeyPlaces.Remove(place.Key);
        }
        private bool CheckIsSoftlockPlacement(KeyValuePair<int, RandoInfo> place, List<int> placedSoFar)
        {
            // Can only place item in slot if the keyconditions have been met
            var keysets = place.Value.KeySet;

            // Try each different option for key requirements
            foreach (var keyset in keysets)
            {
                if (keyset.Keys.All(kid => IsPlaced(kid, placedSoFar)))
                    return true; // all required keys are placed for at least one Keyset
            }
            return false;
        }
        private bool IsPlaced(KEYID kid, List<int> placedSoFar)
        {
            // Function to handle different checks depending on KeyTypes I guess:
            switch (kid)
            {
                case KEYID.BELFRYLUNA:
                    // Branch && Pharros Lockstone x2
                    return condLuna();

                case KEYID.SINNERSRISE:
                    // Branch || Antiquated
                    return condSinner();
                    
                case KEYID.DRANGLEIC:
                    // Branch && Rotunda && Sinner's Rise
                    return condDrangleic();

                case KEYID.AMANA:
                    // Drangleic && King's passage
                    return condAmana();

                case KEYID.ALDIASKEEP:
                    // Branch && King's Ring
                    return condAldias();

                case KEYID.MEMORYJEIGH:
                    // King's Ring && Ashen Mist
                    return condJeigh();

                case KEYID.GANKSQUAD:
                    // DLC1 && Eternal Sanctum
                    return condGankSquad();

                case KEYID.PUZZLINGSWORD:
                    // DLC1 (TODO Bow/Arrow as keys)
                    return condDLC1();

                case KEYID.ELANA:
                    // DLC1 && Dragon Stone
                    return condElana();

                case KEYID.FUME:
                    // DLC2 && Scorching Sceptor
                    return condFume();

                case KEYID.BLUESMELTER:
                    // DLC2 && Tower Key
                    return condBlueSmelter();

                case KEYID.ALONNE:
                    // DLC2 && Tower Key && Scorching Scepter && Ashen Mist
                    return condAlonne();

                case KEYID.DLC3:
                    // DLC3key && Drangleic
                    return condDLC3();

                case KEYID.FRIGIDOUTSKIRTS:
                    // DLC3 && Garrison Ward Key
                    return condFrigid();

                case KEYID.CREDITS:
                    // Drangleic & Memory of Jeigh
                    return condCredits();

                case KEYID.VENDRICK:
                    // Amana + SoaG x3
                    return condVendrick();

                case KEYID.BRANCH:
                    // Three branches available
                    return condBranch();

                case KEYID.TENBRANCHLOCK:
                    // Ten branches available
                    return condTenBranch();

                case KEYID.NADALIA:
                    // DLC2 && Scepter && Tower Key && 12x Smelter Wedge
                    return condNadalia();

                case KEYID.PHARROS:
                    // Eight Pharros lockstones available
                    return condPharros();

                case KEYID.BELFRYSOL:
                    // Rotunda Lockstone && Pharros Lockstone x2
                    return condSol();

                default:
                    return condKey(kid); // Simple Key check
            }
            
            // Conditions wrappers:
            int countBranches() => placedSoFar.Where(i => i == (int)KEYID.FRAGRANTBRANCH).Count();
            int countPharros() => placedSoFar.Where(i => i == (int)KEYID.PHARROSLOCKSTONE).Count();
            bool condKey(KEYID keyid) => placedSoFar.Contains((int)keyid);
            bool condBranch() => countBranches() >= 3;
            bool condTenBranch() => countBranches() >= 10;
            bool condRotunda() => condKey(KEYID.ROTUNDA);
            bool condAshen() => condKey(KEYID.ASHENMIST);
            bool condKingsRing() => condKey(KEYID.KINGSRING);
            bool condDLC1() => condKey(KEYID.DLC1);
            bool condDLC2() => condKey(KEYID.DLC2);
            bool condSinner() => condBranch() || condKey(KEYID.ANTIQUATED);
            bool condDrangleic() => condBranch() && condRotunda() && condSinner();
            bool condAmana() => condDrangleic() && condKey(KEYID.KINGSPASSAGE);
            bool condAldias() => condBranch() && condKingsRing();
            bool condJeigh() => condAshen() && condKingsRing();
            bool condGankSquad() => condDLC1() && condKey(KEYID.ETERNALSANCTUM);
            bool condElana() => condDLC1() && condKey(KEYID.DRAGONSTONE);
            bool condFume() => condDLC2() && condKey(KEYID.SCEPTER);
            bool condBlueSmelter() => condDLC2() && condKey(KEYID.TOWER);
            bool condAlonne() => condDLC2() && condKey(KEYID.TOWER) && condKey(KEYID.SCEPTER) && condAshen();
            bool condDLC3() => condKey(KEYID.DLC3KEY) && condDrangleic();
            bool condFrigid() => condDLC3() && condKey(KEYID.GARRISONWARD);
            bool condCredits() => condDrangleic() && condJeigh();
            bool condWedges() => placedSoFar.Where(i => i == (int)KEYID.SMELTERWEDGE).Count() == 12;
            bool condNadalia() => condFume() && condBlueSmelter() && condWedges();
            bool condVendrick() => condAmana() && (placedSoFar.Where(i => i == (int)KEYID.SOULOFAGIANT).Count() >= 3);
            bool condBigPharros() => countPharros() >= 2;
            bool condPharros() => countPharros() >= 8; // surely enough
            bool condLuna() => condBranch() && condBigPharros();
            bool condSol() => condRotunda() && condBigPharros();
        }

            

        // Utility checks:
        private bool IsPlaceSaturated(Dictionary<int, ItemLot> shuflots, int placeid)
        {
            return shuflots[placeid].NumDrops == VanillaLots[placeid].NumDrops;
        }

        private void AddToLots(Dictionary<int, ItemLot> shuflots, KeyValuePair<int, RandoInfo> place, DropInfo item)
        {
            // ShuffledLots passed in by ref since dictionary
            int pkey = place.Key;
            if (shuflots.ContainsKey(pkey))
                shuflots[pkey].AddDrop(item);
            else
                shuflots[pkey] = new ItemLot(item);
        }
        private bool IsValidKeyPickup(KeyValuePair<int, RandoInfo> kvp_pickup, List<PICKUPTYPE> bannedtypes)
        {
            PICKUPTYPE[] PTs = kvp_pickup.Value.Types;
            return !PTs.Any(bannedtypes.Contains);
        }


        private void unrandomize()
        {
            var timer = new Stopwatch();
            timer.Start();
            Hook.WriteAllLots(VanillaLots);
            isRandomized = false;

            timer.Stop();
            Console.WriteLine($"Execution time: {timer.Elapsed.TotalSeconds} ms");
        }

        
        private void btnRandomize_Click(object sender, RoutedEventArgs e)
        {
            // Want to try to Hook in DS2 to change the wooden chest above cardinal tower to metal chest items:
            if (!Hook.Hooked)
                return;

            if (isRandomized)
                unrandomize();
            else
                randomize();

            // Force an area reload. TODO add warning:
            Hook.WarpLast();

            // Update UI:
            btnRandomize.Content = isRandomized ? "Unrandomize!" : "Randomize!";
            Color bkg = isRandomized ? PURPLE : LIGHTGREEN;
            lblGameRandomization.Background = new SolidColorBrush(bkg);
            string gamestate = isRandomized ? "Randomized" : "Normal";
            lblGameRandomization.Content = $"Game is {gamestate}!";
        }
    }
}

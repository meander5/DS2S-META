﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml;

namespace DS2S_META.Randomizer
{
    public enum MapArea
    {
        ThingsBetwixt,
        Majula,
        ForestOfFallenGiants,
        HeidesTowerOfFlame,
        CathedralOfBlue,
        NoMansWharf,
        TheLostBastille,
        BelfryLuna,
        SinnersRise,
        HuntsmansCopse,
        UndeadPurgatory,
        HarvestValley,
        EarthenPeak,
        IronKeep,
        BelfrySol,
        ShadedWoods,
        DoorsOfPharros,
        BrightstoneCoveTseldora,
        LordsPrivateChamber,
        ThePit,
        GraveOfSaints,
        TheGutter,
        BlackGulch,
        ShrineOfWinter,
        DrangleicCastle,
        KingsPassage,
        ShrineOfAmana,
        UndeadCrypt,
        ThroneOfWant,
        AldiasKeep,
        DragonAerie,
        DragonShrine,
        DarkChasmOfOld,
        MemoryOfJeigh,
        MemoryOfOrro,
        MemoryOfVammar,
        DragonMemories,
        MemoryOfTheKing,
        ShulvaSanctumCity,
        DragonsSanctum,
        DragonsRest,
        CaveOfTheDead,
        BrumeTower,
        IronPassage,
        MemoryOfTheOldIronKing,
        FrozenEleumLoyce,
        GrandCathedral,
        TheOldChaos,
        FrigidOutskirts,
        // Create pseudoareas Lucatiel/Benhart/RatCovenant/CovenantOfTheDark (in Chasm atm)/BellKeepers?
    };

    internal class AreaDistanceCalculator
    {
        // Price for area transition [from][to] = price
        internal static Dictionary<MapArea, Dictionary<MapArea, int>> ConnectedAreaDistances = new()
        {
            [MapArea.ThingsBetwixt] = new() { [MapArea.Majula] = 1 },
            [MapArea.Majula] = new()
            {
                [MapArea.ThingsBetwixt] = 1,
                [MapArea.ForestOfFallenGiants] = 1,
                [MapArea.HeidesTowerOfFlame] = 1,
                [MapArea.HuntsmansCopse] = 1,
                [MapArea.ShadedWoods] = 1,
                [MapArea.ThePit] = 1
            },
            [MapArea.ForestOfFallenGiants] = new()
            {
                [MapArea.Majula] = 1,
                [MapArea.TheLostBastille] = 1,
                [MapArea.MemoryOfOrro] = 1,
                [MapArea.MemoryOfJeigh] = 1,
                [MapArea.MemoryOfVammar] = 1
            },
            [MapArea.HeidesTowerOfFlame] = new()
            {
                [MapArea.Majula] = 1,
                [MapArea.CathedralOfBlue] = 1,
                [MapArea.NoMansWharf] = 1
            },
            [MapArea.CathedralOfBlue] = new() { [MapArea.HeidesTowerOfFlame] = 1 },
            [MapArea.NoMansWharf] = new() { [MapArea.HeidesTowerOfFlame] = 1, [MapArea.TheLostBastille] = 1 },
            [MapArea.TheLostBastille] = new()
            {
                [MapArea.ForestOfFallenGiants] = 1,
                [MapArea.NoMansWharf] = 1,
                [MapArea.BelfryLuna] = 1,
                [MapArea.SinnersRise] = 1
            },
            [MapArea.BelfryLuna] = new() { [MapArea.TheLostBastille] = 1 },
            [MapArea.SinnersRise] = new() { [MapArea.TheLostBastille] = 1, [MapArea.Majula] = 1 },
            [MapArea.HuntsmansCopse] = new()
            {
                [MapArea.Majula] = 1,
                [MapArea.UndeadPurgatory] = 1,
                [MapArea.HarvestValley] = 1
            },
            [MapArea.UndeadPurgatory] = new() { [MapArea.HuntsmansCopse] = 1 },
            [MapArea.HarvestValley] = new() { [MapArea.HuntsmansCopse] = 1, [MapArea.EarthenPeak] = 1 },
            [MapArea.EarthenPeak] = new() { [MapArea.HarvestValley] = 1, [MapArea.IronKeep] = 1 },
            [MapArea.IronKeep] = new()
            {
                [MapArea.EarthenPeak] = 1,
                [MapArea.BelfrySol] = 1,
                [MapArea.BrumeTower] = 1,
                [MapArea.Majula] = 1
            },
            [MapArea.BelfrySol] = new() { [MapArea.IronKeep] = 1 },
            [MapArea.ShadedWoods] = new()
            {
                [MapArea.Majula] = 1,
                [MapArea.DoorsOfPharros] = 1,
                [MapArea.ShrineOfWinter] = 1,
                [MapArea.AldiasKeep] = 1,
                [MapArea.DarkChasmOfOld] = 1
            },
            [MapArea.DoorsOfPharros] = new() { [MapArea.ShadedWoods] = 1, [MapArea.BrightstoneCoveTseldora] = 1 },
            [MapArea.BrightstoneCoveTseldora] = new()
            {
                [MapArea.DoorsOfPharros] = 1,
                [MapArea.DragonMemories] = 1,
                [MapArea.LordsPrivateChamber] = 1
            },
            [MapArea.LordsPrivateChamber] = new() { [MapArea.BrightstoneCoveTseldora] = 1, [MapArea.Majula] = 1 },
            [MapArea.ThePit] = new()
            {
                [MapArea.Majula] = 1,
                [MapArea.GraveOfSaints] = 1,
                [MapArea.TheGutter] = 1
            },
            [MapArea.GraveOfSaints] = new() { [MapArea.ThePit] = 1, [MapArea.TheGutter] = 1 },
            [MapArea.TheGutter] = new()
            {
                [MapArea.ThePit] = 1,
                [MapArea.GraveOfSaints] = 1,
                [MapArea.BlackGulch] = 1
            },
            [MapArea.BlackGulch] = new()
            {
                [MapArea.TheGutter] = 1,
                [MapArea.ShulvaSanctumCity] = 1,
                [MapArea.DarkChasmOfOld] = 1,
                [MapArea.Majula] = 1
            },
            [MapArea.ShrineOfWinter] = new()
            {
                [MapArea.ShadedWoods] = 1,
                [MapArea.DrangleicCastle] = 1,
                [MapArea.FrozenEleumLoyce] = 1
            },
            [MapArea.DrangleicCastle] = new()
            {
                [MapArea.ShrineOfWinter] = 1,
                [MapArea.ThroneOfWant] = 1,
                [MapArea.KingsPassage] = 1,
                [MapArea.DarkChasmOfOld] = 1
            },
            [MapArea.KingsPassage] = new() { [MapArea.DrangleicCastle] = 1, [MapArea.ShrineOfAmana] = 1 },
            [MapArea.ShrineOfAmana] = new() { [MapArea.KingsPassage] = 1, [MapArea.UndeadCrypt] = 1 },
            [MapArea.UndeadCrypt] = new() { [MapArea.ShrineOfAmana] = 1, [MapArea.MemoryOfTheKing] = 1 },
            [MapArea.ThroneOfWant] = new() { [MapArea.DrangleicCastle] = 1 },
            [MapArea.AldiasKeep] = new() { [MapArea.ShadedWoods] = 1, [MapArea.DragonAerie] = 1 },
            [MapArea.DragonAerie] = new() { [MapArea.AldiasKeep] = 1, [MapArea.DragonShrine] = 1 },
            [MapArea.DragonShrine] = new() { [MapArea.DragonAerie] = 1 },
            [MapArea.DarkChasmOfOld] = new()
            { // Those cause shorts in calculation, eg. path from gulch to castle would lead through chasm (which is nonsense), unless path prices are disproportionately high
                //[MapArea.ShadedWoods] = 1,
                //[MapArea.BlackGulch] = 1,
                //[MapArea.DrangleicCastle] = 1
            },
            [MapArea.MemoryOfJeigh] = new() { [MapArea.ForestOfFallenGiants] = 1 },
            [MapArea.MemoryOfOrro] = new() { [MapArea.ForestOfFallenGiants] = 1 },
            [MapArea.MemoryOfVammar] = new() { [MapArea.ForestOfFallenGiants] = 1 },
            [MapArea.DragonMemories] = new() { [MapArea.BrightstoneCoveTseldora] = 1 },
            [MapArea.MemoryOfTheKing] = new() { [MapArea.UndeadCrypt] = 1 },
            [MapArea.ShulvaSanctumCity] = new()
            {
                [MapArea.BlackGulch] = 1,
                [MapArea.DragonsSanctum] = 1,
                [MapArea.CaveOfTheDead] = 1
            },
            [MapArea.DragonsSanctum] = new() { [MapArea.ShulvaSanctumCity] = 1, [MapArea.DragonsRest] = 1 },
            [MapArea.DragonsRest] = new() { [MapArea.DragonsSanctum] = 1 },
            [MapArea.CaveOfTheDead] = new() { [MapArea.ShulvaSanctumCity] = 1 },
            [MapArea.BrumeTower] = new()
            {
                [MapArea.IronKeep] = 1,
                [MapArea.IronPassage] = 1,
                [MapArea.MemoryOfTheOldIronKing] = 1
            },
            [MapArea.IronPassage] = new() { [MapArea.BrumeTower] = 1 },
            [MapArea.MemoryOfTheOldIronKing] = new() { [MapArea.BrumeTower] = 1 },
            [MapArea.FrozenEleumLoyce] = new()
            {
                [MapArea.ShrineOfWinter] = 1,
                [MapArea.GrandCathedral] = 1,
                [MapArea.FrigidOutskirts] = 1
            },
            [MapArea.GrandCathedral] = new() { [MapArea.FrozenEleumLoyce] = 1, [MapArea.TheOldChaos] = 1 },
            [MapArea.TheOldChaos] = new() { [MapArea.GrandCathedral] = 1 },
            [MapArea.FrigidOutskirts] = new() { [MapArea.FrozenEleumLoyce] = 1 }
        };

        internal static int AreaCount = Enum.GetValues(typeof(MapArea)).Length;
        internal static int[][] DistanceMatrix = new int[AreaCount][];
        internal static KeyValuePair<int, MapArea>[][] SortedAreasByDistanceMatrix = new KeyValuePair<int, MapArea>[AreaCount][];

        internal static void CalculateDistanceMatrix()
        {
            for (int areaID = 0; areaID < AreaCount; areaID++)
            {
                MapArea area = (MapArea)areaID;

                DistanceMatrix[areaID] = new int[AreaCount];
                Array.Fill<int>(DistanceMatrix[areaID], int.MaxValue, 0, AreaCount);
                DistanceMatrix[areaID][areaID] = 0;

                var areasToExpand = ConnectedAreaDistances[area].ToList();
                while (areasToExpand.Count() > 0)
                {
                    var expandedArea = areasToExpand.First().Key;
                    var currentPrice = areasToExpand.First().Value;

                    if (currentPrice < DistanceMatrix[areaID][(int)expandedArea])
                    {
                        DistanceMatrix[areaID][(int)expandedArea] = currentPrice;
                        foreach (var kvp in ConnectedAreaDistances[expandedArea].ToList())
                        {
                            if (kvp.Key == area)
                            {
                                continue;
                            }

                            areasToExpand.Add(new KeyValuePair<MapArea, int>(kvp.Key, kvp.Value + currentPrice));
                        }
                    }
                    areasToExpand.RemoveAt(0);
                }

                // Idea: Special Shrine of Winter path cost calculations for areas before the door - something like Max(Primals) + (ShadedWoods -> SoW)? Or perhaps even add cost of path to Shaded Woods from the area?
                // This logic wouldn't apply to areas after SoW, of course (and paths from SoW to SW), but either option would cause the cost of passing the SoW door from areas before Shaded Woods to be ridiculously high
                // For now, it could be simulated by increased Shaded Woods -> Shrine of Winter price

                // Simpler (?) version of this problem would be cost adjustment of key-dependent transitions based on the location of the necessary key, but that would require distance matrix calculation after the key placement phase
                // |-> Simpler perhaps in sense there's just one area you need to visit in order to be able to pass through the transition, but path to the key's area may need some other key(s), what would quickly complicate stuff
                // At that point, all transitions traversable only in one-way could be considered (eg. Pit/Grave->Gutter, Gutter->Gulch, Wharf/FoFG->Bastille, Copse->Valley
                // Then distance matrix would contain the price of traversing between areas, instead of distance of areas (and probably there
            }

            CreateSortedDistanceMatrix();
        }

        internal static void CreateSortedDistanceMatrix()
        {
            for (int areaID = 0; areaID < AreaCount; areaID++)
            {
                SortedAreasByDistanceMatrix[areaID] = DistanceMatrix[areaID].Select((distance, index) => new KeyValuePair<int, MapArea>(distance, (MapArea)index)).ToArray();
                Array.Sort(SortedAreasByDistanceMatrix[areaID], (a, b) => a.Key < b.Key ? -1 : a.Key == b.Key ? 0 : 1);
            }
        }

        internal static bool IsLocationInAreas(int locationID, IEnumerable<MapArea> areas)
        {
            foreach (var area in areas)
            {
                if (areaItems.ContainsKey(area) && areaItems[area].Contains(locationID))
                {
                    return true;
                }
            }
            return false;
        }

        // Taken from CasualItemSet.cs, with some items split into their subareas
        internal static Dictionary<MapArea, HashSet<int>> areaItems = new()
        {
            [MapArea.ThingsBetwixt] = new() { 50000000, 50000001, 50000002, 50000003, 50000100, 50000101, 50000102, 50000103, 50000200, 50000201, 50000202, 50000203, 50000300, 50000301, 50000302, 50000303, 1705000, 1723000, 10025010, 10026000, 10026001, 10026020, 10026030, 10026031, 10026040, 10026050, 10026060, 10026070, 10026080, 10026090, 10026100, 60008100, 60008110 },
            [MapArea.Majula] = new() { 1700000, 1704000, 1741000, 1741010, 1751010, 1761000, 1762000, 1763000, 1764000, 2001000, 2001011, 2001012, 2001013, 2009000, 2009011, 2009012, 2009013, 10045000, 10045001, 10045002, 10045010, 10045040, 10045060, 10045070, 10045600, 10046000, 10046010, 10046020, 10046030, 10046040, 10046070, 10046100, 10046110, 10046120, 10296000, 10296010 },
            [MapArea.ForestOfFallenGiants] = new() { 309600, 318000, 60008000, 10105020, 10105021, 10106110, 10106000, 10106230, 10106260, 10106280, 10106580, 10106590, 10106200, 10106170, 1744020, 1751000, 10105010, 10105070, 10106050, 10105030, 10105040, 10105041, 10105050, 10105080, 10105090, 10105100, 10105110, 10105120, 10105130, 10105140, 10105150, 10106010, 10106030, 10106060, 10106061, 10106270, 10106070, 10106080, 10106090, 10106100, 10106120, 60002000, 10106130, 10106140, 10106150, 10106160, 10106180, 10106190, 10106210, 10106220, 10106240, 10106250, 10106300, 10106310, 10106320, 10106321, 10106340, 10106350, 10106360, 10106370, 10106371, 10106380, 10106290, 10106390, 10106400, 10106410, 10106420, 10106430, 10106440, 10106450, 10106460, 10106470, 10106480, 10106490, 10106500, 10106510, 10106520, 10106530, 10106540, 10106550, 10106560, 10106570, 10106600, 10106610, 10106620, 10106630 },
            [MapArea.HeidesTowerOfFlame] = new() { 611000, 10305010, 10305020, 10306000, 10306010, 10306020, 10306030, 10315000, 10315001, 10316010, 10316040, 10316041, 10316050, 10316090, 10316100, 10316101 },
            [MapArea.CathedralOfBlue] = new() { 625000, 2002000, 2002011, 2002012, 2002013, 1785040, 10315010, 10315020, 10315030, 10316110 },
            [MapArea.NoMansWharf] = new() { 303300, 10185000, 10185001, 10185030, 10185040, 10185050, 10185060, 10185070, 10185071, 10185080, 10185081, 10185100, 10185110, 10185120, 10186000, 10186010, 10186020, 10186021, 10186030, 10186050, 10186070, 10186071, 10186100, 10186110, 10186120, 10186130, 10186140, 10186150, 10186160, 10186170 },
            [MapArea.TheLostBastille] = new() { 10166460, 10166470, 10166300, 10166310, 10166010, 10166070, 10165240, 10166420, 10166421, 10165160, 10165170, 10165180, 10165190, 10165150, 1764300, 10165140, 10166080, 10165250, 10165260, 10166020, 10166030, 10166050, 10166100, 10166150, 325000, 10166320, 10166270, 10166000, 10165040, 10165210, 10166370, 10165041, 10165050, 10165080, 10165130, 10166380, 10166180, 10166290, 10166490, 10166430, 10165010, 10165000, 10165020, 10165070, 10165110, 10166410, 10166130, 10166480, 10166200, 10166260, 10166040, 10166440, 10166441, 1768000, 10166190, 10166191 },
            [MapArea.BelfryLuna] = new() { 324000, 324001, 10165200, 10165220, 10165230, 10166140, 10166160, 10166170, 10166250, 10166390, 10166400 },
            [MapArea.SinnersRise] = new() { 626000, 626001, 10165120, 10166060, 10166090, 10166110, 10166120, 10166230, 10166280, 10166330, 10166350, 10166360, 10166450 },
            [MapArea.ThePit] = new() { 10045020, 10045030, 10045050, 10046140, 10046130, 10046060, 10046050, 10046090, 10046150, 10345000, 10345010, 10345020, 10346000, 10346010, 10346070, 10346090, 10346091, 10346100, 10346110 },
            [MapArea.GraveOfSaints] = new() { 226100, 10346020, 10346030, 10346031, 10346040, 10346050, 10346060, 10346080 },
            [MapArea.TheGutter] = new() { 10256220, 10256250, 10256000, 10256410, 10256420, 10256430, 10256440, 10256450, 10255010, 10255110, 10256030, 10256060, 10256090, 10255040, 10256230, 10256240, 10256260, 10256270, 10256280, 10256290, 10256330, 10256400, 10255100, 10256310, 10256170, 10256300, 10256320, 10255030, 10256340, 10256130, 10256160 },
            [MapArea.BlackGulch] = new() { 10255130, 10256370, 10256380, 10256390, 10255050, 10255090, 10256350, 60001000, 10256360, 326000, 326001, 10256500, 10256210, 10255120 },
            [MapArea.HuntsmansCopse] = new() { 154000, 154001, 1770000, 10046080, 10235010, 10235020, 10236000, 10236020, 10236021, 10236030, 10236040, 10236050, 10236060, 10236070, 10236071, 10236080, 10236090, 10236100, 10236110, 10236120, 10236130, 10236131, 10236140, 10236150, 10236160, 10236170, 10236230, 10236240, 10236250, 10236260, 10236270 },
            [MapArea.UndeadPurgatory] = new() { 619100, 619101, 2003000, 2003011, 2003012, 2003013, 1783040, 10236010, 10236180, 10236190, 10236200, 10236210, 10236220 },
            [MapArea.HarvestValley] = new() { 2007000, 2007011, 2007012, 2007013, 10175020, 10175021, 10175030, 10175110, 10176000, 10176020, 10176030, 10176060, 10176070, 10176080, 10176090, 10176130, 10176160, 10176180, 10176200, 10176210, 10176220, 10176221, 10176230, 10176231, 10176250, 10176260, 10176270, 10176280, 10176290, 10176300, 10176340, 10176350, 10176370, 10176390, 10176400, 10176410, 10176460, 10176461, 10176470, 10176480, 10176490, 10176500, 10176510, 10176520, 10176530, 10176540, 10176550, 10176560, 10176570, 10176580, 10176590, 10176600, 10176610, 10176620 },
            [MapArea.EarthenPeak] = new() { 500000, 501000, 501001, 1744010, 10175040, 10175050, 10175060, 10175070, 10175090, 10175120, 10175130, 10175140, 10175150, 10175160, 10176010, 10176040, 10176050, 10176100, 10176110, 10176120, 10176140, 10176150, 10176170, 10176171, 10176190, 10176420, 10176430, 10176440, 10176450, 10176630 },
            [MapArea.IronKeep] = new() { 305000, 607000, 607001, 1772000, 2008000, 2008011, 2008012, 2008013, 10195000, 10195001, 10195030, 10195040, 10195090, 10195100, 10195110, 10195140, 10195150, 10196030, 10196040, 10196050, 10196060, 10196070, 10196080, 10196090, 10196100, 10196110, 10196111, 10196120, 10196130, 10196140, 10196150, 10196160, 10196170, 10196180, 10196190, 10196210, 10196211, 10196220 },
            [MapArea.BelfrySol] = new() { 10195130, 10196000, 10196010, 10196020, 10195120, 10196200, 10195050, 10195060 },
            [MapArea.ShadedWoods] = new() { 10295000, 10296020, 10326000, 10326110, 10326270, 10326280, 10326170, 10326020, 10326030, 10325020, 10325050, 10325080, 10326060, 1307000, 10326240, 10326070, 10326080, 10326081, 10326090, 10326100, 10326101, 1502000, 1502010, 10325000, 10325001, 10325010, 10325030, 10325060, 10325110, 10325120, 10326010, 10326040, 10326120, 10326130, 10326140, 10326141, 10326150, 10326160, 10326180, 10326190, 10326191, 10326210, 10326200, 503000, 503001, 10325100, 10326230, 10325040, 60009000 },
            [MapArea.DoorsOfPharros] = new() { 223500, 10335000, 10335010, 10335020, 10335021, 10335030, 10335031, 10335040, 10336000, 10336010, 10336011, 10336020, 10336040, 10336041, 10336050, 10336060, 10336070, 10336080 },
            [MapArea.BrightstoneCoveTseldora] = new() { 106000, 603000, 603001, 1742000, 1742010, 1784000, 10145060, 10145061, 10145070, 10145080, 10145110, 10145120, 10145130, 10146000, 10146010, 10146030, 10146040, 10146050, 10146051, 10146060, 10146110, 10146140, 10146150, 10146070, 10146080, 10146090, 10146100, 10146120, 10146130, 10146160, 10146170, 10146180, 10146181, 10146190, 10146200, 10146210, 10146230, 10146020, 10146240, 10146250, 10146260, 10146270, 10146280, 10146290, 10146300, 10146310, 10146320, 10146220, 10146330, 10146340, 10146350, 10146360, 10146370, 10146380, 10146381, 10146390, 10146400, 10146410, 10146420, 10146480, 10146490, 10146500, 10146510, 10146520 },
            [MapArea.DragonMemories] = new() { 60005000 },
            [MapArea.ShrineOfWinter] = new() { 10326260, 10326220, 10326250 },
            [MapArea.DrangleicCastle] = new() { 309610, 1721000, 1760200, 20215000, 20215010, 20215011, 20215020, 20215021, 20215050, 20215130, 20216000, 20215070, 20215080, 20215090, 20215060, 20215100, 20215110, 20215120, 20215140, 20215160, 20215170, 20216010, 20216020, 20216021, 20216030, 20216050, 20216090, 20216100, 20216110, 20216040, 20216130, 20216140 },
            [MapArea.DarkChasmOfOld] = new() { 2006000, 2006011, 2006012, 506100, 2006013, 1725000 }, // Those include Grandahl's items, not sure if that's a great idea
            [MapArea.KingsPassage] = new() { 504000, 504001, 20215150, 20216060, 20216061, 20216070, 20216120, 20216080 },
            [MapArea.ThroneOfWant] = new() { 332000, 332001, 627000 },
            [MapArea.ShrineOfAmana] = new() { 602000, 1760010, 1760000, 1760020, 1760110, 1760100, 1760120, 20115000, 20115010, 20115020, 20115030, 20115040, 20115050, 20115051, 20115060, 20115080, 20115090, 20115070, 20115500, 20116000, 20116010, 20116011, 20116030, 20116040, 20116060, 20116070, 20116080, 20116090, 20116100, 20116130, 20116140, 20116150, 20116120, 20116020, 20116160, 20116170, 20116171, 20116210, 20116190, 20116200, 20116220, 20116110, 20115100, 20115110 },
            [MapArea.UndeadCrypt] = new() { 333000, 333001, 1506000, 20245000, 20245010, 20245020, 20245030, 20245040, 20245050, 20245070, 20245080, 20245090, 20245100, 20246000, 20246010, 20246011, 20246030, 20246040, 20246050, 20246070, 20246100, 20246110, 20246111, 20246120, 20246121, 20246130, 20246140, 20246150, 20246151, 20246180, 20246190, 20246200, 20246210, 20246220, 20246500 },
            [MapArea.AldiasKeep] = new() { 212000, 1752000, 1771010, 1771020, 1771030, 1771040, 1771000, 10155000, 10155010, 10155020, 10155030, 10156000, 10156010, 10156030, 10156031, 10156040, 10156140, 10156050, 10156060, 10156130, 10156070, 10156100, 10156160, 10156161, 10156020, 10156080, 10156170, 10156090, 10156180, 10156190, 10156200, 10156150, 60050000 },
            [MapArea.DragonAerie] = new() { 1701000, 10276010, 10276020, 10276030, 10276120, 10276040, 10276041, 10276190, 10276060, 10276061, 10276180, 10276080, 10276070, 10276000, 10276050, 10276090, 10276100, 10276110, 10276170, 10276130, 10276160 },
            [MapArea.DragonShrine] = new() { 600000, 1787000, 10275000, 10275060, 10275010, 10275020, 10275021, 10276140, 10275030, 10275040, 10275050, 10275070, 10276150, 60003000 },
            [MapArea.MemoryOfJeigh] = new() { 309700, 309701, 60004000, 20106100, 20106110, 20106111, 20106120 },
            [MapArea.MemoryOfOrro] = new() { 1743000, 20105020, 20105030, 20105040, 20105050, 20105060, 20106050, 20106070, 20106080, 20106090, 20106130, 20106140, 20106141 },
            [MapArea.MemoryOfVammar] = new() { 1724000, 20105000, 20105001, 20105010, 20106000, 20106010, 20106011, 20106020, 20106030, 20106040, 20106060, 20106061, 20106150 },
            [MapArea.ShulvaSanctumCity] = new() { 50356130, 50356000, 50356010, 50356020, 50356030, 50356140, 50356160, 50356560, 50356400, 50356170, 50356190, 50356590, 50356600, 50356050, 50356040, 50356200, 50356060, 50356220, 50356570, 50356580, 50356410, 50356380, 50356530, 50356240, 50356250, 50356390, 50355140 },
            [MapArea.DragonsSanctum] = new() { 682000, 50356430, 50355050, 50355250, 50355260, 50355270, 50355280, 50355010, 50355350, 50356260, 50356280, 50356290, 50356300, 50356310, 50355090, 50356270, 50356090, 50356100, 50355020, 50355030, 50355060, 50356320, 50356330, 50356340, 50356350, 50355120, 50356420, 50355110, 50355070, 50356630, 50356640, 50355130, 50356360, 50356370, 50356440, 50356650, 50356660, 50356510, 50356150, 50356450, 50356460, 50356470, 50356480, 50356490, 50356500, 50356520, 50355190, 50355200, 50355210, 50355220, 50355230, 50355240 },
            [MapArea.DragonsRest] = new() { 681000, 50356540, 60020000 },
            [MapArea.CaveOfTheDead] = new() { 862000, 862001, 50355180, 50356610, 50356620, 50356670, 50355150, 50356210 },
            [MapArea.BrumeTower] = new() { 675000, 675010, 60019000, 50367140, 50366340, 50366020, 50366360, 50366380, 50366350, 50368000, 50366030, 50365000, 50366900, 50365010, 50366000, 50365560, 50366370, 50366390, 50365500, 50365510, 50365090, 50366260, 50366440, 50366280, 50366300, 50366310, 50366320, 50366480, 50368020, 50368030, 50368040, 50368050, 50366510, 50365700, 50366810, 50368060, 50366820, 50366800, 50366170, 50366580, 50365540, 50365590, 50366570, 50367090, 50367100, 50367110, 50367120, 60014000, 50366830, 50366850, 50366860, 50366870, 50366710, 50366720, 50368070, 50366210, 50365680, 50366760, 50365080, 50368010, 50368080, 60016000, 50365020, 50365550, 50366240, 50366250, 50367130, 50366880, 50366890, 50365690, 50365580, 50366070, 50366530, 50365650, 50366680, 50366700, 50365570, 50366520, 50365030, 50366740, 60012100, 60012000, 60012050, 60012080, 60012010, 60012020, 60012030, 60012040, 60012060, 60012070, 60012090 },
            [MapArea.IronPassage] = new() { 305010, 50367010, 50367020, 50367030, 50367040, 50367050, 50366980, 50366990, 50367000, 50367060 },
            [MapArea.MemoryOfTheOldIronKing] = new() { 680000, 50366910, 50366920, 50366930, 50366940, 50366950, 50366970, 50366960, 60013000 },
            [MapArea.FrozenEleumLoyce] = new() { 679000, 50376340, 50376410, 50376350, 50376050, 50376750, 50376760, 50376070, 50376060, 50376010, 50375510, 50375520, 50375540, 50376000, 50376080, 50376090, 50376580, 50376300, 50376570, 50376540, 50376530, 50376100, 50376110, 50375580, 50375590, 50375600, 50375610, 50376120, 50376130, 50376140, 50375550, 50375530, 50376560, 50375740, 50376590, 50376360, 50375640, 50376150, 50376370, 50375500, 50376160, 50376170, 50375690, 50376600, 50375700, 50375730, 50376380, 50375670, 50375660, 50376510, 50376770, 50376400, 50375680, 50376310, 50376320, 50376180, 50376190, 50376660, 50376200, 50375560, 50376520, 50376610, 50376620, 50376630, 50376670, 50376680, 50376690, 50376640, 50376650, 50376420, 50376430, 50376440 },
            [MapArea.GrandCathedral] = new() { 50375710, 1788000, 1788010, 1788020, 1788030 },
            [MapArea.TheOldChaos] = new() { 690000, 60031000 },
            [MapArea.FrigidOutskirts] = new() { 679010, 50376730, 50376740, 50376220, 50376450, 50376230, 50376460, 50376470, 50376710 }
        };
    }
}
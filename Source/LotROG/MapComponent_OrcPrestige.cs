using RimWorld;
using Verse;

namespace LotROG
{
    public class MapComponent_OrcPrestige : MapComponent
    {
        private const int POINTS_KILL = 5;
        private const int POINTS_BANNERS = 5;
        private const int POINTS_SKULLPOLES = 5;
        private const int POINTS_BADASSORCS = 10;
        private const int POINTS_ARENA = 100;

        private int kills = 0;
        private int banners = 0;
        private int skullpoles = 0;
        private int badassOrcs = 0;
        private int arenas = 0;

        public int Kills
        {
            get => kills;
            set => kills = value;
        }

        public int Banners
        {
            get => banners;
            set => banners = value;
        }

        public int Skullpoles
        {
            get => skullpoles;
            set => skullpoles = value;
        }

        public int BadassOrcs
        {
            get => badassOrcs;
            set => badassOrcs = value;
        }

        public int Arenas
        {
            get => arenas;
            set => arenas = value;
        }

        private int cachedPrestige = 0;

        public int Prestige
        {
            get
            {
                if (Find.TickManager.TicksGame % 250 == 0)
                {
                    cachedPrestige =
                        (kills * POINTS_KILL) +
                        (banners * POINTS_BANNERS) +
                        (skullpoles * POINTS_SKULLPOLES) +
                        (badassOrcs * POINTS_BADASSORCS) +
                        (arenas * POINTS_ARENA);
                }

                return cachedPrestige;
            }
        }

        private bool? isOrcColony; 
        public bool IsOrcColony
        {
            get
            {
                if (isOrcColony == null)
                {
                    isOrcColony = map.mapPawns.AllPawnsSpawned.Any(x =>
                        x.Faction == Faction.OfPlayer && x.def.defName.Contains("LotRG"));
                }
                return isOrcColony.Value;
            }
        }

        public MapComponent_OrcPrestige(Map map) : base(map)
        {
            
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref kills, "kills", 0);
            Scribe_Values.Look(ref banners, "banners", 0);
            Scribe_Values.Look(ref skullpoles, "skullpoles", 0);
            Scribe_Values.Look(ref badassOrcs, "badassOrcs", 0);
            Scribe_Values.Look(ref arenas, "arenas", 0);
            Scribe_Values.Look(ref cachedPrestige, "cachedPrestige", 0);
        }
    }

}
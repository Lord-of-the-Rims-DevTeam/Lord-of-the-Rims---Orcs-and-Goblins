using System.Collections.Generic;
using Verse;

namespace LotROG
{
    /// <summary>
    /// MELKOR PITS
    /// 
    /// When breeding is not fast enough to raise an army, the Melkor pit provides.
    /// The Melkor pit is a one per colony construction that digs deep into the bowels of the earth.
    /// Tolkien hints at around 7 different possible origins for orcs,
    /// (Source: https://scifi.stackexchange.com/questions/26725/what-is-the-true-origin-of-the-orcs)
    ///   without a definitive origin, so for the purpose of large army-based,
    ///   we have chosen for orcs to originate from a combination of stone
    ///   and corruption of other races. To this end, orcs can be produced in Melkor pits.
    /// Melkor pits allow players to corrupt other races into the orcs we know.
    /// Depending on the race of those thrown in the pit along with special stones,
    ///   different orcs will arise. 
    /// </summary>
    public class MelkorPit : Building
    {
        private bool active = true;
        private List<OrcStew> stewsABrewin;
        
        public override void Tick()
        {
            base.Tick();
            foreach (var stew in stewsABrewin)
            {
                stew.Tick();
            }
        }


        public static void TrySpawnOrcFrom(Pawn victim, ThingWithComps ingredient)
        {
            
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref this.stewsABrewin, "stewsABrewin", LookMode.Deep);
        }
    }

    /// <summary>
    /// Orc Stew
    /// This is the formation of an orc.
    /// It is composed of a primary pawn (non-orc), and ingredients.
    /// Progress increases with each tick from being in the melkor pit.
    /// When the stew reaches 100%, it is ready to be fished out and opened.
    /// </summary>
    public class OrcStew : IExposable
    {
        public Pawn Meat;
        public List<ThingWithComps> ingredients;
        private float progress;

        public void Tick()
        {
            progress += 0.01f;
        }

        public OrcStew(Pawn newMeat, List<ThingWithComps> newIngredients)
        {
            Meat = newMeat;
            ingredients = newIngredients;
            progress = 0f;
        }

        public void ExposeData()
        {
            Scribe_References.Look(ref Meat, "meat");
            Scribe_Collections.Look(ref this.ingredients, "ingredients", LookMode.Deep);
            Scribe_Values.Look(ref progress, "progress", 0f);
        }
    }
}
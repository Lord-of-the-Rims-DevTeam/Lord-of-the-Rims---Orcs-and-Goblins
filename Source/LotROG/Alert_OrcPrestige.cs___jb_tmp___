using System.Collections.Generic;
using System.Text;
using RimWorld;
using Verse;

namespace LotROG
{
    // Token: 0x020007C1 RID: 1985
    public class Alert_OrcPrestige : Alert
    {
        // Token: 0x06002C0B RID: 11275 RVA: 0x0014A1DB File Offset: 0x001485DB
        public Alert_OrcPrestige()
        {
            defaultLabel = "Fuck it";
            defaultPriority = AlertPriority.Medium;
        }

        // Token: 0x06002C0D RID: 11277 RVA: 0x0014A218 File Offset: 0x00148618
        public override string GetExplanation()
        {
            return "Goddamn it";
        }

        // Token: 0x06002C0E RID: 11278 RVA: 0x0014A29C File Offset: 0x0014869C
        public override AlertReport GetReport()
        {
            if (Find.AnyPlayerHomeMap == null ||
                (Find.CurrentMap is Map map && map.IsPlayerHome && 
                map.GetComponent<MapComponent_OrcPrestige>() is MapComponent_OrcPrestige orcPrestige &&
                !orcPrestige.IsOrcColony))
            {
                return false;
            }
            return true;
        }
    }
}

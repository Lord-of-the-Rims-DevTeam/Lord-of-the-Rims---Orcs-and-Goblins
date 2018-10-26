using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;
using System.Reflection;
using UnityEngine;
using Verse.AI.Group;

namespace LotROG
{
//
//    [StaticConstructorOnStartup]
//    static class HarmonyPatches
//    {
//        
//        static HarmonyPatches()
//        {
//            var harmony = HarmonyInstance.Create("rimworld.rimquest");
//            harmony.Patch(AccessTools.Method(typeof(PawnUIOverlay), "DrawPawnGUIOverlay"),
//                null, new HarmonyMethod(typeof(HarmonyPatches), nameof(DrawPawnGUIOverlay)));
//            harmony.Patch(AccessTools.Method(typeof(PawnRenderer), "RenderPawnAt", new []{typeof(Vector3), typeof(RotDrawMode), typeof(bool)}),
//                null, new HarmonyMethod(typeof(HarmonyPatches), nameof(RenderPawnAt)));
//            harmony.Patch(AccessTools.Method(typeof(IncidentWorker_VisitorGroup), "TryConvertOnePawnToSmallTrader"),
//                null, new HarmonyMethod(typeof(HarmonyPatches), nameof(AddQuestGiver)));
//            harmony.Patch(AccessTools.Method(typeof(PawnGroupKindWorker_Trader), "GenerateGuards"),
//                null, new HarmonyMethod(typeof(HarmonyPatches), nameof(AddQuestGiverTwo)));
//            harmony.Patch(AccessTools.Method(typeof(FloatMenuMakerMap), "AddHumanlikeOrders"),
//                null, new HarmonyMethod(typeof(HarmonyPatches), nameof(AddHumanlikeOrders)));
//        }
//
//        //PawnRenderer
//        public static void RenderPawnAt(PawnRenderer __instance, Vector3 drawLoc, RotDrawMode bodyDrawType, bool headStump)
//        {
//            var pawn = Traverse.Create(__instance).Field("pawn").GetValue<Pawn>();
//            if (pawn.GetQuestPawn() != null) RenderExclamationPointOverlay(pawn);            
//        }
//
//        //PawnGroupKindWorker_Trader
//        public static void AddQuestGiverTwo(PawnGroupMakerParms parms, PawnGroupMaker groupMaker, Pawn trader,
//            List<Thing> wares, List<Pawn> outPawns)
//        {
//            LongEventHandler.QueueLongEvent(() =>
//            {
//                var pawn = outPawns.FirstOrDefault(x => x.Spawned);
//                Map map = pawn?.MapHeld;
//                if (map != null)
//                {
//                    List<Pawn> newPawnList = map.mapPawns.AllPawnsSpawned.FindAll(x => x.Faction == pawn.Faction && !x.IsPrisoner);   
//                    var newQuestPawn = RimQuestUtility.GetNewQuestGiver(outPawns);
//                    if (newQuestPawn?.Faction == null) return;
//                    var questPawns = Find.World.GetComponent<RimQuestTracker>().questPawns;
//                    if (!questPawns.Any(x => x.pawn == newQuestPawn))
//                    {
//                        questPawns.Add(new QuestPawn(newQuestPawn));
//                    }
//                }
//            }, "RQ_LoadingScreen".Translate(), true, null);
//
//        }
//
//        //FloatMenuMakerMap
//        public static void AddHumanlikeOrders(Vector3 clickPos, Pawn pawn, List<FloatMenuOption> opts)
//        {
//            foreach (var localTargetInfo4 in GenUI.TargetsAt(clickPos, ForQuest(), true))
//            {
//                var dest = localTargetInfo4;
//                if (!pawn.CanReach(dest, PathEndMode.OnCell, Danger.Deadly))
//                {
//                    opts.Add(new FloatMenuOption("RQ_CannotQuest".Translate() + " (" + "NoPath".Translate() + ")", null));
//                }
//                else if (pawn.skills.GetSkill(SkillDefOf.Social).TotallyDisabled)
//                {
//                    opts.Add(new FloatMenuOption("CannotPrioritizeWorkTypeDisabled".Translate(new object[]
//                    {
//                        SkillDefOf.Social.LabelCap
//                    }), null));
//                }
//                else
//                {
//                    var pTarg = (Pawn)dest.Thing;
//                    void Action4()
//                    {
//                        var job = new Job(RimQuestDefOf.RQ_QuestWithPawn, pTarg);
//                        job.playerForced = true;
//                        pawn.jobs.TryTakeOrderedJob(job);
//                    }
//                    var str = string.Empty;
//                    if (pTarg.Faction != null)
//                        str = " (" + pTarg.Faction.Name + ")";
//                    var label = "RQ_QuestWith".Translate(new object[]
//                    {
//                        pTarg.LabelShort
//                    }) + str;
//                    var action = (Action) Action4;
//                    var priority2 = MenuOptionPriority.InitiateSocial;
//                    var thing = dest.Thing;
//                    opts.Add(FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption(label, action, priority2, null, thing), pawn, pTarg));
//                }
//            }
//        }
//
//        
//        //public class PawnUIOverlay
//        public static void DrawPawnGUIOverlay(PawnUIOverlay __instance)
//        {
//
//        }
//        
//
//        //IncidentWorker_VisitorGroup
//        //TryConvertOneSmallTrader
//        public static void AddQuestGiver(List<Pawn> pawns, Faction faction, Map map, ref bool __result)
//        {
//            if (!__result || !(pawns?.Count > 1)) return;
//            var newQuestPawn = RimQuestUtility.GetNewQuestGiver(pawns);
//            if (newQuestPawn?.Faction == null) return;
//            var questPawns = Find.World.GetComponent<RimQuestTracker>().questPawns;
//            if (!questPawns.Any(x => x.pawn == newQuestPawn))
//            {
//                questPawns.Add(new QuestPawn(newQuestPawn));
//            }
//        }
//
//
//
//        private static TargetingParameters ForQuest()
//        {
//            var targetingParameters = new TargetingParameters
//            {
//                canTargetPawns = true,
//                canTargetBuildings = false,
//                mapObjectTargetsMustBeAutoAttackable = false,
//                validator = x =>
//                    x.Thing is Pawn pawn &&
//                    pawn.GetQuestPawn() != null
//            };
//            return targetingParameters;
//        }
//
//
//        
//        private static void RenderExclamationPointOverlay(Thing t)
//        {
//            if (!t.Spawned) return;
//            var drawPos = t.DrawPos;
//            drawPos.y = Altitudes.AltitudeFor(AltitudeLayer.MetaOverlays) + 0.28125f;
//            if (t is Pawn)
//            {
//                drawPos.x += (float)t.def.size.x - 0.52f;
//                drawPos.z += (float)t.def.size.z - 0.45f;
//            }
//            RenderPulsingOverlayQuest(t, HarmonyPatches.ExclamationPointMat, drawPos, MeshPool.plane05);
//        }
//        
//        private static readonly Material ExclamationPointMat = 
//            MaterialPool.MatFrom("UI/Overlays/RQ_ExclamationPoint", ShaderDatabase.MetaOverlay);
//        
//        private static void RenderPulsingOverlayQuest(Thing thing, Material mat, Vector3 drawPos, Mesh mesh)
//        {
//            var num = (Time.realtimeSinceStartup + 397f * (float)(thing.thingIDNumber % 571)) * 4f;
//            var num2 = ((float)Math.Sin((double)num) + 1f) * 0.5f;
//            num2 = 0.3f + num2 * 0.7f;
//            var material = FadedMaterialPool.FadedVersionOf(mat, num2);
//            Graphics.DrawMesh(mesh, drawPos, Quaternion.identity, material, 0);
//        }
//        
//        
//    }
}

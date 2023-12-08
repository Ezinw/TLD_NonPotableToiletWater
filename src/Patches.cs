using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace NonPotableToiletWater
{
    [HarmonyPatch(typeof(WaterSource), nameof(WaterSource.Update))]
    public class NonPotableToiletWater
    {
        private static int frameCounter = 0;

        static void Postfix(WaterSource __instance)
        {
            frameCounter++;

            int frameInterval = 5;

            if (frameCounter >= frameInterval)
            {
                ToiletWaterQuality();

                frameCounter = 0;
            }
        }

        static void ToiletWaterQuality()
        {
            float searchRadius = 3.0f;

            Transform playerTransform = GameManager.GetPlayerTransform();

            if (playerTransform == null)
                return;

            Collider[] colliders = Physics.OverlapSphere(playerTransform.position, searchRadius);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Stone") || collider.CompareTag("Metal"))
                {
                    WaterSource waterSource = collider.GetComponent<WaterSource>();

                    if (waterSource != null)
                    {
                         waterSource.m_CurrentLiquidQuality = LiquidQuality.NonPotable;
                    }
                }
            }
        }
    }
}

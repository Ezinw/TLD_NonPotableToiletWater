using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace NonPotableToiletWater
{
    [HarmonyPatch(typeof(WaterSource), nameof(WaterSource.Update))]
    public class NonPotableToiletWater
    {
        static void Postfix(WaterSource __instance)
        {
            ToiletWaterQuality();
        }

        static void ToiletWaterQuality()
        {
            float searchRadius = 5.0f;

            Transform playerTransform = GameManager.GetPlayerTransform();

            if (playerTransform == null)
                return;

            Collider[] colliders = Physics.OverlapSphere(playerTransform.position, searchRadius);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Stone") ||
                    collider.CompareTag("Metal"))
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

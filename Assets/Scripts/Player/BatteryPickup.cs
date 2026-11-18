
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public float rechargeAmount = 30f;

    private bool collected;

    public void Collect(FlashlightBattery battery)
    {
        if (collected || battery == null)
            return;

        if (battery.currentBattery >= battery.maxBattery)
            return;

        battery.Recharge(rechargeAmount);

        collected = true;
        gameObject.SetActive(false);

        Debug.Log("Batterie eingesammelt! Taschenlampe aufgeladen.");
    }
}

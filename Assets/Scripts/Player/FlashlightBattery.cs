
using UnityEngine;

public class FlashlightBattery : MonoBehaviour
{
    [Header("Taschenlampe")]
    public Light flashlight;

    [Header("Akku")]
    public float maxBattery = 100f;
    public float currentBattery = 100f;
    public float drainPerSecond = 2f;

    [Header("Flackern")]
    public float lowBatteryThreshold = 20f;
    public float flickerInterval = 0.15f;
    public float flickerChance = 0.25f;

    private float flickerTimer;
    private float originalIntensity;
    private bool flickering;

    private void Awake()
    {
        if (flashlight != null)
            originalIntensity = flashlight.intensity;

        currentBattery = Mathf.Clamp(
            currentBattery, 0f, maxBattery);
    }

    private void Update()
    {
        if (flashlight == null || currentBattery <= 0f)
            return;

        if (flashlight.enabled)
        {
            currentBattery -= drainPerSecond * Time.deltaTime;
            currentBattery = Mathf.Clamp(
                currentBattery, 0f, maxBattery);
        }

        if (currentBattery <= 0f)
        {
            flashlight.enabled = false;
            return;
        }

        if (currentBattery <= lowBatteryThreshold &&
            flashlight.enabled)
        {
            Flicker();
        }
    }

    private void Flicker()
    {
        flickerTimer += Time.deltaTime;

        if (flickerTimer < flickerInterval)
            return;

        flickerTimer = 0f;

        if (Random.value < flickerChance)
        {
            flashlight.enabled = false;
            CancelInvoke(nameof(RestoreLight));
            Invoke(nameof(RestoreLight), 0.08f);
        }
    }

    private void RestoreLight()
    {
        if (flashlight != null && currentBattery > 0f)
            flashlight.enabled = true;
    }

    public void Recharge(float amount)
    {
        currentBattery = Mathf.Clamp(
            currentBattery + amount, 0f, maxBattery);
    }

    public float BatteryPercent =>
        maxBattery <= 0f ? 0f :
        currentBattery / maxBattery * 100f;
}

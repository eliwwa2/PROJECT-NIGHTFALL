
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void SaveGame(
        float health,
        float battery,
        Vector3 playerPosition)
    {
        PlayerPrefs.SetFloat("PlayerHealth", health);
        PlayerPrefs.SetFloat("FlashlightBattery", battery);

        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        PlayerPrefs.Save();

        Debug.Log("Spielstand wurde gespeichert!");
    }

    public void LoadGame(
        PlayerHealth health,
        FlashlightBattery battery,
        Transform player)
    {
        if (!PlayerPrefs.HasKey("PlayerHealth"))
        {
            Debug.Log("Kein Spielstand gefunden.");
            return;
        }

        health.currentHealth =
            PlayerPrefs.GetFloat("PlayerHealth");

        battery.currentBattery =
            PlayerPrefs.GetFloat("FlashlightBattery");

        Vector3 position = new Vector3(
            PlayerPrefs.GetFloat("PlayerX"),
            PlayerPrefs.GetFloat("PlayerY"),
            PlayerPrefs.GetFloat("PlayerZ")
        );

        player.position = position;

        Debug.Log("Spielstand wurde geladen!");
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Spielstand wurde gelöscht!");
    }
}

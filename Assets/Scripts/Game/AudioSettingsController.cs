
using UnityEngine;

public class AudioSettingsController : MonoBehaviour
{
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        float savedVolume =
            PlayerPrefs.GetFloat("MasterVolume", 1f);

        AudioListener.volume = savedVolume;
    }
}

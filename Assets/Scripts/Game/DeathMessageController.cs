
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathMessageController : MonoBehaviour
{
    public CanvasGroup messagePanel;
    public Text messageText;

    public float fadeDuration = 2f;

    private string[] messages =
    {
        "ER HAT DICH GEFUNDEN.",
        "DU HÄTTEST NICHT HIER SEIN SOLLEN.",
        "DAS KRANKENHAUS LÄSST DICH NICHT GEHEN.",
        "DEIN LICHT IST ERLOSCHEN."
    };

    public void ShowDeathMessage()
    {
        if (messagePanel == null || messageText == null)
            return;

        int index = Random.Range(0, messages.Length);

        messageText.text = messages[index];

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        messagePanel.gameObject.SetActive(true);
        messagePanel.alpha = 0f;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            messagePanel.alpha = Mathf.Clamp01(
                elapsed / fadeDuration
            );

            yield return null;
        }

        messagePanel.alpha = 1f;
    }
}

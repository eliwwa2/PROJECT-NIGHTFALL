
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveSystem : MonoBehaviour
{
    public Text objectiveText;

    private int currentObjective;

    private readonly string[] objectives =
    {
        "Finde den Krankenhausschlüssel.",
        "Öffne die verschlossene Tür.",
        "Finde einen Weg in den Keller.",
        "Entkomme aus dem Krankenhaus."
    };

    private void Start()
    {
        UpdateObjective();
    }

    public void CompleteObjective()
    {
        if (currentObjective >= objectives.Length)
            return;

        currentObjective++;
        UpdateObjective();
    }

    private void UpdateObjective()
    {
        if (objectiveText == null)
            return;

        if (currentObjective >= objectives.Length)
        {
            objectiveText.text = "Du hast überlebt!";
            return;
        }

        objectiveText.text =
            "Aufgabe: " + objectives[currentObjective];
    }
}

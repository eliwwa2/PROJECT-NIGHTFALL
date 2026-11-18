
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Leben")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead || damage <= 0f)
            return;

        currentHealth = Mathf.Max(0f, currentHealth - damage);

        Debug.Log("Du hast Schaden bekommen! Leben: " + currentHealth);

        if (currentHealth <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        if (IsDead || amount <= 0f)
            return;

        currentHealth = Mathf.Min(
            maxHealth, currentHealth + amount);
    }

    private void Die()
    {
        IsDead = true;

        DeathSequence death = FindFirstObjectByType<DeathSequence>();

        if (death != null)
            death.StartDeath();

        Debug.Log("Du bist gestorben!");
    }
}

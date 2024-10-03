using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Vida : MonoBehaviour
{
    public static Vida Instance { get; private set; }

    [SerializeField] UnityEngine.UI.Image healthBarImage;

    [SerializeField] GameObject diePanel;

    [SerializeField] private float respawnDelay = 3f;

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private Vector3 respawnPosition;

    [SerializeField] Transform posicionJugador;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();  

        diePanel.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }
    private void Update()
    {
        if (posicionJugador.position.y <= -5) { Respawn(); }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (currentHealth / maxHealth) * 100f; 
        healthBarImage.fillAmount = healthPercentage / 100f; 
    }

    private IEnumerator HandleDeath()
    {
        diePanel.SetActive(true);

        yield return new WaitForSeconds(respawnDelay);

        diePanel.SetActive(false);

        Respawn();
    }

    private void Respawn()
    {
        transform.position = respawnPosition;

        currentHealth = maxHealth;

        UpdateHealthBar();
    }
}

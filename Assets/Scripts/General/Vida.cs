using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Vida : MonoBehaviour
{
    // Referencia a la imagen que quieres ajustar
    [SerializeField] UnityEngine.UI.Image healthBarImage;

    [SerializeField] GameObject diePanel;

    [SerializeField] private float respawnDelay = 3f;

    // Vida m�xima y actual del jugador
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    // Posici�n de resurrecci�n
    [SerializeField] private Vector3 respawnPosition;

    private void Start()
    {
        // Inicializar la vida actual al m�ximo al inicio
        currentHealth = maxHealth;
        UpdateHealthBar();  // Actualiza la barra de vida inicialmente

        diePanel.SetActive(false);
    }

    // M�todo para actualizar la vida del jugador
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Aseg�rate de que la vida no sea menor a 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de vida
        UpdateHealthBar();

        // Si la vida es 0 o menor, realizar respawn
        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    // M�todo para curar al jugador
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        // Aseg�rate de que la vida no sea mayor a la vida m�xima
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de vida
        UpdateHealthBar();
    }

    // M�todo para actualizar el porcentaje de llenado de la barra de vida
    private void UpdateHealthBar()
    {
        float healthPercentage = (currentHealth / maxHealth) * 100f; // Convertir a porcentaje
        healthBarImage.fillAmount = healthPercentage / 100f; // Convertir a fillAmount (0-1)
    }

    private IEnumerator HandleDeath()
    {
        // Mostrar el panel de muerte
        diePanel.SetActive(true);

        // Esperar el tiempo definido (respawnDelay) antes de realizar respawn
        yield return new WaitForSeconds(respawnDelay);

        // Ocultar el panel de muerte
        diePanel.SetActive(false);

        // Realizar el respawn
        Respawn();
    }

    // M�todo para mover al jugador a una posici�n espec�fica y restaurar la vida
    private void Respawn()
    {
        // Mover al jugador a la posici�n de respawn
        transform.position = respawnPosition;

        // Restaurar la vida del jugador al m�ximo
        currentHealth = maxHealth;

        // Actualizar la barra de vida
        UpdateHealthBar();
    }
}

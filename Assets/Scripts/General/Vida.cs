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

    // Vida máxima y actual del jugador
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    // Posición de resurrección
    [SerializeField] private Vector3 respawnPosition;

    private void Start()
    {
        // Inicializar la vida actual al máximo al inicio
        currentHealth = maxHealth;
        UpdateHealthBar();  // Actualiza la barra de vida inicialmente

        diePanel.SetActive(false);
    }

    // Método para actualizar la vida del jugador
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Asegúrate de que la vida no sea menor a 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de vida
        UpdateHealthBar();

        // Si la vida es 0 o menor, realizar respawn
        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    // Método para curar al jugador
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        // Asegúrate de que la vida no sea mayor a la vida máxima
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de vida
        UpdateHealthBar();
    }

    // Método para actualizar el porcentaje de llenado de la barra de vida
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

    // Método para mover al jugador a una posición específica y restaurar la vida
    private void Respawn()
    {
        // Mover al jugador a la posición de respawn
        transform.position = respawnPosition;

        // Restaurar la vida del jugador al máximo
        currentHealth = maxHealth;

        // Actualizar la barra de vida
        UpdateHealthBar();
    }
}

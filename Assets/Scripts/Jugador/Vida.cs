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

    public float healAmount = 2f; // Cantidad de curaci�n cada vez que se llama Heal
    public float currentHealth;
    public float maxHealth;

    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] Transform posicionJugador;

    private Coroutine healingCoroutine;

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

        // Iniciar la curaci�n si se presiona la tecla "H" y a�n no se ha iniciado la corrutina
        if (Input.GetKeyDown("h") && healingCoroutine == null)
        {
            healingCoroutine = StartCoroutine(HealOverTime());
        }

        // Detener la curaci�n si se suelta la tecla "H" y la corrutina est� activa
        if (Input.GetKeyUp("h") && healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
        }

        // Para prop�sitos de prueba: causa da�o con la tecla "M"
        if (Input.GetKeyDown("m")) { TakeDamage(50); }
    }

    private IEnumerator HealOverTime()
    {
        while (currentHealth < maxHealth)
        {
            Heal(healAmount);
            yield return new WaitForSeconds(0.1f); // Curaci�n cada 0.1 segundos
        }
        healingCoroutine = null; // Restablecer cuando alcance el l�mite de salud
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
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

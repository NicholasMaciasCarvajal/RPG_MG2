using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject vidaPanel;
    [SerializeField] GameObject pausePanel;

    public static UiManager Instance { get; private set; }

    [SerializeField] UnityEngine.UI.Image healthBarImage;

    public void UpdateI(float percentage)
    {
        percentage = Mathf.Clamp(percentage, 0, 100);

        float fillAmount = percentage / 100f;

        healthBarImage.fillAmount = fillAmount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else if (Time.timeScale == 0)
            {
                Resume();
            }
        }
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        vidaPanel.SetActive(false);

        // Desbloquear el cursor cuando el juego está en pausa
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        vidaPanel.SetActive(true);

        // Bloquear el cursor cuando se reanuda el juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

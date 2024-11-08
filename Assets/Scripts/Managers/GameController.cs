using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameSaveManager saveManager;
    private Vida vida;
    private Levels levels;

    void Start()
    {
        // Obtener la referencia al GameSaveManager, Vida y Levels
        saveManager = FindObjectOfType<GameSaveManager>();
        vida = FindObjectOfType<Vida>();
        levels = FindObjectOfType<Levels>();
    }

    // Método para guardar el juego
    public void SaveGame()
    {
        if (saveManager != null && vida != null && levels != null)
        {
            // Guardar el estado actual del jugador
            saveManager.SaveGame();
            Debug.Log("Juego guardado.");
        }
        else
        {
            Debug.LogWarning("No se pudo guardar el juego. Verifica que todos los componentes estén asignados.");
        }
    }

    // Método para cargar el juego
    public void LoadGame()
    {
        if (saveManager != null && vida != null && levels != null)
        {
            saveManager.LoadGame();
            Debug.Log("Juego cargado con éxito.");
        }
        else
        {
            Debug.LogWarning("No se pudo cargar el juego. Verifica que todos los componentes estén asignados.");
        }
    }

    // Método para iniciar una nueva partida
    public void NewGame()
    {
        if (saveManager != null && vida != null && levels != null)
        {
            saveManager.NewGame();
            Debug.Log("Nueva partida iniciada.");
        }
        else
        {
            Debug.LogWarning("No se pudo iniciar una nueva partida. Verifica que todos los componentes estén asignados.");
        }
    }
}

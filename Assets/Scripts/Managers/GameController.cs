using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameSaveManager saveManager;

    void Start()
    {
        // Obtener la referencia al GameSaveManager
        saveManager = FindObjectOfType<GameSaveManager>();
    }

    // M�todo para guardar el juego
    public void SaveGame()
    {
        GameData currentData = new GameData
        {
            playerLevel = 5, // Ejemplo: nivel actual del jugador
            playerHealth = 80f, // Ejemplo: salud actual del jugador
            playerPosition = new Vector3(1.5f, 0f, 2.3f) // Ejemplo: posici�n actual del jugador
        };

        saveManager.SaveGame(currentData);
        Debug.Log("Juego guardado.");
    }

    // M�todo para cargar el juego
    public void LoadGame()
    {
        GameData loadedData = saveManager.LoadGame();

        if (loadedData != null)
        {
            // Usar los datos cargados para restaurar el estado del juego
            Debug.Log("Nivel del jugador: " + loadedData.playerLevel);
            Debug.Log("Salud del jugador: " + loadedData.playerHealth);
            Debug.Log("Posici�n del jugador: " + loadedData.playerPosition);

            // Aqu� podr�as actualizar el estado real del jugador en el juego
        }
    }
    public void NewGame()
    {
        GameData newGameData = saveManager.NewGame();

        // Configurar el juego para iniciar una nueva partida
        Debug.Log("Nueva partida creada:");
        Debug.Log("Nivel inicial del jugador: " + newGameData.playerLevel);
        Debug.Log("Salud inicial del jugador: " + newGameData.playerHealth);
        Debug.Log("Posici�n inicial del jugador: " + newGameData.playerPosition);

        // Aqu� podr�as inicializar el estado del jugador para la nueva partida
    }
}

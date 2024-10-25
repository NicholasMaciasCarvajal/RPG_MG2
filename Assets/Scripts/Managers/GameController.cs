using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameSaveManager saveManager;

    void Start()
    {
        // Obtener la referencia al GameSaveManager
        saveManager = FindObjectOfType<GameSaveManager>();
    }

    // Método para guardar el juego
    public void SaveGame()
    {
        GameData currentData = new GameData
        {
            playerLevel = 5, // Ejemplo: nivel actual del jugador
            playerHealth = 80f, // Ejemplo: salud actual del jugador
            playerPosition = new Vector3(1.5f, 0f, 2.3f) // Ejemplo: posición actual del jugador
        };

        saveManager.SaveGame(currentData);
        Debug.Log("Juego guardado.");
    }

    // Método para cargar el juego
    public void LoadGame()
    {
        GameData loadedData = saveManager.LoadGame();

        if (loadedData != null)
        {
            // Usar los datos cargados para restaurar el estado del juego
            Debug.Log("Nivel del jugador: " + loadedData.playerLevel);
            Debug.Log("Salud del jugador: " + loadedData.playerHealth);
            Debug.Log("Posición del jugador: " + loadedData.playerPosition);

            // Aquí podrías actualizar el estado real del jugador en el juego
        }
    }
    public void NewGame()
    {
        GameData newGameData = saveManager.NewGame();

        // Configurar el juego para iniciar una nueva partida
        Debug.Log("Nueva partida creada:");
        Debug.Log("Nivel inicial del jugador: " + newGameData.playerLevel);
        Debug.Log("Salud inicial del jugador: " + newGameData.playerHealth);
        Debug.Log("Posición inicial del jugador: " + newGameData.playerPosition);

        // Aquí podrías inicializar el estado del jugador para la nueva partida
    }
}

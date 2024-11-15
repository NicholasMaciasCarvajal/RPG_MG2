using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameSaveManager saveManager;
    [SerializeField] private Vida vida;
    [SerializeField] private Levels levels;

    void Start()
    {
        // Acceso al Singleton de GameSaveManager
        if (saveManager == null)
        {
            saveManager = GameSaveManager.Instance;
            if (saveManager == null)
            {
                Debug.LogWarning("No se encontr� GameSaveManager en la escena o no est� inicializado como Singleton.");
            }
        }

        // Intentar obtener Vida y Levels a trav�s de sus etiquetas
        if (vida == null)
        {
            GameObject vidaObject = GameObject.FindWithTag("Vida");
            if (vidaObject != null)
                vida = vidaObject.GetComponent<Vida>();
            else
                Debug.LogWarning("No se encontr� el objeto Vida en la escena.");
        }

        if (levels == null)
        {
            GameObject levelsObject = GameObject.FindWithTag("Levels");
            if (levelsObject != null)
                levels = levelsObject.GetComponent<Levels>();
            else
                Debug.LogWarning("No se encontr� el objeto Levels en la escena.");
        }

        // Verificaci�n final de referencias
        if (saveManager == null || vida == null || levels == null)
        {
            Debug.LogWarning("No se encontraron todos los componentes necesarios en la escena.");
        }
    }

    // M�todo para guardar el juego
    public void SaveGame()
    {
        if (saveManager != null && vida != null && levels != null)
        {
            saveManager.SaveGame();
            Debug.Log("Juego guardado.");
        }
        else
        {
            Debug.LogWarning("No se pudo guardar el juego. Verifica que todos los componentes est�n asignados.");
        }
    }

    // M�todo para cargar el juego
    public void LoadGame()
    {
        if (saveManager != null && vida != null && levels != null)
        {
            saveManager.LoadGame();
            Debug.Log("Juego cargado con �xito.");
        }
        else
        {
            Debug.LogWarning("No se pudo cargar el juego. Verifica que todos los componentes est�n asignados.");
        }
    }

    // M�todo para iniciar una nueva partida
    public void NewGame()
    {
        if (saveManager != null && vida != null && levels != null)
        {
            saveManager.NewGame();
            Debug.Log("Nueva partida iniciada.");
        }
        else
        {
            Debug.LogWarning("No se pudo iniciar una nueva partida. Verifica que todos los componentes est�n asignados.");
        }
    }
}

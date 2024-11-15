using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int playerLevel;
    public float playerHealth;
    public Vector3 playerPosition;
    public float maxPlayerHealth;
}

public class GameSaveManager : MonoBehaviour
{
    private string saveFilePath;

    public static GameSaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Referencias a otros componentes (se asignan desde el Inspector)
    [SerializeField] private Vida vida;
    [SerializeField] private Levels levels;
    [SerializeField] private Transform playerTransform;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/savegame.dat";

        // Opcional: verificar si las referencias están asignadas para evitar errores
        if (vida == null || levels == null || playerTransform == null)
        {
            Debug.LogWarning("Algunas referencias no están asignadas en el Inspector.");
        }
    }

    // Método para guardar el juego
    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        GameData data = new GameData
        {
            playerLevel = (int)levels.Level,
            playerHealth = vida.currentHealth,
            maxPlayerHealth = vida.maxHealth,
            playerPosition = playerTransform.position
        };

        formatter.Serialize(file, data);
        file.Close();

        Debug.Log("Juego guardado en: " + saveFilePath);
    }

    // Método para cargar el juego
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(file);
            file.Close();

            // Aplicar los datos cargados
            levels.Level = data.playerLevel;
            vida.currentHealth = data.playerHealth;
            vida.maxHealth = data.maxPlayerHealth;
            vida.UpdateHealthBar();
            playerTransform.position = data.playerPosition;

            Debug.Log("Juego cargado de: " + saveFilePath);
        }
        else
        {
            Debug.LogWarning("No se encontró un archivo de guardado.");
        }
    }

    // Método para crear una nueva partida
    public void NewGame()
    {
        levels.Level = 1;
        vida.currentHealth = 100f;
        vida.maxHealth = 100f;
        vida.UpdateHealthBar();
        playerTransform.position = Vector3.zero;

        SceneManager.LoadScene("Game");

        Debug.Log("Nueva partida creada.");
    }
}

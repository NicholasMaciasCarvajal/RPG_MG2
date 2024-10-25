using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerLevel;
    public float playerHealth;
    public Vector3 playerPosition;
}

public class GameSaveManager : MonoBehaviour
{
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/savegame.dat";
    }

    // Método para guardar el juego
    public void SaveGame(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        // Serializar los datos y guardarlos en un archivo
        formatter.Serialize(file, data);
        file.Close();

        Debug.Log("Juego guardado en: " + saveFilePath);
    }

    // Método para cargar el juego
    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);

            // Deserializar los datos del archivo
            GameData data = (GameData)formatter.Deserialize(file);
            file.Close();

            Debug.Log("Juego cargado de: " + saveFilePath);
            return data;
        }
        else
        {
            Debug.LogWarning("No se encontró un archivo de guardado.");
            return null;
        }
    }

    // Método para crear una nueva partida
    public GameData NewGame()
    {
        GameData newData = new GameData
        {
            playerLevel = 1,
            playerHealth = 100f,
            playerPosition = Vector3.zero // Posición inicial
        };

        Debug.Log("Nueva partida creada.");
        return newData;
    }
}

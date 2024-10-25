using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject panelGuardados;
    [SerializeField] private GameObject panelNuevaPartida;

    public void exitGame()
    {
        Application.Quit();
    }

}

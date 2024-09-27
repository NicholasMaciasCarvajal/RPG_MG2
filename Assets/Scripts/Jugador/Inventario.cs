using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject[] espacioInventario;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                panelInventario.SetActive(true);
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                panelInventario.SetActive(false);
            }
        }
    }
    private void inventario()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
    // Referencia a la imagen que quieres ajustar
    [SerializeField] UnityEngine.UI.Image healthBarImage;

    // Método para actualizar el porcentaje de llenado
    public void UpdateI(float percentage)
    {
        // Asegúrate de que el porcentaje esté entre 0 y 100
        percentage = Mathf.Clamp(percentage, 0, 100);

        // Convertir el porcentaje (0-100) a fillAmount (0-1)
        float fillAmount = percentage / 100f;

        // Actualizar el fillAmount de la imagen
        healthBarImage.fillAmount = fillAmount;
    }
}

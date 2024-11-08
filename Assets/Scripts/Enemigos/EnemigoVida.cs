using UnityEngine;

public class EnemigoVida : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;

    public Transform barraDeVidaPlano; // Asigna aquí el plano de la barra de vida en el inspector

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarraDeVida();
    }

    // Función para tomar daño
    public void TomarDanio(float cantidadDanio)
    {
        vidaActual -= cantidadDanio;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima); // Limita la vida entre 0 y vida máxima
        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    // Actualiza la escala del plano en función de la vida actual
    private void ActualizarBarraDeVida()
    {
        if (barraDeVidaPlano != null)
        {
            // Calcula la nueva escala en el eje Z basado en la proporción de vida
            float escalaZ = vidaActual / vidaMaxima;
            barraDeVidaPlano.localScale = new Vector3(barraDeVidaPlano.localScale.x, barraDeVidaPlano.localScale.y, escalaZ);
        }
    }

    private void Morir()
    {
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }
}

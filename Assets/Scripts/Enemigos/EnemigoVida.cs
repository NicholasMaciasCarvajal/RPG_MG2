using UnityEngine;

public class EnemigoVida : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;
    public Transform barraDeVidaPlano;

    private Levels levelsScript;

    public AudioClip sonidoMuerte; // Clip de sonido a reproducir cuando el enemigo muera

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarraDeVida();

        // Buscar el objeto que tiene el script Levels (por ejemplo, el jugador o un controlador)
        levelsScript = FindObjectOfType<Levels>();
    }

    public void TomarDanio(float cantidadDanio)
    {
        vidaActual -= cantidadDanio;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void ActualizarBarraDeVida()
    {
        if (barraDeVidaPlano != null)
        {
            float escalaZ = vidaActual / vidaMaxima;
            barraDeVidaPlano.localScale = new Vector3(barraDeVidaPlano.localScale.x, barraDeVidaPlano.localScale.y, escalaZ);
        }
    }

    private void Morir()
    {
        Debug.Log("El enemigo ha muerto.");

        // Reproducir el sonido de muerte en un objeto separado
        if (sonidoMuerte != null)
        {
            GameObject objetoSonido = new GameObject("SonidoMuerte");
            AudioSource audioSource = objetoSonido.AddComponent<AudioSource>();
            audioSource.clip = sonidoMuerte;
            audioSource.Play();

            // Destruir el objeto de sonido una vez que el clip haya terminado
            Destroy(objetoSonido, sonidoMuerte.length);
        }

        // Llamar al método de Levels para incrementar kills
        if (levelsScript != null)
        {
            levelsScript.AumentarKills();
        }

        // Destruir el enemigo de inmediato
        Destroy(gameObject);
    }
}


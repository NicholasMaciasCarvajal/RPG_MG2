using System.Collections;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    public GameObject posicionOriginal;
    public GameObject posicionAtaque1;
    public GameObject posicionAtaque2;

    private GameObject posicionActual;
    private bool alternarAtaque;
    private float tiempoUltimoClick;
    public float tiempoInactividad = 2.0f; // Tiempo en segundos antes de volver a la posición original
    public string enemigoTag = "Enemigo"; // Tag del enemigo
    public float anguloAtaque = 120f; // Ángulo en grados para permitir el ataque
    public float distanciaAtaque = 5f; // Distancia máxima para que el ataque sea válido
    public float daño = 20f; // Daño a aplicar al enemigo

    void Start()
    {
        posicionActual = posicionOriginal; // Iniciar en la posición original
        ActivarPosicion(posicionOriginal);
    }

    void Update()
    {
        // Detectar clic derecho o izquierdo
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            tiempoUltimoClick = Time.time; // Actualizar el tiempo del último clic

            // Alternar entre las posiciones de ataque
            if (alternarAtaque)
            {
                ActivarPosicion(posicionAtaque2);
            }
            else
            {
                ActivarPosicion(posicionAtaque1);
            }

            alternarAtaque = !alternarAtaque; // Cambiar el estado para alternar en el siguiente clic

            // Buscar y dañar enemigos dentro del rango
            DañarEnemigosEnRango();
        }

        // Volver a la posición original después de 2 segundos de inactividad
        if (Time.time - tiempoUltimoClick > tiempoInactividad && posicionActual != posicionOriginal)
        {
            ActivarPosicion(posicionOriginal);
            alternarAtaque = false; // Reiniciar el estado de alternancia
        }
    }

    void ActivarPosicion(GameObject nuevaPosicion)
    {
        posicionOriginal.SetActive(false);
        posicionAtaque1.SetActive(false);
        posicionAtaque2.SetActive(false);

        nuevaPosicion.SetActive(true);
        posicionActual = nuevaPosicion; // Actualizar la posición actual
    }

    void DañarEnemigosEnRango()
    {
        // Encontrar todos los objetos con el tag de enemigo
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(enemigoTag);

        foreach (GameObject enemigo in enemigos)
        {
            Vector3 direccionAlEnemigo = enemigo.transform.position - transform.position;
            float distanciaAlEnemigo = direccionAlEnemigo.magnitude;

            // Verificar que el enemigo esté dentro de la distancia de ataque
            if (distanciaAlEnemigo > distanciaAtaque) continue;

            // Calcular el ángulo entre la dirección del jugador y la dirección al enemigo
            float angulo = Vector3.Angle(transform.forward, direccionAlEnemigo);

            // Verificar si el ángulo está dentro del rango permitido
            if (angulo <= anguloAtaque / 2)
            {
                // Intentar obtener el componente EnemigoVida del enemigo y aplicarle daño
                EnemigoVida enemigoVida = enemigo.GetComponent<EnemigoVida>();
                if (enemigoVida != null)
                {
                    enemigoVida.TomarDanio(daño);
                }
            }
        }
    }
}

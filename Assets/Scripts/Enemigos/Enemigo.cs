using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public Transform target;

    public float followSpeed = 5f;
    public float stoppingDistance = 0.5f;
    public float followRadius = 10f;

    private Vida playerVida;
    public float damageAmount = 10f; // Cantidad de daño que inflige el enemigo

    public float attackCooldown = 2f; // Tiempo de espera entre ataques en segundos
    private float nextAttackTime = 0f; // Marca de tiempo para el próximo ataque permitido

    private float fixedYPosition; // Posición fija en el eje y

    private void Start()
    {
        // Encuentra al jugador y obtiene su componente Vida
        target = GameObject.Find("Jugador").transform;
        if (target != null)
        {
            playerVida = target.GetComponent<Vida>();
        }

        // Guardar la posición inicial en el eje y
        fixedYPosition = transform.position.y;
    }

    void Update()
    {
        // Si no hay objetivo o componente Vida, salir del método
        if (target == null || playerVida == null)
            return;

        // Calcular la distancia al objetivo
        float distance = Vector3.Distance(transform.position, target.position);

        // Verificar si el objetivo está dentro del radio de seguimiento
        if (distance < followRadius && distance > stoppingDistance)
        {
            // Interpolar suavemente hacia la posición del objetivo en el plano x-z
            Vector3 targetPosition = new Vector3(target.position.x, fixedYPosition, target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Rotar hacia el objetivo en el eje y únicamente
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // Elimina cualquier rotación en el eje x y z

            if (direction != Vector3.zero) // Asegura que la dirección no sea un vector cero
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, followSpeed * Time.deltaTime);
            }
        }
        // Si el enemigo está a la distancia de parada y ha pasado el tiempo de cooldown, inflige daño al jugador
        else if (distance <= stoppingDistance && Time.time >= nextAttackTime)
        {
            playerVida.TakeDamage(damageAmount);
            nextAttackTime = Time.time + attackCooldown; // Actualizar el tiempo para el próximo ataque
        }
    }
}
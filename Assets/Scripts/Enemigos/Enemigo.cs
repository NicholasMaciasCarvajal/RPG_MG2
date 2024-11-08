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

    private void Start()
    {
        // Encuentra al jugador y obtiene su componente Vida
        target = GameObject.Find("Jugador").transform;
        if (target != null)
        {
            playerVida = target.GetComponent<Vida>();
        }
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
            // Interpolar suavemente hacia la posición del objetivo
            transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

            // Rotar hacia el objetivo
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, followSpeed * Time.deltaTime);
        }
        // Si el enemigo está a la distancia de parada y ha pasado el tiempo de cooldown, inflige daño al jugador
        else if (distance <= stoppingDistance && Time.time >= nextAttackTime)
        {
            playerVida.TakeDamage(damageAmount);
            nextAttackTime = Time.time + attackCooldown; // Actualizar el tiempo para el próximo ataque
        }
    }
}


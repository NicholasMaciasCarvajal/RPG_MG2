using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad;
    public NavMeshAgent enemigo;

    private void Update()
    {
        enemigo.speed = velocidad;
        enemigo.SetDestination(objetivo.position);
    }
}

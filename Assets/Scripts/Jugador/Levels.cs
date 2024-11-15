using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public float Level;
    private float multiVelocidad = 0.5f;
    private float multiVida = 0.5f;
    private float multiRegVida = 0.5f;
    public int cantidadKill;

    void Multiplicadores()
    {
        multiVida = Level * multiVida;
        multiVelocidad = Level * multiVelocidad;
        multiRegVida = Level * multiRegVida;
    }

    private void Update()
    {
        SubidadeNivel();
        Multiplicadores();
        SubidaStats();
    }

    private void Start()
    {
        SubidadeNivel();
        Multiplicadores();
        SubidaStats();
    }

    void SubidaStats()
    {
        GetComponent<Vida>().maxHealth = (GetComponent<Vida>().maxHealth * multiVida) + GetComponent<Vida>().maxHealth;
        GetComponent<Vida>().healAmount = (GetComponent<Vida>().healAmount * multiRegVida) + GetComponent<Vida>().healAmount;
        GetComponent<MovimientoJugador>().velocidadMovimiento = (GetComponent<MovimientoJugador>().velocidadMovimiento * multiVelocidad) + GetComponent<MovimientoJugador>().velocidadMovimiento;
    }

    void SubidadeNivel()
    {
        Level = cantidadKill / 25;
    }

    public void AumentarKills()
    {
        cantidadKill++;
        SubidadeNivel();
        Multiplicadores();
        SubidaStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            AumentarKills();
            Destroy(other.gameObject); // Destruye el objeto enemigo
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public float Level;
    public float multiVelocidad = 0.05f;
    public float multiVida = 0.05f;
    public float multiRegVida = 0.05f;
    public int cantidadKill;

    void Multiplicadores()
    {
        multiVida = Level * multiVida;
        multiVelocidad = Level * multiVelocidad;
        multiRegVida = Level * multiRegVida;
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
        Level = cantidadKill % 25;
    }
}
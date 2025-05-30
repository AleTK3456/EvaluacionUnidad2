using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public Transform objetivo;         // El jugador (Chan)
    public float velocidad = 3.0f;     // Velocidad de movimiento
    public float distanciaMinima = 1.5f; // Para que no se le pegue completamente

    private bool haMatado = false;
    void Update()
    {
        if (GameManager.instancia != null && GameManager.instancia.jugadorMurio)
            return;

        if (objetivo == null) return;

        // Dirección desde el enemigo hacia el jugador
        Vector3 direccion = objetivo.position - transform.position;
        direccion.y = 0f; // opcional: mantenerlo en el mismo nivel

        // Si está muy cerca, no moverse más
        if (direccion.magnitude < distanciaMinima) return;

        // Mover hacia el jugador
        Vector3 movimiento = direccion.normalized * velocidad * Time.deltaTime;
        transform.position += movimiento;

        // Hacer que el enemigo mire hacia el jugador
        transform.LookAt(objetivo);

    }
    void OnTriggerEnter(Collider other)
    {
        if (haMatado) return;

        if (other.CompareTag("Player"))
        {
            ControlPlayer control = other.GetComponent<ControlPlayer>();
            if (control != null && !control.estaMuerto)
            {
                control.Morir();
                haMatado = true;
            }
        }
    }
}

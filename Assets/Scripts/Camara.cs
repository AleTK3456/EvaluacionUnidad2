using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform target;         // A quién seguir (Chan)
    public float distance = 5f;      // Distancia detrás del jugador
    public float height = 2f;        // Altura sobre el jugador
    public float velocidadGiro = 10f; // (opcional) velocidad de giro automático

    private Vector3 offset;

    void Start()
    {
        // Offset inicial relativo al jugador
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        // Calcula la posición deseada directamente
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Posiciona directamente sin suavizado
        transform.position = desiredPosition;

        // Mira siempre al personaje
        transform.LookAt(target);

        // (opcional) rotación automática alrededor
        // transform.RotateAround(target.position, Vector3.up, velocidadGiro * Time.deltaTime);
    }
}

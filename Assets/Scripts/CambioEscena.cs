using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscenaSiguiente;
    private float tiempoTranscurrido = 0f;
    public float tiempoEspera = 20f;

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido >= tiempoEspera || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
    }
}

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemigoCielo : MonoBehaviour
{
    public Transform objetivo;
    public float distanciaAlerta = 10f;

    private NavMeshAgent agent;
    private bool estaMuerto = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (estaMuerto) return;

        float distancia = Vector3.Distance(transform.position, objetivo.position);

        if (distancia <= distanciaAlerta)
        {
            agent.SetDestination(objetivo.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (estaMuerto) return;

        Debug.Log("Colision detectada con: " + other.name);

        if (other.CompareTag("Player"))
        {
            estaMuerto = true;
            agent.isStopped = true; // Detener al monstruo

            // Detener futuras colisiones
            GetComponent<Collider>().enabled = false;

            // Llamar al método de desplome
            ControlChanCielo control = other.GetComponent<ControlChanCielo>();
            if (control != null)
            {
                control.Desplomar();
                Debug.Log("Chan se desploma");
            }

            // Esperar 2 segundos antes de recargar la escena
            StartCoroutine(ReiniciarDespuesDeDesplome(2f));
        }
    }

    IEnumerator ReiniciarDespuesDeDesplome(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Cielo");
    }
}

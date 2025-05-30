using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorColision : MonoBehaviour
{
    public GameObject canvasMuerte; 
    private ControlPlayer control;

    void Start()
    {
        control = GetComponent<ControlPlayer>();

        if (canvasMuerte != null)
        {
            canvasMuerte.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            if (control != null && !control.estaMuerto)
            {
                control.Morir();

                if (canvasMuerte != null)
                {
                    canvasMuerte.SetActive(true);
                }
            }
        }
    }
}

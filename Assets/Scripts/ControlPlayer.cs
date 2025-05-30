using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPlayer : MonoBehaviour
{
    float speed = 5.0f;
    float angle = 2.5f;

    public Animator animator;
    public TextMeshProUGUI modoTexto;
    public Transform modeloVisual;
    public GameObject canvasMuerte; // Asignar en Inspector

    enum ModoControl { Teclado, Mouse }
    ModoControl modoActual = ModoControl.Teclado;

    public bool estaMuerto = false;

    void Start()
    {
        ActualizarTextoModo();
        if (canvasMuerte != null)
            canvasMuerte.SetActive(false);
    }

    void Update()
    {
        if (estaMuerto) return;

        if (Input.GetKeyDown(KeyCode.V))
        {
            modoActual = (modoActual == ModoControl.Teclado) ? ModoControl.Mouse : ModoControl.Teclado;
            ActualizarTextoModo();
        }

        if (modoActual == ModoControl.Teclado)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.Rotate(0, horizontal * angle, 0, Space.Self);
            Vector3 moveDirection = new Vector3(0, 0, vertical);
            moveDirection = transform.rotation * moveDirection;
            transform.position += moveDirection * speed * Time.deltaTime;

            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");

            transform.Rotate(0, mouseX * angle * 10f, 0, Space.Self);
            transform.position += transform.forward * speed * Time.deltaTime;

            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 1);
        }
    }

    void ActualizarTextoModo()
    {
        if (modoTexto != null)
        {
            modoTexto.text = "Modo actual: " + (modoActual == ModoControl.Teclado ? "Teclado (WASD)" : "Mouse");
        }
    }

    public void Morir()
    {
        if (estaMuerto) return;
        estaMuerto = true;
        Debug.Log("Morir llamado desde: " + gameObject.name);

        GameManager.instancia.jugadorMurio = true;

        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);

        StartCoroutine(CaerYMostrarCanvas());
    }

    IEnumerator CaerYMostrarCanvas()
    {
        Quaternion rotInicial = transform.rotation;
        Quaternion rotFinal = Quaternion.Euler(90f, transform.eulerAngles.y, transform.eulerAngles.z);

        float duracion = 1f;
        float tiempo = 0f;
        
        while (tiempo < duracion)
        {
            transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.rotation = rotFinal;

        yield return new WaitForSeconds(1f);
        canvasMuerte.SetActive(true);
        Debug.Log("Mostrando canvas de muerte");
    }

    void OnTriggerEnter(Collider other)
    {
        if (estaMuerto) return;

        if (other.CompareTag("Enemigo"))
        {
            Morir();
        }
    }

    // Estos métodos serán llamados por los botones del Canvas
    public void IrAlCielo()
    {
        SceneManager.LoadScene("Cielo"); 
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuScene"); 
    }
}

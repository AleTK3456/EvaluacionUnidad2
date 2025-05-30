using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPlayer : MonoBehaviour
{
    public int vidas = 1;

    float speed = 4.5f;
    float angle = 2.5f;
    public bool muerto = false;

    public Animator animator;
    public TextMeshProUGUI modoTexto;
    public Transform modeloVisual;

    enum ModoControl { Teclado, Mouse }
    ModoControl modoActual = ModoControl.Teclado;


    void Start()
    {

    }

    void Update()
    {
        if (!muerto)
        {
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
            }

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

    public void Tomar_damage(int daño)
    {
        vidas -= daño;

        if (vidas <= 0)
        {
            Muerto();
        }
    }

    public void Muerto()
    {
        muerto = true;
    }
}
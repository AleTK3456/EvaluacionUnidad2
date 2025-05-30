using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControlChanCielo : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 180f;
    public Animator animator;

    private CharacterController controller;
    private bool estaMuerto = false;
    private Quaternion rotacionOriginal;
    private Quaternion rotacionDesplome;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rotacionOriginal = transform.rotation;
    }

    void Update()
    {
        if (estaMuerto) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        animator.SetFloat("Speed", Mathf.Abs(vertical));

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);
            controller.Move(transform.forward * speed * Time.deltaTime);
            animator.SetFloat("Speed", 1f);
        }
    }

    // 🔻 Se desploma (muere)
    public void Desplomar()
    {
        if (estaMuerto) return;  
        StartCoroutine(AnimacionDesplome());
    }
    IEnumerator AnimacionDesplome()
    {
        estaMuerto = true;
        animator.SetFloat("Speed", 0f);

        float duracion = 1f;
        float tiempo = 0f;

        Quaternion rotInicial = transform.rotation;
        Quaternion rotFinal = Quaternion.Euler(90f, transform.eulerAngles.y, 0f);

        while (tiempo < duracion)
        {
            transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.rotation = rotFinal;
        rotacionDesplome = rotFinal;  

        this.enabled = false; 

        Debug.Log("Chan se desploma");
    }

    public void Revivir()
    {
        estaMuerto = false;
        this.enabled = true;  

        animator.ResetTrigger("Desplome");
        animator.SetFloat("Speed", 0f);

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f); 

        Debug.Log("Chan revive");
    }
}

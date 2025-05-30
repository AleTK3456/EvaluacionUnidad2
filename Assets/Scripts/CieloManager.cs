using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;
using UnityEngine.SceneManagement;

public class CieloManager : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    public TextMeshProUGUI modoTexto;
    public ControlChanCielo jugador;

    private float tiempoRestante = 45f;
    private bool estaContando = false;

    private bool modoTeclado = true;

    void Start()
    {
        contadorTexto.gameObject.SetActive(true);
        modoTexto.gameObject.SetActive(true);
        ActualizarTextoModo();
        StartCoroutine(ConteoParaRevivir());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            modoTeclado = !modoTeclado;
            ActualizarTextoModo();

            if (jugador != null)
            {
                //jugador.CambiarModo(modoTeclado);
            }
        }
    }

    void ActualizarTextoModo()
    {
        if (modoTexto != null)
        {
            modoTexto.text = "Modo actual: " + (modoTeclado ? "Teclado" : "Mouse");
        }
    }

    IEnumerator ConteoParaRevivir()
    {
        while (tiempoRestante > 0)
        {
            contadorTexto.text = "Revivir en: " + Mathf.CeilToInt(tiempoRestante).ToString() + "s";
            yield return new WaitForSeconds(1f);
            tiempoRestante--;
        }

        contadorTexto.text = "¡Reviviendo!";
        yield return new WaitForSeconds(1f);

        contadorTexto.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Escena");
    }
}

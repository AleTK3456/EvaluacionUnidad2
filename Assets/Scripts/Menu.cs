using UnityEngine;
using UnityEngine.SceneManagement;  // Importante para manejar escenas

public class Menu : MonoBehaviour
{
    // Nombre de la escena que quieres cargar al presionar Jugar
    public string nombreEscenaJuego;

    // Método público para asignar al botón
    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }
}
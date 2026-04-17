using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PruebaIniciarScript : MonoBehaviour
{
   
    public Button iniciarButton;

    void Start()
    {
        // Si asignas un botón en el Inspector, funcionará al inicio
        if (iniciarButton != null)
            iniciarButton.onClick.AddListener(empezar);
    }

    void empezar()
    {
        SceneManager.LoadScene(1); // Carga el nivel
    }

    // FUNCIÓN PARA EL BOTÓN DE "IR AL FINAL"
    public void irAlFinal()
    {
        Time.timeScale = 1f; // Resetea el tiempo para que la nueva escena no esté pausada
        SceneManager.LoadScene("final"); // Nombre exacto de la escena en Build Settings
    }
}

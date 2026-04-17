using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Añade esto arriba

public class PruebaTiempo : MonoBehaviour
{
    public float tiempo = 60f;
    public Image relog;
    public TextMeshProUGUI puntuacion;
    public GameObject botonFinal;

    void Update()
    {
        if (tiempo > 0f)
        {
            tiempo -= Time.deltaTime;
            puntuacion.text = (6000 - (int)(tiempo * 100)).ToString();
            relog.fillAmount = tiempo / 60f;
        }
        else
        {
            FinalizarEscena();
        }
    }

    void FinalizarEscena()
    {
        tiempo = 0;
        Time.timeScale = 0f;
        if (botonFinal != null && !botonFinal.activeSelf)
        {
            botonFinal.SetActive(true);
        }
    }

    // --- AÑADE ESTO PARA EL EXAMEN ---
    public void CargarEscenaFinal()
    {
        Time.timeScale = 1f; // Resetea el tiempo
        SceneManager.LoadScene("final"); // Asegúrate que esté en el Build Settings
    }
}
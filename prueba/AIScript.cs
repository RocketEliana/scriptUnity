using UnityEngine;
using TMPro; // Asegúrate de tener esto para usar TextMeshPro

public class AIScript : MonoBehaviour
{
    public GameObject prefab;
    public TextMeshProUGUI textoUI; // Arrastra aquí tu texto del Canvas

    private float tiempoInicio = 0f;
    private float tiempoAparicion;
    private float tiempoTranscurridoTotal = 0f;

    void Start()
    {
        // Primer rango de aparición (lento)
        tiempoAparicion = Random.Range(1f, 5f);
    }

    void Update()
    {
        // 1. Contador de tiempo total de la partida
        tiempoTranscurridoTotal += Time.deltaTime;

        // 2. Actualizamos el texto del Canvas (quitamos decimales con "0")
        textoUI.text = "Tiempo: " + tiempoTranscurridoTotal.ToString("0");

        // 3. Lógica de instanciación
        tiempoInicio += Time.deltaTime;

        if (tiempoInicio >= tiempoAparicion)
        {
            Instantiate(prefab, new Vector3(10, Random.Range(-4.5f, 4.5f), 0), Quaternion.identity);
            tiempoInicio = 0f;

            // 4. CAMBIO DE DIFICULTAD
            // Si han pasado más de 120 segundos (2 minutos)
            if (tiempoTranscurridoTotal > 30f)
            {
                // Mucho más rápido
                tiempoAparicion = Random.Range(0.5f, 1.5f);
            }
            else
            {
                // Ritmo normal
                tiempoAparicion = Random.Range(1f, 5f);
            }
        }
    }
}

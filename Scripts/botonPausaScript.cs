using UnityEngine;
using UnityEngine.UI;

public class botonPausaScript : MonoBehaviour
{
    public Button botonPausa;
    public bool parado = false;
    public Animator animacionMeteorito;

    void Start()
    {
        botonPausa.onClick.AddListener(() =>
        {
            if (!parado)
            {
                Pausar();
            }
            else
            {
                Reanudar();
            }
        });
    }

    void Pausar()
    {
        Time.timeScale = 0f;
        parado = true;

        // --- PAUSAR TODO EL AUDIO ---
        AudioListener.pause = true;

        if (animacionMeteorito != null) animacionMeteorito.enabled = false;
        Debug.Log("Juego Pausado y Sonido Silenciado");
    }

    void Reanudar()
    {
        Time.timeScale = 1f;
        parado = false;

        // --- REANUDAR TODO EL AUDIO ---
        AudioListener.pause = false;

        if (animacionMeteorito != null) animacionMeteorito.enabled = true;
        Debug.Log("Juego Reanudado y Sonido Activado");
    }
}
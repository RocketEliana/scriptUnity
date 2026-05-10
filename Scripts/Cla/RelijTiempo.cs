using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelijTiempo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float tiempo = 50f;
    public Image relog;
    public TextMeshProUGUI puntuacion;
    public DeepMeteo meteorito;
    public Image cartel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cartel.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        puntuacion.text = (5000 - (int)(tiempo * 100)).ToString();
        if (tiempo < 0f)
        {
            cartel.enabled = true;
            meteorito.parar();

            Time.timeScale = 0f;

        }
        relog.fillAmount = tiempo / 50f;

    }
}

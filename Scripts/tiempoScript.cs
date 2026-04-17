using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tiempoScript : MonoBehaviour
{
    public float tiempo = 60f;
    public Image relog;
    public TextMeshProUGUI puntuacion;
    public meteoritoScript meteorito;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        puntuacion.text = (6000 - (int)(tiempo * 100)).ToString();
        if (tiempo < 0f)
        {
            meteorito.parar();

            Time.timeScale = 0f;

        }
        relog.fillAmount = tiempo / 60f;

    }
}

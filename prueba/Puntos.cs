using TMPro;
using UnityEngine;

public class Puntos : MonoBehaviour
{
     public TextMeshProUGUI puntuacion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
     puntuacion.text= "0";
    }

    // Update is called once per frame
    public void Sumar(int puntosSumar)
    {
        int puntosActuales = int.Parse(puntuacion.text);
        puntosActuales += puntosSumar;
        puntuacion.text = puntosActuales.ToString();

    }
    public int GetPuntos()
    {
        return int.Parse(puntuacion.text);
    }

}

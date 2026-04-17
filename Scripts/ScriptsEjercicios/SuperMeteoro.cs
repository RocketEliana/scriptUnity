using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SuperMeteoro : MonoBehaviour
{
    public Transform punto;
    public GameObject miniMeteoror;
    private float tiempo = 0;
    private float rango;
    private Vector3 destinoInicial;
    private Vector3 posicionInicial;
    private bool faseUno = false;
    private bool volviendo = false;
    private float speedInicial = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Instanciar());
        rango = Random.Range(2f, 8f);
        destinoInicial = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
        //Cálculo de la Z en ViewportToWorldPoint: Si le pasas transform.position.z y tu objeto está en $Z=0$ mientras la cámara está en $Z=-10$, la distancia es 10.
        //Si le pasas 0, el punto calculado estará encima de la cámara y no lo verás. He forzado la Z para que sea coherente.
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (!faseUno)
        {

            transform.position = Vector3.MoveTowards(transform.position, destinoInicial, speedInicial * Time.deltaTime);
            if (transform.position == destinoInicial) { faseUno = true; volviendo = true; }
            //transform.position == destinoInicial es mucho menos exacto pero vecto3.distance..<0.1f no funciona xDDD
        }
        else if (volviendo)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial, 2 * Time.deltaTime);
            if (transform.position == posicionInicial) { volviendo = false; }
        }
        //Así, cuando llega al destino, NO ejecuta el movimiento de vuelta hasta el próximo frame por eso el else if,para que no parpadee
    }
    IEnumerator Instanciar()//hago la corrutina por que lo lanza el hijo ,si no parpadea al tener que referenciar posiciones
    {
        while (true)//siempre instancio
        {
            yield return new WaitForSeconds(5f);
            Instantiate(miniMeteoror, transform.position, Quaternion.identity);
        }


    }
}
/*
 * =====================================================================
 * TABLA VIEWPORT (X, Y) - Coordenadas de pantalla en Unity 2D
 * =====================================================================
 * 
 * Te dicen...                      Es (X, Y) en Viewport
 * ─────────────────────────────────────────────────────────────────
 * Mitad de pantalla                (0.5, 0.5)
 * Un cuarto desde izquierda        (0.25, 0.5)
 * Tres cuartos desde izquierda     (0.75, 0.5)
 * Borde izquierdo                  (0, 0.5)
 * Borde derecho                    (1, 0.5)
 * Parte de arriba (centrado)       (0.5, 0.75)
 * Parte de abajo (centrado)        (0.5, 0.25)
 * Esquina arriba-izquierda         (0, 1)
 * Esquina arriba-derecha           (1, 1)
 * Esquina abajo-izquierda          (0, 0)
 * Esquina abajo-derecha            (1, 0)
 * 
 * =====================================================================
 * USO: Camera.main.ViewportToWorldPoint(new Vector3(x, y, 10f));
 * =====================================================================
 */


using System.Collections;
using UnityEngine;

public class EnemigoE2 : MonoBehaviour
{
    private float velocidad = 2;
    private Vector3 mitadPantalla;
    private Vector3 limitePantalla;
    private enum Estado { irCentro, volver, disparar, localizarFase1, localizarFase2, volverFase2, irFinal }
    private Estado actual = Estado.irCentro;
    private bool disparando = false;
    public Transform puntDisparo;
    public GameObject prefabDisparo;
    private float time = 0;
    private Vector3 posicionMeteoro;
    private Vector3 posicionCuartoPantallla;
    private Vector3 escalaOriginal;

    void Start()
    {
        // Corregido: Limites dentro de la pantalla (ajusta según tu cámara)
        mitadPantalla = new Vector3(0f, Random.Range(-4f, 4f), 0f);
        limitePantalla = new Vector3(8.5f, Random.Range(-4f, 4f), 0f);
        posicionCuartoPantallla = new Vector3(-4.5f, transform.position.y, 0f);
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        switch (actual)
        {
            case Estado.irCentro:
                transform.position = Vector3.MoveTowards(transform.position, mitadPantalla, velocidad * Time.deltaTime);
                if (Vector3.Distance(transform.position, mitadPantalla) < 0.1) { actual = Estado.volver; }
                break;

            case Estado.volver:
                transform.position = Vector3.MoveTowards(transform.position, limitePantalla, velocidad * Time.deltaTime);
                if (Vector3.Distance(transform.position, limitePantalla) < 0.1f)
                {
                    actual = Estado.disparar;
                    disparando = true;
                    StartCoroutine(disparar());
                }
                break;

            case Estado.disparar:
                // 3.b: Se mueve entre mitad y limite durante 10s
                time += Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, mitadPantalla, velocidad * Time.deltaTime);
                if (Vector3.Distance(transform.position, mitadPantalla) < 0.1f)
                {
                    // Refresca destinos para que no se quede quieto
                    mitadPantalla.y = Random.Range(-4f, 4f);
                    limitePantalla.y = Random.Range(-4f, 4f);
                    actual = Estado.volver;
                }

                if (time >= 10f) { disparando = false; actual = Estado.localizarFase1; time = 0f; }
                break;

            case Estado.localizarFase1:
                posicionMeteoro = Localizar("Player", 20f);
                // 3.c: Se lanza a por él (Y del jugador) hasta X=0
                Vector3 destinoF1 = new Vector3(0, posicionMeteoro.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, destinoF1, velocidad * Time.deltaTime);

                if (transform.position.x <= 0.1f) { actual = Estado.localizarFase2; }
                break;

            case Estado.localizarFase2:
                posicionMeteoro = Localizar("Player", 20f);
                // 3.c: Vuelve a localizar y va al cuarto de pantalla
                Vector3 destinoF2 = new Vector3(posicionCuartoPantallla.x, posicionMeteoro.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, destinoF2, velocidad * Time.deltaTime);

                if (transform.position.x <= posicionCuartoPantallla.x + 0.1f) { actual = Estado.volverFase2; }
                break;

            case Estado.volverFase2:
                // 3.c: Volver a la derecha a velocidad 2X
                transform.position = Vector3.MoveTowards(transform.position, limitePantalla, velocidad * 2 * Time.deltaTime);

                if (Vector3.Distance(transform.position, limitePantalla) < 0.1f)
                {
                    // 3.d: Crear abanico y lanzarse
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3 offset = new Vector3(0, (i - 1.5f) * 1.5f, 0);
                        GameObject clon = Instantiate(gameObject, transform.position + offset, Quaternion.identity);
                        // CORRECCIÓN CRÍTICA: El clon no debe ejecutar este script desde el principio
                        EnemigoE2 scriptClon = clon.GetComponent<EnemigoE2>();
                        scriptClon.actual = Estado.irFinal;
                    }
                    Destroy(gameObject);
                }
                break;

            case Estado.irFinal:
                // 3.d: Tamaño doble y embestida recta
                transform.localScale = escalaOriginal * 2f;
                transform.Translate(Vector3.left * velocidad * 2 * Time.deltaTime);
                if (transform.position.x < -15f) Destroy(gameObject);
                break;
        }
    }

    IEnumerator disparar()
    {
        while (disparando)
        {
            Instantiate(prefabDisparo, puntDisparo.position, Quaternion.identity);
            // 3.c: En fases de localización dispara cada 1s, si no cada 2s
            float espera = (actual == Estado.localizarFase1 || actual == Estado.localizarFase2) ? 1f : 2f;
            yield return new WaitForSeconds(espera);
        }
    }

    Vector3 Localizar(string tipo, float radio)
    {
        Collider2D objeto = Physics2D.OverlapCircle(transform.position, radio);
        if (objeto != null && objeto.CompareTag(tipo)) return objeto.transform.position;
        return Vector3.zero;
    }
}
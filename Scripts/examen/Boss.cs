using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float velocidad = 3f;

    public GameObject prefabProyectil;
    public Transform ptoDisparo;

    private enum Estados
    {
        aparecer,
        limiteMitad,
        localizarMitad,
        cambioCuarto,
        vuelta
    }

    private Estados actual = Estados.aparecer;

    private bool disparando = false;

    private Vector3 mitadPantalla = Vector3.zero;

    private Vector3 player;

    private float time = 0f;

    private Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
    }

    void Update()
    {
        switch (actual)
        {
            // =========================
            // APARECER
            // =========================
            case Estados.aparecer:

                time += Time.deltaTime;

                // Espera 5 segundos
                if (time >= 5f)
                {
                    time = 0f;

                    // Aparece en una altura aleatoria
                    transform.position = new Vector3(
                        transform.position.x,
                        Random.Range(-4.5f, 4.5f),
                        transform.position.z
                    );

                    actual = Estados.limiteMitad;
                }

                break;

            // =========================
            // IR A LA MITAD
            // =========================
            case Estados.limiteMitad:

                time += Time.deltaTime;

                Vector3 destinoMitad = new Vector3(
                    0f,
                    transform.position.y,
                    transform.position.z
                );

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    destinoMitad,
                    velocidad * Time.deltaTime
                );

                // Disparo continuo
                if (!disparando)
                {
                    disparando = true;
                    StartCoroutine(disparar());
                }

                // Tras 10 segundos cambia de estado
                if (time >= 10f)
                {
                    time = 0f;

                    disparando = false;

                    actual = Estados.localizarMitad;
                }

                break;

            // =========================
            // PERSEGUIR PLAYER
            // =========================
            case Estados.localizarMitad:

                player = Localizar("Player", 25f);

                if (player != Vector3.zero)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        player,
                        velocidad * Time.deltaTime
                    );

                    // Si llega suficientemente cerca
                    if (Vector3.Distance(transform.position, player) < 0.3f)
                    {
                        actual = Estados.cambioCuarto;
                    }
                }

                break;

            // =========================
            // AVANCE AGRESIVO
            // =========================
            case Estados.cambioCuarto:

                player = Localizar("Player", 25f);

                if (player != Vector3.zero)
                {
                    Vector3 objetivo = new Vector3(
                        player.x + 2f,
                        player.y,
                        transform.position.z
                    );

                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        objetivo,
                        velocidad * 2f * Time.deltaTime
                    );

                    // Cuando lo sobrepasa
                    if (transform.position.x >= objetivo.x)
                    {
                        actual = Estados.vuelta;
                    }
                }

                break;

            // =========================
            // RETIRADA
            // =========================
            case Estados.vuelta:

                Vector3 posicionVuelta = new Vector3(
                    8f,
                    transform.position.y,
                    transform.position.z
                );

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    posicionVuelta,
                    velocidad * Time.deltaTime
                );

                // Cuando llega al borde
                if (Vector3.Distance(transform.position, posicionVuelta) < 0.1f)
                {
                    transform.localScale = scale * 2f;

                    actual = Estados.aparecer;
                }

                break;
        }
    }

    // ===================================
    // LOCALIZAR PLAYER
    // ===================================
    Vector3 Localizar(string tipo, float radio)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(
            transform.position,
            radio
        );

        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].CompareTag(tipo))
            {
                return objetos[i].transform.position;
            }
        }

        return Vector3.zero;
    }

    
    IEnumerator disparar()
    {
        while (disparando)
        {
            Instantiate(
                prefabProyectil,
                ptoDisparo.position,
                Quaternion.identity
            );

            yield return new WaitForSeconds(2f);
        }
    }
}
using System.Collections;
using UnityEngine;

public class dragonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // float movimientoX = Mathf.PingPong(Time.time * velocidad, 14) - 7;
    //transform.position = new Vector3(movimientoX, 0, 0);
    public float velocidad = 1;
    public Transform puntoDisparo;
    private float time = 0;
    private enum Estados { esperar,moverseTercio,localizarPlayer}
    private Estados actual = Estados.esperar;
    public GameObject proyectil;
    private Vector3 tercio;
    private bool haSalido = false;
    private bool disparando = false;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (actual) {
            case Estados.esperar:
                time += Time.deltaTime;
                if (time >= 5 && !haSalido) {
                    
                    transform.position = new Vector3(7.5f, Random.Range(-4f, 4f), transform.position.z);
                    haSalido = true;
                    GetComponent<SpriteRenderer>().enabled = true;
                    actual = Estados.moverseTercio;
                    time = 0;
                }

                break;
            case Estados.moverseTercio:
                time += Time.deltaTime;

                float movimientoX = Mathf.PingPong(Time.time * velocidad, 5)+6;//movimiento total 4,minimo 8
                transform.position = new Vector3(movimientoX, transform.position.y, transform.position.z);//la x se va moviendo
                if (!disparando) { 
                    disparando = true; 
                    StartCoroutine(disparar());
                }
                if (time >= 12f) {
                    disparando = false;
                    actual = Estados.localizarPlayer;
                }


                break;
            case Estados.localizarPlayer:
                break;
        }
    }
    IEnumerator disparar() {
        while (disparando)
        {
            yield return new WaitForSecondsRealtime(2f);
            Instantiate(proyectil, puntoDisparo.transform.position, Quaternion.identity);
            Destroy(gameObject, 20f);

        }
    }
    Vector3 Localizar(string tipo, float radio)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);

        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].gameObject.CompareTag(tipo))
            {
                return objetos[i].transform.position;
            }
        }

        return Vector3.zero; // Retorna vector cero si no encuentra nada
    }

}

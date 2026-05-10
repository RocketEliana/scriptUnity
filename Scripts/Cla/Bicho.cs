using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Bicho : MonoBehaviour
{

    private float velocidad = 1;
    private Vector3 aparece;
    private Vector3 centro;
    private Vector3 player;
    public Transform puntoDisparo;
    public GameObject prefebProyectil;
    public bool disparando;
    private float time = 0f;
    private Vector3 cuartoP;
    
    private enum Estados { aparecer,irCentro,pingPong,volver,localizarPlayerCuartoP,recalcular,desaparecer }
    private Estados actual = Estados.aparecer;
    private Vector3 inicial;
    
    void Start()
    {
        inicial = transform.localScale;
        aparece = new Vector3(7f, Random.Range(-4.5f, 4.5f), transform.position.z);
        centro = Vector3.zero;
        player = Vector3.zero;
        cuartoP = new Vector3(-4f, Random.Range(-4.5f, 4.5f), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        switch (actual) {
            case Estados.aparecer:
                transform.position = aparece;
                actual = Estados.irCentro;
                break;
            case Estados.irCentro:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(centro.x,transform.position.y,transform.position.z), velocidad * Time.deltaTime);
                if (transform.position.x == centro.x) { actual = Estados.pingPong; }
                break;
            case Estados.pingPong:
                time += Time.deltaTime;
                if (time <= 8f)
                {
                    float movimientoY = Mathf.PingPong(Time.time * velocidad, 8) - 4;
                    transform.position = new Vector3(transform.position.x, movimientoY, 0);
                    if (!disparando)
                    {
                        disparando = true;
                        StartCoroutine(disparaProyectil());
                    }
                }
                else {
                    time = 0;
                    disparando = false;
                    actual = Estados.localizarPlayerCuartoP;

                }
                    break;
            case Estados.localizarPlayerCuartoP:
               
                    player = Localizar("Player", 20f);
                transform.position = Vector3.MoveTowards(transform.position, player, velocidad * Time.deltaTime);
                if (transform.position.x <= cuartoP.x) { actual = Estados.recalcular; }
                        
                break;
            case Estados.recalcular:
                player = Localizar("Player", 20f);
                transform.position = Vector3.MoveTowards(transform.position, player, velocidad * Time.deltaTime);
                if (Mathf.Abs(transform.position.x - player.x) < 0.1f)
                {
                    actual = Estados.volver;
                }
                break;
            case Estados.volver:
                transform.position = Vector3.MoveTowards(transform.position, aparece, velocidad * 2 * Time.deltaTime);
                if (transform.position.x >= aparece.x) {
                    transform.localScale = inicial * 2;
                    actual = Estados.desaparecer;
                  
                }
                break;
            case Estados.desaparecer:
                transform.position += Vector3.left * velocidad * Time.deltaTime;
                Destroy(gameObject, 20f);
                break;
        }
        
    }
    IEnumerator disparaProyectil() {
        while (disparando) {

            Instantiate(prefebProyectil,puntoDisparo.position, Quaternion.identity);
            // 3.c: En fases de localización dispara cada 1s, si no cada 2s;
            float espera = (actual == Estados.pingPong || actual == Estados.volver) ? 1.5f : 0.5f;
            yield return new WaitForSeconds(espera);
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


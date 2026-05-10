using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bichoVerde : MonoBehaviour
{
    private float velocidad = 0.5f;
    public Image[] vidasUI;
    private  int vidas = 2;
    private float time = 0;
    private enum Estados { aparecer,localizar,redireccionar}

    private Estados actual = Estados.aparecer;
    private Vector3 limitesPantalla;
    private Vector3 enemigo;
    private bool aparecido = false;
    private Vector3 aparecer;
    void Start()
    {
        enemigo = Vector3.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        aparecer = new Vector3(-6, Random.Range(-4.5f, 4.5f), transform.position.z);
    /*limitesPantalla = transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -7f, 7f),
            transform.position.y,
            transform.position.z
        );*/
    }

    // Update is called once per frame
    void Update()
    {
        if (vidas <= 0) {
            Destroy(gameObject);
            return;
        }
        if ((transform.position.x <= -7f || transform.position.x >= 7f || transform.position.y >= 4.5f || transform.position.y < -4.5f) && aparecido) {
            actual = Estados.redireccionar;
                }
        switch (actual) {
            case Estados.aparecer:
                time += Time.deltaTime;
                if (time >= 5f) {
                    GetComponent<SpriteRenderer>().enabled = true;
                    transform.position = aparecer;
                    aparecido = true;
                    time = 0f;
                    actual = Estados.localizar;

                }
              
                break;
            case Estados.localizar:
               
                    transform.position += Vector3.right * velocidad * Time.deltaTime;
                    enemigo = Localizar("Enemigo", 20f);
               if(enemigo != Vector3.zero) { 
                    transform.position = Vector3.MoveTowards(transform.position, enemigo, velocidad
                        * Time.deltaTime);
                        }

                    break;
            case Estados.redireccionar:
                transform.position = aparecer;
                actual = Estados.localizar;
                break;
        
        
        
        }
      





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo")) {
            vidas -= 1;
            Destroy(collision.gameObject);
            for (int i = vidas; i < vidasUI.Length; i++)
            {
                vidasUI[i].enabled = false;
            }
            enemigo = Vector3.zero;
        }

    }
    // Función que busca el objeto más cercano con un tag específico dentro de un radio
    // Devuelve su posición como Vector3, o Vector3.zero si no encuentra nada
    Vector3 Localizar(string tipo, float radio)
    {
        // Detecta todos los colliders dentro de un círculo centrado en este objeto
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);

        // Aquí guardaremos el collider ganador (el más cercano)
        Collider2D masCercano = null;

        // Empieza en infinito para que cualquier distancia real sea menor
        float menorDistancia = Mathf.Infinity;

        // Recorre todos los objetos detectados dentro del círculo
        for (int i = 0; i < objetos.Length; i++)
        {
            // Filtra: solo nos interesan los que tengan el tag buscado (ej: "Enemigo")
            if (objetos[i].gameObject.CompareTag(tipo))
            {
                // Calcula la distancia entre este objeto y el candidato
                float distancia = Vector3.Distance(transform.position, objetos[i].transform.position);

                // Si este candidato está más cerca que el mejor registrado hasta ahora...
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia; // ...actualiza la distancia mínima
                    masCercano = objetos[i];    // ...y guárdalo como el más cercano
                }
            }
        }

        // Si encontró al menos un objeto con ese tag, devuelve su posición
        if (masCercano != null)
            return masCercano.transform.position;

        // Si no encontró nada, devuelve el origen del mundo como señal de "vacío"
        return Vector3.zero;
    }
}

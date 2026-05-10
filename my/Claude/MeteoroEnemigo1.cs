using System.Collections;
using UnityEngine;
//PINGPONG,CAMBIAcOLOR

public class MeteoroEnemigo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float velocidad = 2f;
    public Transform puntoDisparo;
    private Vector3 posCentro;
    public GameObject prefabProyectil;
    private Vector3 posMeteo;
    private bool disparando = false;
    private bool yaFinalizo = false;
    private enum Estado { patrullar, irCentro }
    private float tiempoDisparo = 0;
    private float tiempoPrimeraRonda = 0;
    private Estado acual = Estado.patrullar;
    void Start()
    {
        posMeteo = Vector3.zero;
        posCentro = new Vector3(0, Random.Range(-4, 4), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        switch (acual)
        {
            case Estado.patrullar:
                tiempoPrimeraRonda += Time.deltaTime;
                if (tiempoPrimeraRonda > 10) {
                    acual = Estado.irCentro;
                }
                else
                {// Esto oscila entre 0 y 8. Luego le restamos 4 para que sea entre -4 y 4.
                    float movimientoY = Mathf.PingPong(Time.time * velocidad, 8) - 4;
                    transform.position = new Vector3(7f, movimientoY, 0);
                    posMeteo = Localizar("Player", 25);
                    tiempoDisparo += Time.deltaTime;

                    if (tiempoDisparo >= 3f && posMeteo != Vector3.zero) {
                        SpriteRenderer sr = GetComponent<SpriteRenderer>();
                        sr.color = Color.red;
                        disparando = true;
                        StartCoroutine(disparar());
                        tiempoDisparo = 0;
                        disparando = false;
                      

                    }
                }
                    break;
            case Estado.irCentro:
                transform.position = Vector3.MoveTowards(transform.position, posCentro, velocidad * Time.deltaTime);

                // Añadimos "&& !yaFinalizo" a la condición
                if (Vector3.Distance(transform.position, posCentro) < 0.1f && !yaFinalizo)
                {
                    yaFinalizo = true; // Bloqueamos la entrada para que no se repita
                    StartCoroutine(fin());
                }
                break;
        }

    }
    IEnumerator disparar()
    {
        while (disparando)
        {
            yield return new WaitForSecondsRealtime(1);
            Instantiate(prefabProyectil, puntoDisparo.transform.position, Quaternion.identity);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = Color.white;

        }
    }
    IEnumerator fin()
    {
        // 1. Obtener el componente
     //   Animator anim = GetComponent<Animator>();

        // 2. Activar la animación mediante el nombre del Trigger
       // anim.SetTrigger("Explosion");

        // 3. ¡CUIDADO AQUÍ! 
        // Si destruyes el objeto inmediatamente, la animación no se verá.
        // Debes esperar a que la animación termine.
        yield return new WaitForSeconds(1.5f); // Ajusta este tiempo a lo que dure tu clip

        // 4. Ahora sí, adiós
        Destroy(gameObject);
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

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VerdeCla1 : MonoBehaviour
{
    public Slider barraVida; // Este es el que tiene que aparecer
    public int vidaMax = 3;
    private int vidaActual;
    private float time = 0;
    private float velocidad = 2;
    private enum Estados { espera, iniciar, localizaMasLejano }
    private Estados actual = Estados.espera;
    bool yaPosicionado = false;
    private Transform masLejano;
    bool morirB = false;

    void Start()
    {
        masLejano = null;
        vidaActual = vidaMax;
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMax;
            barraVida.value = vidaActual;
        }
    }
    void Update()
    {
       

        switch (actual) {
            case Estados.espera:
                time += Time.deltaTime;
                if (time >= 5f) { actual = Estados.iniciar; }
                break;
            case Estados.iniciar:
                if (!yaPosicionado)//solo UNA VEZ
                {
                    transform.position = new Vector3(-8, Random.Range(-4f, 4f), transform.position.z);
                    yaPosicionado = true; // Bloquea que se vuelva a ejecutar
                }
                masLejano = LocalizarTransform("Enemigo", 20);
                if (masLejano != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, masLejano.transform.position, velocidad * Time.deltaTime);
                }
                else {
                    transform.position += Vector3.left * velocidad * Time.deltaTime;
                }

                break;


        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo")) {
            RecibirDanio();
            Destroy(collision.gameObject);
            if (vidaActual <= 2) { velocidad /= 2; }
            if (vidaActual <= 0)
            {
                if (!morirB)
                {
                    morirB = true;
                    StartCoroutine(morir());
                
                    
                }
                

            }
        }
    }
    Transform LocalizarTransform(string tipo, float radio)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);
        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].gameObject.CompareTag(tipo))
            {
                return objetos[i].transform;    // ← devuelve el Transform, no la posición
            }
        }
        return null;
    }
    public void RecibirDanio()
    {
        vidaActual -= 1;
        if (barraVida != null)
        {
            barraVida.value = vidaActual;
        }
    }
    IEnumerator morir() {
        while (morirB) { 
        
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("triggerVerdeExplota");
            // 2. Esperamos a que la animación termine (por ejemplo, 2.5 segundos)
            yield return new WaitForSeconds(2.5f);

            // 3. Ahora que ya vimos la explosión, eliminamos el objeto
            Destroy(gameObject);


        }
}
}
using System.Collections;
using UnityEngine;

public class Salt : MonoBehaviour
{
    private float velocidad = 1;
    private float time = 0;
    private Vector3 aparecer;
    private int vida = 3;
    private Vector3 limiteSuperior;
    private Transform enemigoTransform; // ← referencia viva en vez de Vector3
    private enum Estados { aparecer, localizar, atacar }
    private Estados actual = Estados.aparecer;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        aparecer = new Vector3(Random.Range(-7f, 7f), -6f, transform.position.z);
        limiteSuperior = new Vector3(Random.Range(-7f, 7f), 6f, transform.position.z);
        enemigoTransform = null;
    }

    void Update()
    {
        if (vida <= 0) { Destroy(gameObject); return; }
        if (transform.position.y > limiteSuperior.y) { transform.position = aparecer; }

        switch (actual)
        {
            case Estados.aparecer:
                time += Time.deltaTime;
                if (time >= 5f)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                    transform.position = aparecer;
                    time = 0;
                    actual = Estados.localizar;
                }
                break;

            case Estados.localizar:
                if (enemigoTransform == null)
                {
                    enemigoTransform = Localizar("Enemigo", 20f);
                    if (enemigoTransform != null) { actual = Estados.atacar; }
                }
                else
                {
                    transform.position += Vector3.up * velocidad * Time.deltaTime;
                }
                break;

            case Estados.atacar:
                // Si el enemigo fue destruido, vuelve a localizar
                if (enemigoTransform == null) { actual = Estados.localizar; break; }
                // Persigue la posición ACTUAL del enemigo cada frame
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    enemigoTransform.position, // ← siempre actualizada
                    velocidad * Time.deltaTime
                );
                break;
        }

        // ← Limitar X al final de cada frame
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -7f, 7f),
            transform.position.y,
            transform.position.z
        );
    }

    Transform Localizar(string tipo, float radio) // ← devuelve Transform
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);
        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].gameObject.CompareTag(tipo))
            {
                return objetos[i].transform; // ← devuelve el Transform
            }
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            vida -= 1;
            Debug.Log("A mosqui le quedan" + vida + "vidas");
            Destroy(collision.gameObject);
            enemigoTransform = null; // ← reset, volverá a localizar
        }
    }
}
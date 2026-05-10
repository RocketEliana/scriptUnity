using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VerdeExa2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float time = 0;
    public meteoritoScript meteo;
    private Vector3 enemigo;
    private int vida = 2;
    public Image[] corazones;
    private Vector3 direccion_ataque;
    
    private enum Estados { aparecer,localizar}
    private Estados actual = Estados.aparecer;
    void Start()
    {
        enemigo = Vector3.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (meteo == null) { return; }
        if (vida <= 0) { Destroy(gameObject, 3f); return; }
        switch (actual) {
            case Estados.aparecer:
                time += Time.deltaTime;
                if (time >= 5) {
                    transform.position = new Vector3(-7f, Random.Range(-4.5f, 4.5f), transform.position.z);
                    actual = Estados.localizar;
                }
                break;
            case Estados.localizar:

                if (enemigo == Vector3.zero)
                {
                    enemigo = Localizar("Enemigo", 25f);
                    // Fija la dirección UNA SOLA VEZ
                    direccion_ataque = (enemigo - transform.position).normalized;
                }
                else
                {
                    // Se mueve en la dirección fija, nunca se para
                    transform.position += direccion_ataque *2 * Time.deltaTime;
                }
        
    

                    break;
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
        return Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo")) {
            vida -= 1;
            for (int i = vida; i < corazones.Length; i++)
            {
                corazones[i].enabled = false;
            }
            Destroy(collision.gameObject);
            enemigo = Vector3.zero;
            actual = Estados.localizar;
        
        
        }
    }
}


using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Amigo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private enum Estado { localiza, lanzarse,volver }
    private int vidas = 2;
    private Vector3 posicionVuelta;
    private Vector3 limite;
    private bool llegaLimite = false;
    private bool llegaPosicionVuelta = false;
    private Estado actual = Estado.localiza;
    private Vector3 atacar = Vector3.zero;
    
        void Start()
        {
            posicionVuelta = new Vector3(-8, transform.position.y, transform.position.z);
            limite = new Vector3(7f, transform.position.y, transform.position.z);
        }
    

    // Update is called once per frame
    void Update()
    {
        if (vidas <= 0) { Destroy(gameObject); return; }
        
        switch (actual) {
            case Estado.localiza:
                transform.position += Vector3.right * 4 * Time.deltaTime;
                if (Vector3.Distance(transform.position,limite)<0.5f)
                {
                    actual = Estado.volver;
                    break;
                }

                atacar = Localizar("Enemigo", 5);
                    if (atacar != Vector3.zero)
                    {
                        actual = Estado.lanzarse;
                    }
                
                break;
            case Estado.lanzarse:
                transform.position = Vector3.MoveTowards(transform.position, atacar, 2 * Time.deltaTime);
                if (Vector3.Distance(transform.position, atacar) < 0.1f) // ✅ llegó al objetivo
                {
                    actual = Estado.volver; // vuelve a casa antes de buscar de nuevo
                }
                break;

                break;
            case Estado.volver:
                transform.position = Vector3.MoveTowards(transform.position, posicionVuelta, 2*Time.deltaTime);
                if (Vector3.Distance(transform.position, posicionVuelta) < 0.1f) {
                    actual = Estado.localiza;
                }
                break;
        }


        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo")) {
            vidas -= 1;
            transform.position += Vector3.right * 2 * Time.deltaTime;
            atacar = Vector3.zero;
            actual = Estado.volver;
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

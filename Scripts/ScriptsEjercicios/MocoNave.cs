using System.Collections;
using UnityEngine;

public class MocoNave : MonoBehaviour
{
    public Transform puntoDisparos;
    public Vector3 posicionInicial;
    public float speedInicial = 1.5f;
    public float speedPotenciador = 2f;
    public float speedSuperPotenciador = 2.5f;
    public GameObject bala;
    public bool llegaCentro = false;
    public bool llegaIniciaL = false;
    public bool localizado = false;
    public bool alcanzado = false;
    public Vector3 posicionCentro;
    public enum Estados { fase1,volver,fase2,fase3}//1 ir centro,2 ir centro + detectarDesde centro,3 esta en centro localizza a un cuarto
    Estados actual = Estados.fase1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(instanciaDisparo());
        posicionInicial = transform.position;
        posicionCentro =  Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));



    }

    // Update is called once per frame
    void Update()
    {
        switch (actual) {
            case Estados.fase1:
                transform.position = Vector3.MoveTowards(transform.position, posicionCentro, speedInicial * Time.deltaTime);
                if (Vector3.Distance(transform.position, posicionCentro) < 0.1f) {
                    llegaCentro = true;
                  }
                if (llegaCentro) { actual = Estados.volver; }
                break;

            case Estados.volver:
                transform.position = Vector3.MoveTowards(transform.position, posicionInicial, speedInicial*Time.deltaTime);
                if (Vector3.Distance(transform.position, posicionInicial) < 0.1f) { llegaIniciaL = true; llegaCentro = false; }
                if (llegaIniciaL) { actual = Estados.fase2; };
                break;
            case Estados.fase2:
                if (!llegaCentro)
                {
                    transform.position = Vector3.MoveTowards(transform.position, posicionCentro, speedInicial * Time.deltaTime);
                    if (Vector3.Distance(transform.position, posicionCentro) < 0.1f) { llegaCentro = true; }

                }
                else
                {

                    Vector3 localizar = Localizar("Player", 10);
                    if (localizar != Vector3.zero)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, localizar, speedInicial * speedPotenciador * Time.deltaTime);
                    }
                    else { transform.position = Vector3.left * speedInicial * Time.deltaTime; }
                }

                    break;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("triggerExplosionMoco");
            StartCoroutine(offMoco());//tambien existe stopcorrutine!!!!                                                                                                                                                           

        }
    }
    IEnumerator instanciaDisparo() {
        while (true) {
            yield return new WaitForSecondsRealtime(5f);
            GameObject balaInstanciada = Instantiate(bala, puntoDisparos.transform.position, Quaternion.identity);
        }
    }
    IEnumerator offMoco() {
        yield return new WaitForSecondsRealtime(2f);

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

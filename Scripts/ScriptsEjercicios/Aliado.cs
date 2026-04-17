using UnityEngine;

public class Aliado : MonoBehaviour
{
    public int vida = 2;
    public float speed = 2f;

    public float radioDeteccion = 25f;

    public Transform objetivo;
    private Vector3 inicial;

    public bool tieneObjetivo = false;
    public bool volviendo = false;

    private void Start()
    {
        inicial = transform.position;
    }

    void Update()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
            return;
        }

        
        if (volviendo)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                inicial,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, inicial) < 0.1f)
            {
                volviendo = false;
                tieneObjetivo = false;
            }

            return;
        }

        
        if (!tieneObjetivo)
        {
            objetivo = LocalizarSatelite(radioDeteccion);

            if (objetivo != null)
            {
                tieneObjetivo = true;
            }
        }

        
        if (tieneObjetivo && objetivo != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                objetivo.position,
                speed * Time.deltaTime
            );
        }
    }

    Transform LocalizarSatelite(float radio)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);

        foreach (Collider2D col in objetos)
        {
            if (col.GetComponent<SateliteScript>() != null)
            {
                return col.transform;
            }
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SateliteScript>() != null)
        {
            vida -= 1;
            Destroy(collision.gameObject);

            volviendo = true;   
        }
    }
}
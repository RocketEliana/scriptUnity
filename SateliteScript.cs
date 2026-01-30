using UnityEngine;

public class SateliteScript : MonoBehaviour
{
    [Header("Configuracion del Satelite")]
    [Tooltip("Velocidad del satelite")]
    public float velocidad = 1f;
    public float factor = 2f;

    private Vector3 posicionAtaque;
    private Vector3 posicion_meteoro = new Vector3(0f, 0f, 0f);
    private Vector3 direccion_ataque = new Vector3(0f, 0f, 0f);
    private bool llegarPosicion = false;

    void Start()
    {
        // Límites de pantalla en 4.5f
        posicionAtaque = new Vector3(0f, Random.Range(-4.5f, 4.5f), transform.position.z);
        Destroy(gameObject, 25f);
    }

    void Update()
    {
        if (!llegarPosicion)
        {
            // Movimiento: e = v * t
            transform.position = Vector3.MoveTowards(transform.position, posicionAtaque, velocidad * Time.deltaTime);

            if (transform.position == posicionAtaque)
            {
                llegarPosicion = true;
            }
        }

        // Si ya llegó a la posición y no tiene localizado el meteoro, lo busca
        else{
            if(posicion_meteoro==new Vector3(0f,0f,0f))
        {
            posicion_meteoro = Localizar("Player", 25f);
                //fijo posicion de ataque
                                direccion_ataque = (posicion_meteoro - transform.position).normalized;
                //normalizo la direccion
            }
            else { 
                //Movimiento hacia el meteoro
                transform.position += direccion_ataque * velocidad * factor * Time.deltaTime;
            }
        }
    }

    // Método que localiza cualquier collider en el radio pasado por parámetro
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

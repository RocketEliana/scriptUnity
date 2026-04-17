using UnityEngine;

public class SateliteScript : MonoBehaviour
{
    [Header("Configuracion del Satelite")]
    [Tooltip("Velocidad del satelite")]
    public float velocidad = 1f;
    public float factor = 2f;
    public int daño = 3;

    private Vector3 posicionAtaque;
    private Vector3 posicion_meteoro = new Vector3(0f, 0f, 0f);
    private Vector3 direccion_ataque = new Vector3(0f, 0f, 0f);
    private bool llegarPosicion = false;

    void Start()
    {
        // Límites de pantalla en 4.5f
        posicionAtaque = new Vector3(0f, Random.Range(-4.5f, 4.5f), transform.position.z);
        Destroy(gameObject, 30f);
    }

    void Update()
    {
        if (!llegarPosicion)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionAtaque, velocidad * Time.deltaTime);

            // ← Comprueba si ha llegado
            if (transform.position == posicionAtaque)
                llegarPosicion = true;
        }
        else // ← Solo busca al jugador UNA VEZ ha llegado a su posición
        {
            if (posicion_meteoro == Vector3.zero)
            {
                posicion_meteoro = Localizar("Player", 25f);
                direccion_ataque = (posicion_meteoro - transform.position).normalized;
            }
            else
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    posicion_meteoro,
                    velocidad * factor * Time.deltaTime
                );
            }
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

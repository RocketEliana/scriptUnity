using UnityEngine;

public class coheteGrisScript : MonoBehaviour
{
    [Header("Configuracion del cohete")]
    [Tooltip("Velocidad del cohete")]
    public float velocidad = 1f;

    [Tooltip("Array de sprites de cohetes")]
    public Sprite[] cohetes; // Esta es la variable correcta

    void Start()
    {
        // CORRECCIÓN: Usamos 'cohetes' que es el nombre de tu array
        // 'Instanci' no existe, simplemente asignamos el sprite del array
        if (cohetes.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = cohetes[Random.Range(0, cohetes.Length)];
        }
    }

    void Update()
    {
        // Mover hacia la izquierda
        transform.position += Vector3.left * velocidad * Time.deltaTime;

        // Destruir el objeto cuando salga de la pantalla
        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }
}

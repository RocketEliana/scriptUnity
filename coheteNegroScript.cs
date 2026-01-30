using UnityEngine;

public class coheteNegroScript : MonoBehaviour
{
    [Header("Configuracion del cohete Negro")]
    [Tooltip("Velocidad del cohete Negro")]
    public float velocidad = 1f;

    [Tooltip("Array de sprites de cohetes")]
    public Sprite[] cohetesNegros; // Esta es la variable correcta

    void Start()
    {
        // CORRECCIÓN: Usamos 'cohetes' que es el nombre de tu array
        // 'Instanci' no existe, simplemente asignamos el sprite del array
        if (cohetesNegros.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = cohetesNegros[Random.Range(0, cohetesNegros.Length)];
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

using UnityEngine;

public class proyectilScript : MonoBehaviour
{
    [Header("Configuracion del proyectil")]
    [Tooltip("Velocidad del proyectil")]
    public float velocidad = 1f;
    [Tooltip("Daño del cohete proyectil")]
    public int danio = 2;



    void Start()
    {
        Destroy(gameObject, 25f); // Destruir el cohete después de 10 segundos para evitar acumulación
    }

    void Update()
    {
        // Mover hacia la izquierda
        transform.position += Vector3.left * velocidad * Time.deltaTime;

    }
}


using UnityEngine;

public class coheteGrisScript : MonoBehaviour
{
    [Header("Configuracion del cohete")]
    [Tooltip("Velocidad del cohete")]
    public float velocidad = 1f;
    [Tooltip("Daño del cohete")]
    public int danio = 1;

    void Start()
    {
        Destroy(gameObject, 25f); // Destruir el cohete después de 10 segundos para evitar acumulación
    }

    void Update()
    {

        transform.position += Vector3.left * velocidad * Time.deltaTime;

    }
}


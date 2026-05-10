using UnityEngine;

public class MasVidas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Prefab de vida")]
    public GameObject prefabVida;

    private float tiempoActual;
    

    [Header("Referencias")]
    public DeepMeteo meteorito;

    void Start()
    {
        tiempoActual = 0f;
        

    }

    void Update()
    {
        if (meteorito == null) return;

        if (meteorito.vida >= 5)
        {
            tiempoActual = 0f;
            return;
        }

        tiempoActual += Time.deltaTime;

        if (tiempoActual >= 15f)
        {
            // Genera la vida en la posición X=12
            Instantiate(prefabVida, new Vector3(12, Random.Range(-4.5f, 4.5f), -1), Quaternion.identity);

            tiempoActual = 0f;
            
        }
    }
}

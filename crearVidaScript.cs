using UnityEngine; // Esta DEBE ser la primera línea

public class crearVidaScript : MonoBehaviour
{
    [Header("Prefab de vida")]
    public GameObject prefabVida;

    private float tiempoActual;
    private float intervalo;

    [Header("Referencias")]
    public meteoritoScript meteorito;

    void Start()
    {
        tiempoActual = 0f;
        intervalo = Random.Range(15f, 45f);
    }

    void Update()
    {
        if (meteorito == null) return;

        if (meteorito.vida >= 3)
        {
            tiempoActual = 0f;
            return;
        }

        tiempoActual += Time.deltaTime;

        if (tiempoActual >= intervalo)
        {
            // Genera la vida en la posición X=12
            Instantiate(prefabVida, new Vector3(12, Random.Range(-4.5f, 4.5f), -1), Quaternion.identity);

            tiempoActual = 0f;
            intervalo = Random.Range(15f, 45f);
        }
    }
}
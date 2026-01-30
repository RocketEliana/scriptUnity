using UnityEngine;

public class crearCoheteNegro : MonoBehaviour
{
    [Header("Prefab de cohete")]
    public GameObject cohetePrefab;
    private float tiempo, intervalo = 3f;
  
    void Start()
    {
        tiempo = 0f;
        // Establecer un intervalo inicial aleatorio entre 3 y 7 segundos
        intervalo = Random.Range(2f, 4f);

    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > intervalo)
        {
            //Quaternion.identity representa la rotación sin cambios
            GameObject.Instantiate(cohetePrefab, new Vector3(12, Random.Range(-4.5f, 4.5f), -1), Quaternion.identity);
            tiempo = 0f;
            intervalo = Random.Range(3f, 5f);
        }
        else
        {
            tiempo += Time.deltaTime;
        }

    }
}

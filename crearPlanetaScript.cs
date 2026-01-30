using UnityEngine;

public class crearPlanetaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Prefab de planeta")]
    public GameObject planetaPrefab;
    private float tiempo,intervalo = 5f;
    void Start()
    {
        tiempo = 0f;
        // Establecer un intervalo inicial aleatorio entre 3 y 7 segundos
        intervalo = Random.Range(3f, 7f);


    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > intervalo)
        {
            //Quaternion.identity representa la rotación sin cambios
            GameObject.Instantiate(planetaPrefab, new Vector3(12,Random.Range(-4.5f, 4.5f), -1), Quaternion.identity);
            tiempo = 0f;
            intervalo = Random.Range(3f, 5f);
        }
        else
        {
                        tiempo += Time.deltaTime;
        }
        
    }
}

using UnityEngine;

public class crearPlanetaScript : MonoBehaviour
{
   
    [Header("Prefab de planeta")]
    public GameObject planetaPrefab;
    private float tiempo,intervalo;
    void Start()
    {
        tiempo = 0f;
        // Establecer un intervalo inicial aleatorio entre 3 y 10
        // segundos
        intervalo = Random.Range(5f, 10f);


    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > intervalo)
        {
            //Quaternion.identity representa la rotación sin cambios
            GameObject.Instantiate(planetaPrefab, new Vector3(12,Random.Range(-4.5f, 4.5f), -1), Quaternion.identity);
            tiempo = 0f;
            intervalo = Random.Range(5f, 10f);
        }
        else
        {
                        tiempo += Time.deltaTime;
        }
        
    }
}

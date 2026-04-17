using UnityEngine;

public class crearUps : MonoBehaviour
{
    private float tiempoArranque = 0;
    public GameObject prefab;  // Este es tu prefab para instanciar
    private float rango;

    void Start()
    {
        rango = Random.Range(0, 10);
    }

    void Update()
    {
        if (tiempoArranque > rango)
        {
            Instantiate(prefab,new Vector3(10,Random.Range(5,-5), transform.position.z), Quaternion.identity); // Usar prefab, no gameObject
            tiempoArranque = 0;
            rango = Random.Range(0, 10);
        }
        else
        {
            tiempoArranque += Time.deltaTime;
        }
    } // <- Esta llave estaba faltando
}
        
    

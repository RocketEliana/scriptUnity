using UnityEngine;

public class CrearVidaE2 : MonoBehaviour
{
    private float tiempo = 0f;
    public GameObject prefab;
    private bool creado = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (creado) { return; }
        tiempo += Time.deltaTime;
        if (tiempo >= 5f) {
            Instantiate(prefab, new Vector3(8, Random.Range(-4f, 4f), transform.position.z), Quaternion.identity);
            creado = true;
        }
        
    }
}

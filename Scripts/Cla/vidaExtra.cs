using UnityEngine;

public class vidaExtra : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float time = 0;
    public GameObject prefab;
    private bool yaInstanciado = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 8 && !yaInstanciado) {
            Instantiate(prefab, new Vector3(8, Random.Range(-4f, 4f), transform.position.z), Quaternion.identity);
            yaInstanciado = true;
            time = 0;
        }
        
    }
}

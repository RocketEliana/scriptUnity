using UnityEngine;

public class VidaExtra : MonoBehaviour
{
    public GameObject prefabVida;
    private float time = 0f;
    private bool instanciado = false;
    public MeteoroExamen meteo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (instanciado) { return; }
        time += Time.deltaTime;
        if (time >= 5f && meteo.vida<4) { 
        Instantiate(prefabVida, new Vector3(transform.position.x, Random.Range(-4f, 4f), 0), Quaternion.identity);
            time = 0;
            instanciado = true;

        }
        
    }
}

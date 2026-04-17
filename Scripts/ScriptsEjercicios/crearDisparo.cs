using UnityEngine;

public class crearDisparo : MonoBehaviour
{
    public GameObject disparo;
    private  float time = 0;
    private float rango;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rango = Random.Range(1f, 3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > rango)
        {
            Instantiate(disparo);
            time = 0;
            rango = Random.Range(1, 3);
        }
        else {
            time += Time.deltaTime;
        }
        
    }
}

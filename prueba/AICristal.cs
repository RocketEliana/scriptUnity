using System.Collections;
using UnityEngine;

public class AICristal : MonoBehaviour
{
    public GameObject prefab;
   private float timeInicio=0;
    private float intervalo;


    void Start()
    {
        intervalo=Random.Range(5f, 10f);

    }

    private void Update()
    {
        if (timeInicio > intervalo)
        {
            Instantiate(prefab, new Vector3(Random.Range(-5f,5f),transform.position.y,transform.position.z), Quaternion.identity);
            timeInicio = 0f;
        }
        else
        {
                       timeInicio += Time.deltaTime;
        }
    }
}
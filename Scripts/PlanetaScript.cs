using UnityEngine;

public class CoheteGrisScript : MonoBehaviour
{
    [Header("Configuracion del planeta")]
    [Tooltip("Velocidad del planeta")]
    public float velocidad = 1;
    [Tooltip("Vector de planetas")]
    public Sprite[] planetas;


    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = planetas[Random.Range(0, planetas.Length)];


    }
    void Update()
    { 
        transform.position=transform.position+Vector3.left*velocidad*Time.deltaTime;
        //destruir el objeto cuando salga de la pantalla    
        if (transform.position.x<-11){
            Destroy(gameObject);
        }

    }
}

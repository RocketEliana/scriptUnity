using UnityEngine;

public class CoheteGrisScript : MonoBehaviour
{
    [Header("Configuracion del planeta")]
    [Tooltip("Velocidad del planeta")]
    //Variables publicas
    public float velocidad = 1;
    [Tooltip("Vector de planetas")]
    public Sprite[] planetas; 
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { 
        GetComponent<SpriteRenderer>().sprite = planetas[Random.Range(0, planetas.Length)];
        //Destroy(gameObject, 15f);

    }

    // Update is called once per frame
    void Update()
    {
        //vetor3.left = (-1,0,0)
        transform.position=transform.position+Vector3.left*velocidad*Time.deltaTime;
        //destruir el objeto cuando salga de la pantalla    
        if (transform.position.x<-11){
            Destroy(gameObject);
        }

    }
}

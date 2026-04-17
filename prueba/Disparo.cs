using TMPro;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        
       
        transform.Translate(Vector3.right * Time.deltaTime * 10f);      
        Destroy(gameObject, 5f);
        

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            FindFirstObjectByType<Puntos>().Sumar(10);
        }
    }

}

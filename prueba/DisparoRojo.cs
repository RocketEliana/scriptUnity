using Unity.VisualScripting;
using UnityEngine;

public class DisparoRojo : MonoBehaviour

{
    public float velocidad = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * velocidad * Time.deltaTime;
        if (transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            Debug.Log("TOQUE ALGO: " + collision.gameObject.name + " tag: " + collision.tag);
            FindFirstObjectByType<Puntos>().Sumar(50);
            Destroy(collision.gameObject);
        }
    }
}
        


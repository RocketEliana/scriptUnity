using UnityEngine;

public class miniMeteoro : MonoBehaviour
{
    public float speed = 3f;
    public int danio = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        Destroy(gameObject, 10f);
    }
}

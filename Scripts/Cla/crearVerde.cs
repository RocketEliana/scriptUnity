using UnityEngine;

public class crearVerde : MonoBehaviour
{
    private float time = 0f;
    public GameObject verde;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 5f) {
            Instantiate(verde, new Vector3(transform.position.x,
           Random.Range(-4.5f, 4.5f),
            transform.position.z), Quaternion.identity);
            Destroy(gameObject, 3f);
                }
        
    }
}

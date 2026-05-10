using UnityEngine;

public class miniM : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * 2 * Time.deltaTime;
        Destroy(gameObject, 20f);
    }
}

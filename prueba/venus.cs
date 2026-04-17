using UnityEngine;

public class venus : MonoBehaviour
{
    public float velocidad = 10f;
    public Sprite[] sprites;
    //private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,2)];
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
      transform.position +=Vector3.left * velocidad * Time.deltaTime;
    }
}

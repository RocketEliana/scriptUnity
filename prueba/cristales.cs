using UnityEngine;

public class cristales : MonoBehaviour
 

{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * 3f * Time.deltaTime;
        
    }
}

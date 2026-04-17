using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Velocidad del enemigo")]
    public float velocidad = 5f;
    private Animator animator;
    public AudioClip sonidoExplosion; // ← AudioClip, no AudioSource

    void Start()
    {
       
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;

        if (transform.position.x < -15)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            animator.SetTrigger("triggerGolpe");
            GetComponent<AudioSource>().PlayOneShot(sonidoExplosion);
            GetComponent<Collider2D>().enabled = false;
            velocidad = 0f; // ← 0 para que se detenga del todo
            Destroy(gameObject, 1f);
        }
    }
}
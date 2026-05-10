using UnityEngine;

public class enemigoFracturado : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*case Estado.volverFase2:
            if (Vector3.Distance(transform.position, limitePantalla) < 0.1f)
            {
                // instancia 4 copias en posiciones ligeramente distintas
                for (int i = 0; i < 4; i++)
                {
                    Vector3 offset = new Vector3(0, (i - 1.5f) * 1.5f, 0); // las separa en Y
                    Instantiate(gameObject, transform.position + offset, Quaternion.identity);
                }
                Destroy(gameObject); // se destruye a sí mismo
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, limitePantalla, velocidad * 2 * Time.deltaTime);
            }
            break;
        */
        }
}

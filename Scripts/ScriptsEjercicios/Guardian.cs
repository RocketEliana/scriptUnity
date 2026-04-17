using UnityEngine;

public class Guardian : MonoBehaviour
{
    private float speed = 2f;
    private float speedAumentada = 2f;
    private Vector3 posicionMuro;
    private bool haLlegadoMuro = false;
    public int daño = 1;
    
    void Start()
    {
        posicionMuro = new Vector3(0, transform.position.y, transform.position.z);
        //me importa la x acuerdate de...x=0,y=0 x=0,y=5 me da =
        //en otros codigos me he liado con y z pero eso me da =,la posicion que sea me vale!
      
    }
    void Update()
    {
        if (!haLlegadoMuro)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionMuro, speed * Time.deltaTime);
            if (transform.position == posicionMuro) { haLlegadoMuro = true; }
        }
        else {
            //si ha llegaro giro y trato de localizarlo
            transform.rotation = Quaternion.Euler(0, 180, 0);//0,0,0, dxa,0,0,90 arriba 0,0,-90 abajo
            Vector3 localizado = Localizar("Player", 10);
            if (localizado != Vector3.zero) {
                transform.position = Vector3.MoveTowards(transform.position, localizado, speed * speedAumentada * Time.deltaTime);
            }
        }

    }
    Vector3 Localizar(string tipo, float radio)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);//como un radar,envuelve en un array todos los que colisiona

        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].gameObject.CompareTag(tipo))
            {
                return objetos[i].transform.position;
            }
        }

        return Vector3.zero; // Retorna vector cero si no encuentra nada
    }
    //ver radio
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }


}
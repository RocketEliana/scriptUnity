using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemigoEscenaAsalto : MonoBehaviour
{
    private float velocidad = 2f;
    private float timePinPong = 0;
    private Vector3 centro;
    public Transform puntoDisparo;
    public GameObject proyectil;
    private Estado actual = Estado.irCentro;
    private bool disparando = false;
    private bool haDisparado = false;
    private Vector3 escalaOriginal;
    private Vector3 meteo;
    private Vector3 inicial;
    private Vector3 tercio;
    private enum Estado { irCentro, pingPong, localizaMeteoro,subir }
    void Start()
    {
        centro = new Vector3(0f, 0f, 0f);
        escalaOriginal = transform.localScale;
        meteo = Vector3.zero;
        inicial = transform.position;
        tercio = new Vector3(-3.5f, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        switch (actual)
        {
            case Estado.irCentro:
                transform.position = Vector3.MoveTowards(transform.position, centro, velocidad * Time.deltaTime);
                if (Vector3.Distance(transform.position, centro) < 0.1f)
                {
                    actual = Estado.pingPong;
                }
                break;
            case Estado.pingPong:
                timePinPong += Time.deltaTime;
                float movimientoX = Mathf.PingPong(Time.time * velocidad, 14) - 7;
                transform.position = new Vector3(movimientoX, 0, 0);
                if (!disparando)//LLAMA SOLO UNA VEZ!!EN EL SEGUNDO FRAME YA ES TRUE,NO VA A ENTRAR MAS Y TE VA A HACER UN TUBO!!!!
                {
                disparando = true;
                StartCoroutine(disparo());
                 }


                if (timePinPong >= 8f)
                {
                    disparando = false;
                    actual = Estado.localizaMeteoro;

                }
              
                break;
            case Estado.localizaMeteoro:
                meteo = Localizar("Player", 20f);
                if (meteo != Vector3.zero) {
                    // Usamos la Y del meteoro pero la X fija del tercio
                    //aqui ESTABA METIENDO LA PATA NO PUEDO IR HACIA UN PUNTO 
                    //YCOMPARAR CON OTRO PUNTO POR QUE LE VUELVO LOCO COMO ESTABA HACIENDO
                    //POR ESO,USO LA X DE TERCIO Y LA Y DE METEORO
                    Vector3 puntoIntermedio = new Vector3(tercio.x, meteo.y, transform.position.z);

                    transform.position = Vector3.MoveTowards(transform.position, puntoIntermedio, velocidad * Time.deltaTime);

                    // 3. Si llega al tercio (X = -3.5), cambiamos a subir
                    if (Vector2.Distance(transform.position, puntoIntermedio) < 0.1f)
                    {
                        actual = Estado.subir;
                    }
                }
                break;

            case Estado.subir:
                transform.position += Vector3.up * velocidad * Time.deltaTime;
                //ACABA ESTE METODO DEL EXAMEN DE CLAUDE!!!
                break;  
        }
        

    }
    IEnumerator disparo()
    {
        while (disparando)
        {
            
            Instantiate(proyectil, new Vector3(puntoDisparo.position.x, puntoDisparo.position.y, puntoDisparo.position.z), Quaternion.identity);
            yield return new WaitForSecondsRealtime(1.5f);

        }
    }
    Vector3 Localizar(string tipo, float radio)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);

        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].gameObject.CompareTag(tipo))
            {
                return objetos[i].transform.position;
            }
        }

        return Vector3.zero; // Retorna vector cero si no encuentra nada
    }
}

using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class proyrvtil : MonoBehaviour
{
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 10f;
    public float velocidad = 5f;
    private AudioSource audioJuego;
    public AudioClip sonidoDisparo;
    public GameObject[] corazones;
    private int vidas = 3;
    public GameObject[] ups;
    private int up = 0;
    private bool esIndestructible = false;
    private Vector3 escalaOriginal;
    private int cristal = 0;
    public GameObject[] cristales;
    public GameObject proyectilRojo;
    private bool cristalActivado = false;

    private void Start()
    {
        escalaOriginal = transform.localScale;
        audioJuego = GetComponent<AudioSource>();
        for (int i = 0; i < ups.Length; i++)
        {
            ups[i].SetActive(false);
        }
        for(int i = 0; i < cristales.Length; i++)
        {
            cristales[i].SetActive(false);
        }

    }
    void Update()
    {
        // Esta es la forma del Nuevo Input System
        if (!cristalActivado && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Instanciar();
        }
        if (cristalActivado && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            IstanciarRojo();
        }
        if (Keyboard.current.upArrowKey.isPressed && transform.position.y < 4.5)
        {
            transform.position += Vector3.up * Time.deltaTime * velocidad;
        }
        
        if (Keyboard.current.downArrowKey.isPressed && transform.position.y > -4.5)
        {
            transform.position += Vector3.down * Time.deltaTime * velocidad;
        }

        if (Keyboard.current.rightArrowKey.isPressed && transform.position.x < 4.5)
        {
            transform.position += Vector3.right * Time.deltaTime * velocidad;
        }

        if (Keyboard.current.leftArrowKey.isPressed && transform.position.x > -4.5)
        {
            transform.position += Vector3.left * Time.deltaTime * velocidad;
        }
        if (esIndestructible)
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else { transform.localScale = escalaOriginal;

        }
    }
    public void Instanciar()
    {
        GameObject bala = Instantiate(prefabProyectil, puntoDisparo.position, Quaternion.identity);
        // 2. Reproducimos el sonido
        if (audioJuego != null && sonidoDisparo != null)
        {
            audioJuego.PlayOneShot(sonidoDisparo);
        }
    }
    public void IstanciarRojo()
    {
        GameObject balaRoja = Instantiate(proyectilRojo, puntoDisparo.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            if (esIndestructible)
            {
                Debug.Log("¡Invencible! No pierdes vida.");
                Destroy(collision.gameObject); // Destruye al enemigo igual
                return; // No restar vida
            }
            Debug.Log("Choque con enemigo");
            vidas--;

            // Deshabilitar el corazón correspondiente
            if (vidas >= 0 && vidas < corazones.Length)
            {
                corazones[vidas].SetActive(false);
            }



            if (vidas <= 0)
            {
                Debug.Log("¡Jugador ha muerto!");
                Time.timeScale = 0f;
            }

      
        }
        if (collision.CompareTag("up"))
        {
            Destroy(collision.gameObject); // Destruir el power-up

           
            if (up < ups.Length)
            {
                ups[up].SetActive(true);
            }
            up++;

            if (up == 3)
            {
                esIndestructible = true;
                StartCoroutine(indestructible());
            }
        }
        if (collision.CompareTag("Cristal")){
                       Destroy(collision.gameObject);
            if (cristal < cristales.Length) { 
                cristales[cristal].SetActive(true);
            }
            cristal++;
            if (cristal == 3)
            {
                StartCoroutine(cristalRojo());

            }
        }
    }

    IEnumerator indestructible()
    {
        yield return new WaitForSeconds(5f);
        esIndestructible = false;

        for (int i = 0; i < ups.Length; i++)
        {
            ups[i].SetActive(false);
        }
        up = 0;
    }

    IEnumerator cristalRojo()
    {
        cristalActivado = true;
        yield return new WaitForSeconds(10f);
        cristalActivado = false;

        // Resetear cristales
        for (int i = 0; i < cristales.Length; i++)
            cristales[i].SetActive(false);
        cristal = 0;
    }
}
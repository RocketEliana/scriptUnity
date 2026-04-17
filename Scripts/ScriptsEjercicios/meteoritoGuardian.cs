using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class meteoritoGuardian : MonoBehaviour
{
    [Header("Propiedades meteorito")]
    public float velocidad = 10f;
    public  int vida = 3;
    public Image[] corazones;
    public TextMeshProUGUI texto;

    [Header("Audio")]
    public AudioClip sonidoGolpe;
    public AudioClip sonidoMuerte;
    private AudioSource audioSource;

    private bool isDead = false;
    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (anim != null) anim.updateMode = AnimatorUpdateMode.UnscaledTime;

        if (texto != null && string.IsNullOrEmpty(texto.text))
            texto.text = "0";
    }

    void Update()
    {
        if (isDead) return;

        if (Keyboard.current.upArrowKey.isPressed && transform.position.y < 5f)
            transform.position += velocidad * Vector3.up * Time.deltaTime;

        if (Keyboard.current.downArrowKey.isPressed && transform.position.y > -5f)
            transform.position += velocidad * Vector3.down * Time.deltaTime;
        if (Keyboard.current.rightArrowKey.isPressed && transform.position.x < 9f)
        {
            transform.position += velocidad * Vector3.right * Time.deltaTime;
        }
        if (Keyboard.current.leftArrowKey.isPressed && transform.position.x >-9f)
        {
            transform.position += velocidad * Vector3.left * Time.deltaTime;
        }
    }
        
    //private void OnCollisionEnter2D(Collision2D collision) fuerzas!
    private void OnTriggerEnter2D(Collider2D collision)//detecta colisiones sin fuerzas//
                                                       //, ideal para objetos que no necesitan físicas, como el meteoros
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            ProcesarDanio(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Salud"))
        {
            ProcesarCuracion(collision.gameObject);
        }
    }

    private void ProcesarDanio(GameObject enemigo)
    {
        int danio = 0;

        coheteGrisScript coheteGris = enemigo.GetComponent<coheteGrisScript>();
        coheteNegroScript coheteNegro = enemigo.GetComponent<coheteNegroScript>();
        SateliteScript satelite = enemigo.GetComponent<SateliteScript>();
        Guardian guardian = enemigo.GetComponent<Guardian>();
        miniMeteoro mini = enemigo.GetComponent<miniMeteoro>();




        if (coheteGris != null)
            danio = coheteGris.danio;
        else if (coheteNegro != null)
            danio = coheteNegro.danio;
        else if (satelite != null)
            danio = satelite.daño;
        else if (guardian != null)
            danio = guardian.daño;
        else if (mini != null)
            danio = mini.danio;


        else
            Debug.LogWarning("El enemigo no tiene script de daño: " + enemigo.name);


        vida -= danio;
        vida = Mathf.Clamp(vida, 0, corazones.Length); // Mathf.Clamp garantiza que vida nunca salga del rango

        // Desactivar TODOS los corazones desde el índice "vida" hasta el final
        for (int i = vida; i < corazones.Length; i++)
        {
            corazones[i].enabled = false;
        }

        if (vida <= 0)
        {
            //Solo triggerMuerte, no triggerGolpe
            isDead = true; // ← Bloquea nuevas colisiones YA
            anim.SetTrigger("triggerMuerte");
            StartCoroutine(MorirYDestruir());
        }
        else
        {
            anim.SetTrigger("triggerGolpe"); // ← Solo si NO muere
        }





        // --- SONIDO DE COLISIÓN/GOLPE ---
        if (audioSource != null && sonidoGolpe != null)
        {
            audioSource.PlayOneShot(sonidoGolpe);
        }
        //__//

        try
        {
            texto.text = (Int32.Parse(texto.text) - danio * 100).ToString();
        }
        catch
        {
            Debug.LogWarning("Error al leer puntuación");
        }
        Destroy(enemigo);
    }

    private IEnumerator MorirYDestruir()
    {
        yield return new WaitForSecondsRealtime(0.8f);


        if (audioSource != null && sonidoMuerte != null)
            AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);

        Time.timeScale = 0f;

        //Destruir DESPUÉS de congelar, no al mismo tiempo
        Destroy(gameObject);
    }

    private void ProcesarCuracion(GameObject objetoVida)
    {
        if (vida < corazones.Length)
        {
            vida += 1;
            if (vida - 1 < corazones.Length)
            {
                corazones[vida - 1].enabled = true;
            }
        }
        Destroy(objetoVida);
    }
    public void parar()
    {
        anim.enabled = false;
        audioSource.Pause();
    }



}
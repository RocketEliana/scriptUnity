using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class meteoritoScript : MonoBehaviour
{
    [Header("Propiedades meteorito")]
    public float velocidad = 10f;
    public int vida = 3;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        danioScript scriptDano = enemigo.GetComponent<danioScript>();
        int damageValue = (scriptDano != null) ? scriptDano.damage : 1;

        // --- SONIDO DE COLISIÓN/GOLPE ---
        if (audioSource != null && sonidoGolpe != null)
        {
            audioSource.PlayOneShot(sonidoGolpe);
        }

        try
        {
            int puntosActuales = Int32.Parse(texto.text.Trim());
            texto.text = (puntosActuales - (vida * 100)).ToString();
        }
        catch { Debug.LogWarning("Error al leer puntuación"); }

        for (int i = 0; i < damageValue; i++)
        {
            int indiceCorazon = vida - 1 - i;
            if (indiceCorazon >= 0 && indiceCorazon < corazones.Length)
            {
                corazones[indiceCorazon].enabled = false;
            }
        }

        vida -= damageValue;
        Destroy(enemigo);

        if (vida <= 0)
        {
            Morir();
        }
        else
        {
            anim.SetTrigger("triggerGolpe");
        }
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

    void Morir()
    {
        if (isDead) return; // Evita que el sonido suene varias veces
        isDead = true;

        // --- SONIDO DE MUERTE ---
        if (audioSource != null && sonidoMuerte != null)
        {
            audioSource.PlayOneShot(sonidoMuerte);
        }

        if (anim != null) anim.SetTrigger("triggerMuerte");

        GetComponent<Collider2D>().enabled = false;

        Invoke("OcultarMeteoro", 0.5f);
        Invoke("PararJuego", 1.4f);
        Destroy(gameObject, 1.5f);
    }

    void OcultarMeteoro()
    {
        if (sprite != null) sprite.enabled = false;
    }

    void PararJuego()
    {
        Time.timeScale = 0f;
        if (anim != null) anim.enabled = false;
    }
}
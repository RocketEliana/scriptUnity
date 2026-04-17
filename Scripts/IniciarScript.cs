using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IniciarScript : MonoBehaviour
{
    public  Button iniciarButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        iniciarButton.onClick.AddListener(empezar);
    }
    void empezar()
    {
        SceneManager.
            LoadScene("EscenaPrincipal");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

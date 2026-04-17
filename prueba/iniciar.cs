using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class iniciar : MonoBehaviour
{
    
    public Button iniciarButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        iniciarButton.onClick.AddListener(empezar);
    }
    void empezar()
    {
        SceneManager.
            LoadScene("SampleScene");
    }
    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;

public class VidaEjercicio2 : MonoBehaviour
{
    public int vida;
    public float velocidad = 1f;
    private void Start() {
        Destroy(gameObject, 25f);
    }
    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);
    }



}

using UnityEngine;

public class MercanteInteracao : MonoBehaviour
{
    public GameObject painelCompra;
    private bool jogadorPerto = false;

    void Start(){
        painelCompra.SetActive(false);
    }
    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            painelCompra.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            painelCompra.SetActive(false);
        }
    }
}
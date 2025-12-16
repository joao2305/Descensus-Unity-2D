using UnityEngine;
using UnityEngine.SceneManagement;

public class FogueiraScript : MonoBehaviour
{
    public GameObject painelFogueira;
    private bool jogadorPerto = false;
void Start()
{
    painelFogueira.SetActive(false);
}
    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            painelFogueira.SetActive(true);
        }
    }

    public void CarregarCena()
    {
        SceneManager.LoadScene("Circulo 1");
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
            if (painelFogueira != null)
            {
            painelFogueira.SetActive(false);
            }
        }
    }
}


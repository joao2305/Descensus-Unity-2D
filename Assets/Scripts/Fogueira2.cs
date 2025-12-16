using UnityEngine;
using UnityEngine.SceneManagement;

public class Fogueira2 : MonoBehaviour
{
    private bool jogadorPerto = false;
void Start()
{
    gameObject.SetActive(false);
}
    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("End");
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
    }
}
}

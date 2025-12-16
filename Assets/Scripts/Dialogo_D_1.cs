using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogo_D_1 : MonoBehaviour
{
    public GameObject Dpainel;
    private bool jogadorPerto = false;
void Start()
{
    Dpainel.SetActive(false);
}
    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            Dpainel.SetActive(true);
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
            if (Dpainel != null)
            {
            Dpainel.SetActive(false);
            }
        }
    }


}

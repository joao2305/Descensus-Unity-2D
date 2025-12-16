using UnityEngine;
using UnityEngine.SceneManagement;

public class paredeinvisivel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Boss 1");
            Debug.Log("Colidiu!");
        }
    }
}


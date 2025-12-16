using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPoisonDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("game over");
        }
    }

}

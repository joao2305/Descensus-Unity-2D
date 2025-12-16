using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class startmenuscript : MonoBehaviour
{
    public void OnstartClick()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnmenuClick()
    {
        SceneManager.LoadScene("startscene");
    }

    public void OndicasClick()
    {
        SceneManager.LoadScene("Dicas");
    }

    public void OnExitClick()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; 
        #else
        Application.Quit(); 
        #endif
    }
}
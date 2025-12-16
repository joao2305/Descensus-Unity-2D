using UnityEngine;

public class HabilidadeManager : MonoBehaviour
{
    public static HabilidadeManager instance;

    public bool dashHabilitado = false;
    public bool puloDuploHabilitado = false;

    void Awake()
    {
if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {

            Destroy(gameObject); 
        }

    }

    public void ComprarDash()
    {
        if (PlayerController.moeda >= 3 && !dashHabilitado)
        {
            PlayerController.moeda -= 3;
            dashHabilitado = true;
        }
    }

    public void ComprarPuloDuplo()
    {
        if (PlayerController.moeda >= 1 && !puloDuploHabilitado)
        {
            PlayerController.moeda -= 1;
            puloDuploHabilitado = true;
        }
    }
}

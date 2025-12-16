using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class gerenciadordehabilits : MonoBehaviour 
{
    public GameObject botaoComprarDash; 
    public GameObject botaoComprarPuloDuplo; 

    public TextMeshProUGUI textoStatusDash; 
    public TextMeshProUGUI textoStatusPuloDuplo; 

    void Start()
    {
        AtualizarEstadoDaUI();
    }


    public void OnClickComprarDash()
    {
        if (HabilidadeManager.instance != null)
        {
            HabilidadeManager.instance.ComprarDash();
            AtualizarEstadoDaUI();
        }
        else
        {
            Debug.LogError("Erro: HabilidadeManager.instance é nulo! O botão não pode funcionar.");
        }
    }

    public void OnClickComprarPuloDuplo()
    {
        if (HabilidadeManager.instance != null)
        {
            HabilidadeManager.instance.ComprarPuloDuplo();
            AtualizarEstadoDaUI();
        }
        else
        {
            Debug.LogError("Erro: HabilidadeManager.instance é nulo! O botão não pode funcionar.");
        }
    }

    public void AtualizarEstadoDaUI()
    {
        if (HabilidadeManager.instance == null)
        {
            Debug.LogWarning("HabilidadeManager ainda não está pronto. A UI da loja não pode ser atualizada.");
            return;
        }


        if (HabilidadeManager.instance.dashHabilitado)
        {
            if (botaoComprarDash != null) botaoComprarDash.SetActive(false); 
            if (textoStatusDash != null) textoStatusDash.text = "DASH COMPRADO!"; 
        }
        else
        {
            if (botaoComprarDash != null) botaoComprarDash.SetActive(true); 
            if (textoStatusDash != null) textoStatusDash.text = "Dash 3 Moedas"; 
        }

        if (HabilidadeManager.instance.puloDuploHabilitado)
        {
            if (botaoComprarPuloDuplo != null) botaoComprarPuloDuplo.SetActive(false); 
            if (textoStatusPuloDuplo != null) textoStatusPuloDuplo.text = "PULO DUPLO COMPRADO!"; 
        }
        else
        {
            if (botaoComprarPuloDuplo != null) botaoComprarPuloDuplo.SetActive(true); 
            if (textoStatusPuloDuplo != null) textoStatusPuloDuplo.text = "Pulo Duplo 1 Moeda"; 
        }
    }
}
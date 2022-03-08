using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour
{
    public float transitionTime;
    public Animator titleLogo;
    public GameObject titleLogoObj;
    public Animator clickToStart;
    public GameObject clickToStartObj;

    void Start()
    {
        Time.timeScale = 0;
    }

    public void ClickToStart()
    {
        StartCoroutine(ClickTransition());
        Time.timeScale = 1;
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/clicked", 1);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/start", 1);
    }

    IEnumerator ClickTransition()
    {
        titleLogo.SetBool("MouseClicked", true);
        clickToStart.SetBool("StartClicked", true);

        yield return new WaitForSeconds(transitionTime);

        clickToStartObj.SetActive(false);
        titleLogoObj.SetActive(false);
    }
}

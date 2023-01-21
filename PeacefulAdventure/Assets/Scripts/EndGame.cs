using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public GameObject text;

    void Start()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame() {
        // show the text
        yield return new WaitForSeconds(0.5f);
        text.TweenAwareEnable();
        // hide the text
        yield return new WaitForSeconds(2f);
        text.TweenAwareDisable();
        // end the game after a short time
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}

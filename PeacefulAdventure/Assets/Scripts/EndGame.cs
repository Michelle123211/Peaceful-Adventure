using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame() {
        // show the TMPro
        // TODO: tween
        yield return new WaitForSeconds(0.5f);
        text.gameObject.SetActive(true);
        // hide the TMPro
        yield return new WaitForSeconds(2f);
        text.gameObject.SetActive(false);
        // end the game after a short time
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}

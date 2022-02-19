using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidOnly : MonoBehaviour
{
    private void Awake() {
        if (!Application.isMobilePlatform) {
            this.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAndroid : MonoBehaviour
{
    private void Awake() {
        if (Application.isMobilePlatform) {
            this.gameObject.SetActive(false);
        }
    }
}

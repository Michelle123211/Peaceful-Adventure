using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidOnly : MonoBehaviour
{
    private void Awake() {
#if !(UNITY_ANDROID || UNITY_EDITOR)
        this.gameObject.SetActive(false);
#endif
    }
}

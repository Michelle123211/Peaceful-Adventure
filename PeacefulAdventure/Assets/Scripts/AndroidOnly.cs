using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidOnly : MonoBehaviour
{
    private void Awake() {
#if UNITY_STANDALONE
        this.gameObject.SetActive(false);
#endif
    }
}

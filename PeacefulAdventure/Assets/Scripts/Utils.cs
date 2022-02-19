using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
    public static Color WithA(this Color c, float a)
        => new Color(c.r, c.g, c.b, a);
    public static Color WithR(this Color c, float r)
        => new Color(r, c.g, c.b, c.a);
    public static Color WithG(this Color c, float g)
        => new Color(c.r, g, c.b, c.a);
    public static Color WithB(this Color c, float b)
        => new Color(c.r, c.g, b, c.a);

    public static Vector2 ConvertToFourDirections(Vector2 inputVector) {
        float xAbs = Mathf.Abs(inputVector.x);
        float yAbs = Mathf.Abs(inputVector.y);
        if (xAbs < 0.1 && yAbs < 0.1)
            return Vector2.zero;
        if (xAbs > yAbs) {
            if (inputVector.x < 0) return Vector2.left;
            else return Vector2.right;
        } else {
            if (inputVector.y < 0) return Vector2.down;
            else return Vector2.up;
        }
    }

    public static List<T> FindObject<T>() where T : Component {
        List<T> result = new List<T>();
        foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects()) {

            AddHidden(root, result);
        }
        return result;
    }

    private static void AddHidden<T>(GameObject root, List<T> result) where T : Component {
        if (root == null) return;
        foreach (T component in root.GetComponents<T>()) {
            result.Add(component);
        }

        for (int i = 0; i < root.transform.childCount; i++) {
            AddHidden(root.transform.GetChild(i).gameObject, result);
        }
    }

    public static void TweenAwareEnable(this GameObject go) {
        Tweenable tweenable = go.GetComponent<Tweenable>();
        if (tweenable != null)
            tweenable.Enable();
        else
            go.SetActive(true);
    }
    public static void TweenAwareDisable(this GameObject go) {
        Tweenable tweenable = go.GetComponent<Tweenable>();
        if (tweenable != null)
            tweenable.Disable();
        else
            go.SetActive(false);
    }
}

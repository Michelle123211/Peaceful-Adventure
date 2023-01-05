using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;



[System.Serializable]
public abstract class TweenProperty<TValue> {
    [Tooltip("If this particular property should be tweened.")]
    public bool tweenThisProperty;

    public void SetTweenedProperty(GameObject defaultTarget, float time) {
        TweenPropertyValues<TValue> tweenPropertyValues = GetTweenPropertyValues();
        SetTweenedPropertyInternal(tweenPropertyValues.tweenTarget != null ? tweenPropertyValues.tweenTarget : defaultTarget, time);
    }
    // generic fields are not serialized so they must be in the derived classes explicitly
    protected abstract TweenPropertyValues<TValue> GetTweenPropertyValues();
    protected abstract void SetTweenedPropertyInternal(GameObject target, float time);
}

[System.Serializable]
public class TweenPropertyPosition : TweenProperty<Vector3> {
    public TweenPropertyValuesVector3 tweenPropertyValues = new TweenPropertyValuesVector3();
    protected override TweenPropertyValues<Vector3> GetTweenPropertyValues() {
        return tweenPropertyValues;
    }
    protected override void SetTweenedPropertyInternal(GameObject target, float time) {
        target.transform.position = tweenPropertyValues.GetTweenValue(time);
    }
}

[System.Serializable]
public class TweenPropertyScale : TweenProperty<Vector3> {
    public TweenPropertyValuesVector3 tweenPropertyValues = new TweenPropertyValuesVector3();
    protected override TweenPropertyValues<Vector3> GetTweenPropertyValues() {
        return tweenPropertyValues;
    }
    protected override void SetTweenedPropertyInternal(GameObject target, float time) {
        target.transform.localScale = tweenPropertyValues.GetTweenValue(time);
    }
}

[System.Serializable]
public class TweenPropertyAlpha : TweenProperty<float> {
    public TweenPropertyValuesFloat tweenPropertyValues = new TweenPropertyValuesFloat();
    protected override TweenPropertyValues<float> GetTweenPropertyValues() {
        return tweenPropertyValues;
    }
    protected override void SetTweenedPropertyInternal(GameObject target, float time) {
        var canvasGroup = target.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
            canvasGroup.alpha = tweenPropertyValues.GetTweenValue(time);
        else
            Debug.LogWarning($"Target object ({target.name}) for tweening the alpha does not have a CanvasGroup component.");
    }
}

[System.Serializable]
public class TweenPropertyColor : TweenProperty<Color> {
    public TweenPropertyValuesColor tweenPropertyValues = new TweenPropertyValuesColor();
    protected override TweenPropertyValues<Color> GetTweenPropertyValues() {
        return tweenPropertyValues;
    }
    protected override void SetTweenedPropertyInternal(GameObject target, float time) {
        Color currentColor = tweenPropertyValues.GetTweenValue(time);
        var spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) {
            spriteRenderer.color = currentColor;
            return;
        }
        var image = target.GetComponent<Image>();
        if (image != null) {
            image.color = currentColor;
            return;
        }
        var light = target.GetComponent<Light2D>();
        if (light != null) {
            light.color = currentColor;
            return;
        }
        var tmpro = target.GetComponent<TextMeshProUGUI>();
        if (tmpro != null) {
            tmpro.color = currentColor;
            return;
        }
        var text = target.GetComponent<Text>();
        if (text != null) {
            text.color = currentColor;
            return;
        }
        Debug.LogWarning($"Target object ({target.name}) for tweening the color does not have a suitable component.");
    }
}

[System.Serializable]
public class TweenPropertyIntensity : TweenProperty<float> {
    public TweenPropertyValuesFloat tweenPropertyValues = new TweenPropertyValuesFloat();
    protected override TweenPropertyValues<float> GetTweenPropertyValues() {
        return tweenPropertyValues;
    }

    protected override void SetTweenedPropertyInternal(GameObject target, float time) {
        var light = target.GetComponent<Light2D>();
        if (light != null) {
            light.intensity = tweenPropertyValues.GetTweenValue(time);
        } else {
            Debug.LogWarning($"Target object ({target.name}) for tweening the intensity does not have a Light2D component.");
        }
    }
}





[System.Serializable]
public abstract class TweenPropertyValues<TValue> {
    public TValue startValue;
    public TValue endValue;
    public TweenVectorCurves tweenCurves;
    [Tooltip("Set only if the target object is different than the one with the GenericTween component.")]
    public GameObject tweenTarget;

    public TweenPropertyValues() {
        tweenCurves = new TweenVectorCurves(NumOfComponents);
    }

    public abstract int NumOfComponents { get; }
    public abstract TValue GetTweenValue(float t);
}

[System.Serializable]
public class TweenPropertyValuesVector3 : TweenPropertyValues<Vector3> {
    public override int NumOfComponents => 3;

    public override Vector3 GetTweenValue(float t) {
        if (tweenCurves.tweenComponentWise) {
            float newX = startValue.x + tweenCurves.tweenCurves[0].Evaluate(t) * (endValue.x - startValue.x);
            float newY = startValue.y + tweenCurves.tweenCurves[1].Evaluate(t) * (endValue.y - startValue.y);
            float newZ = startValue.z + tweenCurves.tweenCurves[2].Evaluate(t) * (endValue.z - startValue.z);
            return new Vector3(newX, newY, newZ);
        } else {
            return startValue + tweenCurves.tweenCurve.Evaluate(t) * (endValue - startValue);
        }
    }
}

[System.Serializable]
public class TweenPropertyValuesFloat : TweenPropertyValues<float> {
    public override int NumOfComponents => 1;

    public override float GetTweenValue(float t) {
        return startValue + tweenCurves.tweenCurve.Evaluate(t) * (endValue - startValue);
    }
}

[System.Serializable]
public class TweenPropertyValuesColor : TweenPropertyValues<Color> {
    public override int NumOfComponents => 4;

    public override Color GetTweenValue(float t) {
        if (tweenCurves.tweenComponentWise) {
            float newR = startValue.r + tweenCurves.tweenCurves[0].Evaluate(t) * (endValue.r - startValue.r);
            float newG = startValue.g + tweenCurves.tweenCurves[1].Evaluate(t) * (endValue.g - startValue.g);
            float newB = startValue.b + tweenCurves.tweenCurves[2].Evaluate(t) * (endValue.b - startValue.b);
            float newA = startValue.a + tweenCurves.tweenCurves[03].Evaluate(t) * (endValue.a - startValue.a);
            return new Color(newR, newG, newB, newA);
        } else {
            return startValue + tweenCurves.tweenCurve.Evaluate(t) * (endValue - startValue);
        }
    }
}





[System.Serializable]
public class TweenVectorCurves {
    // TODO: if tweenComponentWise == false, show tweenCurve, otherwise tweenCurves
    [ConditionalHide("tweenComponentWise", true)]
    public AnimationCurve tweenCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    // TODO: show only if numOfComponents is > 1
    public bool tweenComponentWise; // true = work with the position as a vector, false = tween each axis separately
    [ConditionalHide("tweenComponentWise")]
    public AnimationCurve[] tweenCurves;

    public TweenVectorCurves(int numOfComponents = 3) {
        tweenCurves = new AnimationCurve[numOfComponents];
        for (int i = 0; i < numOfComponents; ++i) {
            tweenCurves[i] = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }
    }
}
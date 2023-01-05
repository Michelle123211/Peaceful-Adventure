using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;
using DG.Tweening.Core;

public class GenericTween : MonoBehaviour {
    public float duration = 0.5f;
    [Tooltip("If the object should be destroyed when the tween is complete.")]
    public bool destroy;
    [Tooltip("If the object should be enabled at the start and disabled when the untween is complete.")]
    public bool enable;
    [Tooltip("After starting the tween, loop it indefinitely.")]
    public bool loop = false;
    [Tooltip("If the tween is started immediately from the start of the object.")]
    public bool playOnAwake = false;
    [Tooltip("If the tween should be reverted immediately after finishing.")]
    public bool revertAfter = false;

    // position
    public TweenPropertyPosition positionTween = new TweenPropertyPosition();
    // scale
    public TweenPropertyScale scaleTween = new TweenPropertyScale();
    // alpha through CanvasGroup
    public TweenPropertyAlpha alphaTween = new TweenPropertyAlpha();
    // color through SpriteRenderer or Image or Light2D or TextMeshProUGUI or Text
    public TweenPropertyColor colorTween = new TweenPropertyColor();
    // intensity through Light2D
    public TweenPropertyIntensity intensityTween = new TweenPropertyIntensity();

    public UnityEvent onTweenComplete = null;
    public UnityEvent onUntweenComplete = null;


    private bool tweenIsRunning = false;
    private bool reversed = false;

    private float time = 0;

    public void DoTween() {
        tweenIsRunning = true;
        reversed = false;
        InitializeTween();
    }

    public void UndoTween() {
        tweenIsRunning = true;
        reversed = true;
        FinalizeTween();
    }

    private void InitializeTween() {
        // set initial values
        time = 0;
        float t = reversed ? 1 : 0;
        if (enable && !reversed)
            gameObject.SetActive(true);
        TweenProperties(t);
    }

    private void FinalizeTween() {
        // set final values
        time = 0;
        float t = reversed ? 0 : 1;
        if (enable && reversed && !loop)
            gameObject.SetActive(false);
        TweenProperties(t);
    }

    private void Awake() {
        if (playOnAwake)
            DoTween();
    }

    private void Update() {
        if (tweenIsRunning) {
            time += Time.deltaTime;
            if (time >= duration) {
                // final values
                tweenIsRunning = false;
                FinalizeTween();
                if (!reversed && revertAfter) {
                    tweenIsRunning = true;
                    reversed = true;
                    return;
                }
                if (loop) {
                    tweenIsRunning = true;
                    reversed = false;
                    return;
                }
                if (destroy) Invoke(nameof(DestroySelf), 0.5f);
            } else {
                // perform a single step
                float t = time / duration; // normalize
                if (reversed) t = 1 - t;
                TweenProperties(t);
            }
        }
    }

    private void TweenProperties(float time) {
        TweenProperty(positionTween, time);
        TweenProperty(alphaTween, time);
        TweenProperty(scaleTween, time);
        TweenProperty(colorTween, time);
        TweenProperty(intensityTween, time);
    }

    private void TweenProperty<TValue>(TweenProperty<TValue> tweenProperty, float time) {
        if (tweenProperty.tweenThisProperty)
            tweenProperty.SetTweenedProperty(gameObject, time);
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

}


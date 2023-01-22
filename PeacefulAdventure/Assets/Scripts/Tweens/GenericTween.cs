using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;
using DG.Tweening.Core;

public class GenericTween : MonoBehaviour {
    [Header("Parameters")]
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

    [Header("Tweened properties")]
    // position
    [Tooltip("Start and end positions are given as relative displacements.")]
    public TweenPropertyPosition positionTween = new TweenPropertyPosition();
    // scale
    public TweenPropertyScale scaleTween = new TweenPropertyScale();
    // alpha through CanvasGroup
    [Tooltip("Affects alpha property of the CanvasGroupt component.")]
    public TweenPropertyAlpha alphaTween = new TweenPropertyAlpha();
    // color through SpriteRenderer or Image or Light2D or TextMeshProUGUI or Text
    [Tooltip("Affects color of the SpriteRenderer, Image, Light2D, TextMeshProUGUI or Text component.")]
    public TweenPropertyColor colorTween = new TweenPropertyColor();
    // intensity through Light2D
    [Tooltip("Affects intensity of the Light2D component.")]
    public TweenPropertyIntensity intensityTween = new TweenPropertyIntensity();

    [Header("Callbacks")]
    public UnityEvent onTweenComplete = null;
    public UnityEvent onUntweenComplete = null;

    private Vector3 initialPosition;


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
        InitializeTween();
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
        positionTween.SetInitialPosition(transform.position);
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
                if (reversed && !revertAfter) { // untween complete
                    if (!Utils.IsNullEvent(onUntweenComplete)) onUntweenComplete.Invoke();
                }
                if ((!reversed) || (reversed && revertAfter)) // tween complete
                    if (!Utils.IsNullEvent(onTweenComplete)) onTweenComplete.Invoke();
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


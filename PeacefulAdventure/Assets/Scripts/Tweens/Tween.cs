using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;
using DG.Tweening.Core;

public abstract class Tween : MonoBehaviour
{
    public float duration;

    public UnityEvent onTweenComplete = null;
    public UnityEvent onUntweenComplete = null;

    public void DoTween() {
        var tween = GetTween();
        if (tween == null) {
            Debug.LogWarning("Tween.GetTween returned null.");
        } else {
            tween.OnComplete(() => {
                if (!Utils.IsNullEvent(onTweenComplete)) onTweenComplete.Invoke();
            });
        }
    }

    public void UndoTween() {
        var tween = GetUntween();
        if (tween == null) {
            Debug.LogWarning("Tween.GetUntween returned null.");
        } else {
            tween.OnComplete(() => {
                if (!Utils.IsNullEvent(onUntweenComplete)) onUntweenComplete.Invoke();
            });
        }
    }

    protected abstract TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> GetTween();
    protected abstract TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> GetUntween();

}

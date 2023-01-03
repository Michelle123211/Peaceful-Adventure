using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;
using DG.Tweening.Core;


public class AlphaTween : Tween
{
    public bool enable = true;
    public bool destroy = false;

    private CanvasGroup canvasGroup;

    protected override TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> GetTween() {
        // fade in
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        if (enable) gameObject.SetActive(true);
        return canvasGroup.DOFade(1f, duration).SetEase(Ease.InOutQuad);
    }

    protected override TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> GetUntween() {
        // fade out
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        return canvasGroup.DOFade(0f, duration).SetEase(Ease.InOutQuad).OnComplete(() => {
            if (enable) gameObject.SetActive(false);
            if (destroy) Invoke(nameof(DestroySelf), 0.5f);
        });
    }

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }
}

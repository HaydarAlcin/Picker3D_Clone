using System.Collections;
using System.Collections.Generic;
using Data.ValueObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private new Renderer renderer;
    [SerializeField] private TextMeshPro scaleText;
    //[SerializeField] private ParticleSystem confetti;

    #endregion

    #region Private Variables

    [SerializeField] private PlayerMeshData _data;

    #endregion

    internal void SetData(PlayerMeshData data)
    {
        _data = data;
    }

    internal void ScaleUpPlayer()
    {
        renderer.gameObject.transform.DOScaleX(_data.ScaleCounter, 1).SetEase(Ease.Flash);
    }

    internal void ShowUpText()
    {
        scaleText.gameObject.SetActive(true);
        scaleText.DOFade(1, 0f).SetEase(Ease.Flash).OnComplete(() => scaleText.DOFade(0, 0).SetDelay(.65f));
        scaleText.rectTransform.DOAnchorPosY(.85f, .65f).SetRelative(true).SetEase(Ease.OutBounce).OnComplete(() =>
            scaleText.rectTransform.DOAnchorPosY(-.85f, .65f).SetRelative(true));
    }

    //internal void PlayConfetti()
    //{
    //    confetti.Play();
    //}

    internal void OnReset()
    {
        renderer.gameObject.transform.DOScaleX(1, 0.5f);
    }
}

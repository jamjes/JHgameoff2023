using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderSpriteEffects : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private void OnEnable()
    {
        KillzoneEventManager.OnDeathEnter += SetColorToRed;
    }

    private void OnDisable()
    {
        KillzoneEventManager.OnDeathEnter -= SetColorToRed;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #region Event Functions

    private void SetColorToRed()
    {
        _spriteRenderer.color = Color.red;
    }

    #endregion
}

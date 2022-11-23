using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleShield : MonoBehaviour
{
    private const string TextureName = "_MainTex";
    [SerializeField] private Material _material;
    [SerializeField] private List<Texture> _appearanceTextures;

    private Coroutine _coroutine;
    private bool _isActive;

    private void OnEnable()
    {
        _isActive = true;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ActivateShield());
    }

    public void DeactivateShield()
    {
        _isActive = false;
    }

    private IEnumerator ActivateShield()
    {
        int textureIndex = 0;
        float horizontalStep = 0.01f;
        float verticalStep = 0.015f;
        float horizontalValue = 0;
        float verticalValue = 0;
        Vector2 currentOffsetValue = new Vector2(horizontalValue, verticalValue);
        _material.SetTextureOffset(TextureName, currentOffsetValue);
        _material.SetTexture(TextureName, _appearanceTextures[textureIndex++]);

        while (_isActive)
        {
            if (horizontalValue >= 1.5f || horizontalValue <= -1.5f)
            {
                horizontalStep = -horizontalStep;
                verticalStep = -verticalStep;
            }

            horizontalValue += horizontalStep;
            verticalValue += verticalStep;
            currentOffsetValue = new Vector2(horizontalValue, verticalValue);
            _material.SetTextureOffset(TextureName, currentOffsetValue);

            if (textureIndex < _appearanceTextures.Count)
                _material.SetTexture(TextureName, _appearanceTextures[textureIndex++]);

            yield return new WaitForSeconds(0.045f);
        }

        while (_material.GetTexture(TextureName) != _appearanceTextures[0])
        {
            if (horizontalValue >= 1.5f || horizontalValue <= -1.5f)
            {
                horizontalStep = -horizontalStep;
                verticalStep = -verticalStep;
            }

            horizontalValue += horizontalStep;
            verticalValue += verticalStep;
            currentOffsetValue = new Vector2(horizontalValue, verticalValue);
            _material.SetTextureOffset(TextureName, currentOffsetValue);

            if (textureIndex >= 0)
                _material.SetTexture(TextureName, _appearanceTextures[--textureIndex]);

            yield return new WaitForSeconds(0.045f);
        }

        gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;

public class SpireAbility : MonoBehaviour
{
    [SerializeField] private float _abilityDuration;
    [SerializeField] private UltimateButton _abilityButton;
    [SerializeField] private CastleShield _castleShield;

    private bool _isActive;
    private float _leftTiime;

    public event Action SpellActivated;
    public event Action SpellOvered;

    private void OnEnable()
    {
        _abilityButton.ButtonClicked += ActivateAbility;
    }

    private void Update()
    {
        if (_isActive && _leftTiime >= _abilityDuration)
            DeactivateAbility();

        _leftTiime += Time.deltaTime;
    }

    private void OnDisable()
    {
        _abilityButton.ButtonClicked -= ActivateAbility;
    }

    public void DeactivateAbility()
    {
        _isActive = false;
        _castleShield.DeactivateShield();
        SpellOvered?.Invoke();
    }

    private void ActivateAbility()
    {
        _isActive = true;
        _leftTiime = 0;
        _castleShield.gameObject.SetActive(true);
        SpellActivated?.Invoke();
    }
}

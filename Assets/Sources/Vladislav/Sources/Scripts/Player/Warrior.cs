using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : MonoBehaviour
{
    [SerializeField] private Weapon _weaponPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private AnimationHandler _animationHandler;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _ultimateTime;

    private float _currentCooldown;
    private float _currentUltimateTime;
    private Enemy _currentEnemy;
    private bool _canAttack;
    private float _currentTime;
    private UltimateButton _ultimateButton;

    public event Action NeedActivateAbility;
    public event Action<Weapon> Shot;
    public Func<Sprite> NeedIcon;

    public bool IsBusy => _currentEnemy != null;

    private void OnEnable()
    {
        _animationHandler.NeedShoot += Shoot;
        _currentCooldown = _cooldown;
    }

    private void Update()
    {
        if (_currentTime >= _currentCooldown)
        {
            _currentTime = 0;
            _canAttack = true;

            if (_currentEnemy != null)
                Attack(_currentEnemy);
        }

        if (_currentUltimateTime >= _ultimateTime)
            _currentCooldown = _cooldown;

        _currentTime += Time.deltaTime;
        _currentUltimateTime += Time.deltaTime;
    }

    private void OnDisable()
    {
        _animationHandler.NeedShoot -= Shoot;
        _ultimateButton.ButtonClicked -= ActivateAbility;
    }

    public void Inizialize(UltimateButton ultimateButton)
    {
        _canAttack = true;
        _currentTime = _currentCooldown;
        _ultimateButton = ultimateButton;
        _ultimateButton.ButtonClicked += ActivateAbility;
    }

    public void Attack(Enemy enemy)
    {
        if (_canAttack)
        {
            _canAttack = false;
            _currentEnemy = enemy;
            _currentEnemy.Dead += OnEnemyDead;
            _rotator.PrepairRotate(_currentEnemy.transform.position);
            _animationHandler.ActivateAttackAnimation();
        }
    }

    public Sprite GetUltimateIcon()
    {
        return NeedIcon?.Invoke();
    }

    public void ActivateUltimate(bool isParametersChange)
    {
        if (isParametersChange)
            ChangeParameters();
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        _currentEnemy = null;
        _canAttack = false;
    }

    private void ChangeParameters()
    {
        _currentCooldown = 1;
        _currentUltimateTime = 0;
        _currentTime = _currentCooldown;
    }

    private void Shoot()
    {
        if (_currentEnemy != null)
        {
            Weapon tempWeapon = Instantiate(_weaponPrefab);
            tempWeapon.transform.position = _spawnPosition.position;
            tempWeapon.PrepairFly(_currentEnemy);

            Shot?.Invoke(tempWeapon);
        }
    }

    private void ActivateAbility()
    {
        NeedActivateAbility?.Invoke();
    }
}

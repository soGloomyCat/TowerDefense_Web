using System.Collections.Generic;
using UnityEngine;
using static WarriorLevelStats;

public class Warrior : MonoBehaviour
{
    [SerializeField] private Weapon _weaponPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private List<AnimationHandler> _animationHandlers;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private UltimateAbility _ultimateAbility;
    [SerializeField] private WarriorLevelStats _levelStats;

    private AnimationHandler _currentHandler;
    private float _currentCooldown;
    private float _tempCooldown;
    private float _currentTime;
    private float _currentDamageMultiplier;
    private Enemy _currentEnemy;
    private bool _canAttack;
    private UltimateButton _ultimateButton;
    private Holder _currentHolder;

    public bool IsBusy => _currentEnemy != null;

    private void OnEnable()
    {
        foreach (var handler in _animationHandlers)
            handler.NeedShoot += Shoot;

        if (_levelStats != null)
        {
            _levelStats.Inizialize();
            SetParameters(_levelStats.GetCurrentLevelStats());
        }
        else
        {
            _currentHandler = _animationHandlers[0];
            _currentCooldown = 1;
            _currentDamageMultiplier = 1;
        }
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

        _currentTime += Time.deltaTime;
    }

    private void OnDisable()
    {
        foreach (var handler in _animationHandlers)
            handler.NeedShoot -= Shoot;

        _ultimateButton.ButtonClicked -= ActivateAbility;
    }

    public void Inizialize(UltimateButton ultimateButton, Holder holder)
    {
        _canAttack = true;
        _ultimateButton = ultimateButton;
        _ultimateButton.ButtonClicked += ActivateAbility;
        _currentHolder = holder;
    }

    public void Attack(Enemy enemy)
    {
        if (_canAttack)
        {
            _canAttack = false;
            _currentEnemy = enemy;
            _currentEnemy.Dead += OnEnemyDead;
            _rotator.PrepairRotate(_currentEnemy.transform.position);
            _currentHandler.ActivateAttackAnimation();
        }
    }

    public Sprite GetUltimateIcon()
    {
        return _ultimateAbility.GetIcon();
    }

    public void UpgradeParameters()
    {
        _levelStats.LevelUp();
        SetParameters(_levelStats.GetCurrentLevelStats());
    }

    public void AccelerateAttackSpeed(float currentCooldown)
    {
        _tempCooldown = _currentCooldown;
        _currentCooldown = currentCooldown;
    }

    public void ResumeAttackSpeed()
    {
        _currentCooldown = _tempCooldown;
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        _currentEnemy = null;
        _canAttack = false;
    }

    private void Shoot()
    {
        if (_currentEnemy != null)
        {
            Weapon tempWeapon = Instantiate(_weaponPrefab, _currentHolder.TrashBox);
            tempWeapon.transform.position = _spawnPosition.position;
            tempWeapon.PrepairFly(_currentEnemy, _currentDamageMultiplier);
        }
    }

    private void ActivateAbility()
    {
        if (_ultimateAbility is ElfSpeed)
            _ultimateAbility.Use(this);
        else if (_ultimateAbility is WallCreator)
            _ultimateAbility.Use(_currentHolder.CurrentCastleTarget);
        else if (_ultimateAbility is MeteoriteCreator)
            _ultimateAbility.Use(_currentHolder.MeteoriteSpawnPoint, _currentHolder.TrashBox);
    }

    private void SetParameters(Stats levelStats)
    {
        _currentCooldown = levelStats.Cooldown;
        _currentDamageMultiplier = levelStats.DamageMultiplier;
        ChangeSkin(levelStats.SkinIndex);
    }

    private void ChangeSkin(int skinIndex)
    {
        for (int i = 0; i < _animationHandlers.Count; i++)
        {
            if (i == skinIndex)
            {
                _animationHandlers[i].gameObject.SetActive(true);
                _currentHandler = _animationHandlers[i];
            }
            else
            {
                _animationHandlers[i].gameObject.SetActive(false);
            }
        }
    }
}

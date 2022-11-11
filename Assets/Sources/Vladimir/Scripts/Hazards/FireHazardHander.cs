using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FireRamp))]
public class FireHazardHander : MonoBehaviour, IHazardHandler
{
    [SerializeField] private EnemyHealth _enemyHealth;

    private FireRamp _fireRamp;

    private void Awake()
    {
        _fireRamp = GetComponent<FireRamp>();
    }

    public void Handle(Hazard hazard)
    {
        if (hazard.TryGetComponent(out HazardFire fireHazard))
        {
            _fireRamp.Activate();
            StartCoroutine(Influence(fireHazard));
        }
    }

    private IEnumerator Influence(HazardFire hazardFire)
    {
        float timeBufer = 0;
        float tickBufer = 0;

        while (timeBufer < hazardFire.Duration)
        {
            timeBufer += Time.deltaTime;
            tickBufer += Time.deltaTime;

            if (tickBufer >= 1)
            { 
                tickBufer = 0;
                Tick(hazardFire.DamagePerSecond);
            }

            yield return null;
        }

        _fireRamp.Deactivate();
    }

    private void Tick(float damage)
    {
        _enemyHealth.TakeDamage(damage);
    }
}

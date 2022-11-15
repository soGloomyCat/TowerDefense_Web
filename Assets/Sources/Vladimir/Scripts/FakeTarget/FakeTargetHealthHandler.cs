using UnityEngine;

public class FakeTargetHealthHandler : MonoBehaviour
{
    [SerializeField] private Transform _model;

    public void OnHealthChange(float currentHealth)
    { 
        //тут можно как - то реагировать на изменение здоровья цели,
        //изменять модель или эффекты
    }
}

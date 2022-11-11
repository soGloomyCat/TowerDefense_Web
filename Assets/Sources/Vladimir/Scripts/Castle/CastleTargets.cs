using UnityEngine;
using UnityEngine.Events;

public class CastleTargets : MonoBehaviour
{
    [SerializeField] private Transform _castle;
    [SerializeField] private FakeTargtesList _targtes;

    private bool _isAnyTargetActive;

    public Transform Castle => _castle;

    public event UnityAction<Transform> TargetChanged;

    public void EnableAnotherTarget()
    {
        if (_isAnyTargetActive)
            return;

        _isAnyTargetActive = true;
        FakeTarget target = _targtes.EnableRandomTarget();
        target.TargetDestroyed += OnTargetDestroyed;
        TargetChanged?.Invoke(target.transform);
    }

    private void OnTargetDestroyed(FakeTarget target)
    { 
        target.TargetDestroyed -= OnTargetDestroyed;
        target.Hide();
        _isAnyTargetActive = false;
        TargetChanged?.Invoke(_castle);
    }

    public void ResetTargets()
    { 
        _targtes.ResetTargetList();
        _isAnyTargetActive = false;
    }
}

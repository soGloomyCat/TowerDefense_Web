using UnityEngine;

public class FakeTargtesList : MonoBehaviour
{
    private FakeTarget[] _fakeTargets;

    private void Awake()
    {
        _fakeTargets = GetComponentsInChildren<FakeTarget>();
    }

    public void ResetTargetList()
    {
        foreach (FakeTarget target in _fakeTargets)
            target.ResetTarget();
    }

    public FakeTarget EnableRandomTarget()
    { 
        FakeTarget fakeTarget = _fakeTargets[Random.Range(0, _fakeTargets.Length)];
        fakeTarget.Show();
        return fakeTarget;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class FakeTargtesList : MonoBehaviour
{
    //private FakeTarget[] _fakeTargets;
    private List<FakeTarget> _fakeTargets = new List<FakeTarget>();

    private async void Awake()
    {
        //_fakeTargets = GetComponentsInChildren<FakeTarget>();

        for (int i = 0; i < transform.childCount; i++)
        { 
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent(out FakeTarget fakeTarget))
            {
                _fakeTargets.Add(fakeTarget);
                fakeTarget.gameObject.SetActive(false);
            }
        }
    }

    public void ResetTargetList()
    {
        foreach (FakeTarget target in _fakeTargets)
            target.ResetTarget();
    }

    public FakeTarget EnableRandomTarget()
    { 
        FakeTarget fakeTarget = _fakeTargets[Random.Range(0, _fakeTargets.Count)];
        fakeTarget.Show();
        return fakeTarget;
    }
}

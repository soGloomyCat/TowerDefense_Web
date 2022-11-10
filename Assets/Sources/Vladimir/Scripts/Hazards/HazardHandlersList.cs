using UnityEngine;

public class HazardHandlersList : MonoBehaviour
{
    private IHazardHandler[] _handlers;

    private void Awake()
    {
        _handlers = GetComponentsInChildren<IHazardHandler>();
    }

    public void Handle(Collider other)
    {
        if (other.TryGetComponent(out Hazard hazard))
        {
            foreach (IHazardHandler handler in _handlers)
                handler.Handle(hazard);
        }
    }
}

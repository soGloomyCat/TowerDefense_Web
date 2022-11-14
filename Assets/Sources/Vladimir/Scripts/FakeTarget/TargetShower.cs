using UnityEngine;

public abstract class TargetShower : MonoBehaviour
{
    public abstract void Show();
    public abstract void Hide();
    public virtual void ResetShower()
    { }
}

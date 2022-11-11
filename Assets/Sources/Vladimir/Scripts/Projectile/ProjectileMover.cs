using System.Collections;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Coroutine _movement;

    //public event UnityAction TargetReached;

    public void Move(Vector3 target)
    {
        _movement = StartCoroutine(Movement(target));
        //StartCoroutine(MovementSlerp(target));
    }

    public void Deactivate()
    {
        if (_movement != null)
        { 
            StopCoroutine(_movement);
            _movement = null;
        }
    }

    private IEnumerator Movement(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.001f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
            transform.rotation = Quaternion.LookRotation(target - transform.position);
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator MovementSlerp(Vector3 target)
    {
        float lerp = 0;
        Vector3 start = transform.position;

        while (lerp < 1)
        {
            transform.rotation = Quaternion.LookRotation(target - transform.position);
            transform.position = Vector3.Slerp(start, target, lerp);
            lerp += Time.deltaTime;
            yield return null;
        }

        //TargetReached?.Invoke();
    }
}

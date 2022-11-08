using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event UnityAction TargetReached;

    public void Move(Vector3 target)
    {
        StartCoroutine(Movement(target));
        //StartCoroutine(MovementSlerp(target));
    }

    private IEnumerator Movement(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.001f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
            transform.rotation = Quaternion.LookRotation(target - transform.position);
            yield return null;
        }

        TargetReached?.Invoke();
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

        TargetReached?.Invoke();
    }
}

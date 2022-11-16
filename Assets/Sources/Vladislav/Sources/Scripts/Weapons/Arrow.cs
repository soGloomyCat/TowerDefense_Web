using System.Collections;
using UnityEngine;

public class Arrow : Weapon
{
    protected override IEnumerator Fly(Enemy enemy)
    {
        while (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, Speed * Time.deltaTime);
            transform.LookAt(enemy.transform);
            yield return null;
        }

        Destroy(gameObject);
    }
}

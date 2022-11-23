using System.Collections;
using UnityEngine;

public class Arrow : Weapon
{
    protected override IEnumerator Fly(Enemy enemy)
    {
        Vector3 tempDirection = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1f, enemy.transform.position.z);

        while (enemy != null && Vector3.Distance(transform.position, tempDirection) >= 0.1f)
        {
            tempDirection = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1f, enemy.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, tempDirection, Speed * Time.deltaTime);
            transform.LookAt(tempDirection);
            yield return null;
        }

        Destroy(gameObject);
    }
}

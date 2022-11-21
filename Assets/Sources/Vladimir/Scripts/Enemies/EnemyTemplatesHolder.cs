using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTemplatesHolder", menuName = "GameAssets/EnemyTemplatesHolder")]
public class EnemyTemplatesHolder : ScriptableObject
{
    [SerializeField] private Enemy[] _templates;

    public Enemy GetRandomTemplate()
    {
        return _templates[Random.Range(0, _templates.Length)];
    }
}

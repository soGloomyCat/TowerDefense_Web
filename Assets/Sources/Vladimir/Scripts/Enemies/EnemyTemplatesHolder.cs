using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTemplatesHolder", menuName = "GameAssets/EnemyTemplatesHolder")]
public class EnemyTemplatesHolder : ScriptableObject
{
    [SerializeField] private Enemy[] _templates;
    [SerializeField] private Enemy[] _templatesBosses;

    public Enemy GetRandomTemplate()
    {
        return _templates[Random.Range(0, _templates.Length)];
    }

    public Enemy GetRandomTemplateBoss()
    {
        return _templatesBosses[Random.Range(0, _templatesBosses.Length)];
    }
}

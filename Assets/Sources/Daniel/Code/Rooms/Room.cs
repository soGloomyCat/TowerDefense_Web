#define DEBUG_BOUNDS

using UnityEngine;
using TowerDefense.Daniel.Interfaces;
using System;
using TowerDefense.Daniel.Models;

namespace TowerDefense.Daniel
{
    public abstract class Room : MonoBehaviour, IReadOnlyRoom
    {
        [SerializeField] private RoomMarketInformation _information = null;
        [SerializeField] private float _previewPadding = 0;

        public event Action<IReadOnlyRoom> Upgraded = null;

        [field: SerializeField] public bool NeedConcreteHolder { get; private set; } = false;
        public int Level { get; private set; } = 0;
        public RoomMarketInformation Information => _information;

        public void FocusIn()
        {

        }

        public void FocusOut()
        {

        }

        public void Accept(IUnit unit)
        {

        }
        
        public Sprite GetPreview(Vector2Int size)
        {
            RuntimePreviewGenerator.PreviewDirection = Vector3.forward;

            RuntimePreviewGenerator.Padding = _previewPadding;
            var preview = RuntimePreviewGenerator.GenerateModelPreview(transform, size.x, size.y, true);

            return Sprite.Create(preview, new Rect(0, 0, size.x, size.y), new Vector2(0.5f, 0.5f));
        }

        public void Upgrade()
        {
            Level = Mathf.Clamp(Level + 1, 0, MaxLevel);

            Upgraded?.Invoke(this);
        }

        protected virtual int MaxLevel => _information.UpgradePrices.Count;
    }
}

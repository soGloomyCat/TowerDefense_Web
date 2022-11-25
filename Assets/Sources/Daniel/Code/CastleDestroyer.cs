using System;
using UnityEngine;

namespace TowerDefense.Daniel
{
    [RequireComponent(typeof(Castle))]
    public class CastleDestroyer : MonoBehaviour
    {
        [SerializeField] private CastleHealth _health = null;
        [SerializeField] private AdHandler _adHandler = null;
        [SerializeField] private BattleCanvas _battleCanvas = null;

        private Castle _castle = null;

        private void Awake()
        {
            _castle = GetComponent<Castle>();
        }

        private void OnEnable()
        {
            _health.CastleDestroyed += Explode;
            _adHandler.RewardAdFinished += Revive;
            _battleCanvas.PanelButtonClicked += Revive;
        }

        private void OnDisable()
        {
            _health.CastleDestroyed -= Explode;
            _adHandler.RewardAdFinished -= Revive;
            _battleCanvas.PanelButtonClicked -= Revive;
        }

        private void Explode()
        {
            _castle.Explode();
        }

        private void Revive()
        {
            _castle.Revive();
        }

        /*private void OnPanelButtonClicked()
        {
            _castle.Revive(true);
        }*/
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Jelewow.DNK.Farms.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Jelewow.DNK.Player.MonoBehaviours
{
    public class PlayerInfoPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _info;
        [SerializeField] private float _cooldown;

        private Coroutine _aliveRoutine;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void UpdateUI(in Dictionary<Resource, int> resources)
        {
            UpdateText(in resources);
            gameObject.SetActive(true);

            if (_aliveRoutine == null)
            {
                _aliveRoutine = StartCoroutine(Waiting());
            }
            else
            {
                StopCoroutine(_aliveRoutine);
                _aliveRoutine = StartCoroutine(Waiting());
            }
        }

        private void UpdateText(in Dictionary<Resource, int> resources)
        {
            var total = 0;
            var table = "Информация\n";

            foreach (var pair in resources)
            {
                table += $"{pair.Key.Name} {pair.Value}\n";
                total += pair.Value;
            }
        
            table += "------------------\n";
            table += $"Всего ресурсов:\t\t{total}";
        
            _info.text = table;
        }

        private IEnumerator Waiting()
        {
            yield return new WaitForSeconds(_cooldown);
            gameObject.SetActive(false);
            _aliveRoutine = null;
        }
    }
}
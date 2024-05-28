﻿using Assets.Sources.Shared.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class GameObjectSpawnerSystem : MonoBehaviour
    {
        [field: SerializeField]
        [field: Tooltip("List of Spawnable Game Object to pop")]
        public List<SpawnableGameObject> GameObjectsToSpawn { get; private set; }

        [field: SerializeField]
        [field: Tooltip("Container where Game Object is pop")]
        public GameObject Container { get; private set; }

        [field: SerializeField]
        [field: Range(0f, float.MaxValue)]
        [field: Tooltip("Min time in seconds to pop a new Game Object")]
        public float MinPopInterval { get; private set; } = 1f;

        [field: SerializeField]
        [field: Range(0f, float.MaxValue)]
        [field: Tooltip("Max time in seconds to pop a new Game Object")]
        public float MaxPopInterval { get; private set; } = 3f;

        [field: SerializeField]
        [field: Range(0f, float.MaxValue)]
        [field: Tooltip("Delay in seconds before to start poping")]
        public float PopStartDelay { get; private set; } = 0f;

        private void Awake()
        {
            if (CheckPopIntervalAvailable() && CheckGameObjectToSpawnAvailable())
            {
                StartCoroutine(PopGameObjectCoroutine());
            }
        }

        private IEnumerator PopGameObjectCoroutine()
        {
            yield return new WaitForSeconds(PopStartDelay <= 0f ? 0f : PopStartDelay);

            while (GameObjectsToSpawn.Any())
            {
                float randomPercentage = Random.Range(0f, 100f);
                List<SpawnableGameObject> preSelectedGameObjectsToSpawn = GameObjectsToSpawn.Where(go => go.SpawnPercentage > randomPercentage).ToList();

                SpawnableGameObject gameObjectToSpawn = preSelectedGameObjectsToSpawn[Random.Range(0, preSelectedGameObjectsToSpawn.Count)];

                Instantiate(gameObjectToSpawn.GameObject, transform.position, transform.rotation, Container.transform);

                yield return new WaitForSeconds(Random.Range(MinPopInterval, MaxPopInterval));
            }
        }

        public bool CheckPopIntervalAvailable()
        {
            if (MinPopInterval < 0)
            {
                Debug.Log($"{gameObject.name} : {nameof(MinPopInterval)} cannot be negative");
                return false;
            }

            if (MaxPopInterval < 0)
            {
                Debug.Log($"{gameObject.name} : {nameof(MaxPopInterval)} cannot be negative");
                return false;
            }

            if (MaxPopInterval <= MinPopInterval)
            {
                Debug.Log($"{gameObject.name} : {nameof(MaxPopInterval)} cannot be less or equal than {nameof(MinPopInterval)}");
                return false;
            }

            return true;
        }

        public bool CheckGameObjectToSpawnAvailable()
        {
            if (GameObjectsToSpawn == null || !GameObjectsToSpawn.Any())
            {
                Debug.Log($"{gameObject.name} : {nameof(GameObjectsToSpawn)} cannot be null or empty");
                return false;
            }

            if (Container == null)
            {
                Debug.Log($"{gameObject.name} : {nameof(Container)} cannot be null");
                return false;
            }

            return true;
        }
    }
}

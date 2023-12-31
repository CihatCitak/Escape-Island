﻿using UnityEngine;
using ObjectPooling;

namespace PawnSystem
{
    [DefaultExecutionOrder(-2)]
    public class PawnPool : ObjectPool<Pawn>
    {
        public static PawnPool Instance { get => instance; set => instance = value; }
        private static PawnPool instance;

        protected override void Awake()
        {
            if(instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            base.Awake();
        }

        protected override void DequeueSettings(Pawn pooledObject)
        {
            LevelDataSystem.LevelManager.Instance.AllPoolObjectReturnsPool += () => Enqueue(pooledObject);

            pooledObject.gameObject.SetActive(true);
        }

        protected override void EnqueueSettings(Pawn pooledObject)
        {
            LevelDataSystem.LevelManager.Instance.AllPoolObjectReturnsPool -= () => Enqueue(pooledObject);

            pooledObject.gameObject.SetActive(false);
            pooledObject.transform.parent = transform;
        }
    }
}

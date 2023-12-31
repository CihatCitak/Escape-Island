using ObjectPooling;
using UnityEngine;

namespace FieldSystem
{
    [DefaultExecutionOrder(-2)]
    public class FieldPool : ObjectPool<FieldViewer>
    {
        public static FieldPool Instance { get => instance; set => instance = value; }
        private static FieldPool instance;

        protected override void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            base.Awake();
        }

        protected override void DequeueSettings(FieldViewer pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        protected override void EnqueueSettings(FieldViewer pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            pooledObject.transform.parent = transform;
        }
    }
}

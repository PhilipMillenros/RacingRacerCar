using UnityEngine;

namespace Object_Pool
{
    public class ExpSpawner : MonoBehaviour
    {
        [SerializeField] private int maxExpOrbsOnField;
        [SerializeField] private float yPosition;
        [SerializeField] private float randomDistanceRange;
        private GenericObjectPool<Exp> objectPool;

        private void Awake()
        {
            objectPool = ExpOrbPool.Instance;
            Exp.OnExpPickup += ReCycleOrb;
            for (var i = 0; i < maxExpOrbsOnField; i++) SpawnNewOrb();
        }

        private void ReCycleOrb(Exp orb)
        {
            objectPool.ReturnToPool(orb);
            SpawnNewOrb();
        }

        public void SpawnNewOrb()
        {
            var newOrb = objectPool.Get();
            newOrb.transform.position = new Vector3(Random.Range(0, randomDistanceRange), yPosition,
                Random.Range(0, randomDistanceRange));
            newOrb.transform.rotation = Quaternion.identity;
            newOrb.gameObject.SetActive(true);
        }
    }
}
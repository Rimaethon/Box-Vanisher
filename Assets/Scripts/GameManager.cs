using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetPrefabs;
    public GameObject explosionPrefab;
    private float spawnRate = 1.0f;
    private int currentTargetIndex = 0;
    private Dictionary<Transform, Target> targetMap = new Dictionary<Transform, Target>();

    void Start()
    {

        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            
            Transform targetTransform = targetPrefabs[currentTargetIndex].transform;
            targetTransform.position = RandomSpawnPos();
            targetTransform.gameObject.SetActive(true);

            currentTargetIndex = (currentTargetIndex + 1) % targetPrefabs.Count;
        }
    }
    

    public void ReturnTargetToPool(Transform targetTransform)
    {
        targetTransform.gameObject.SetActive(false);
    }

    public void ReturnExplosionToPool(GameObject explosion)
    {
        explosion.SetActive(false);
    }

    Vector3 RandomSpawnPos()
    {
        float xRange = 4;
        float ySpawnPos = 1;
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 9;
    private float maxSpeed = 12;
    private float maxTorque = 7;
    public int pointValue;

    public ParticleSystem explosionParticle;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false);

        GameObject explosion = Instantiate(gameManager.explosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActive(true);
        explosion.transform.parent = transform;

        StartCoroutine(ReturnExplosionToPool(explosion));
    }

    IEnumerator ReturnExplosionToPool(GameObject explosion)
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before returning the explosion to the pool
        explosion.transform.parent = null;
        gameManager.ReturnExplosionToPool(explosion);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
}

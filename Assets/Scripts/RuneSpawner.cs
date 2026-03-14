using UnityEngine;

public class RuneSpawner : MonoBehaviour
{
    public GameObject runePrefab;

    [SerializeField] private Bounds spawnBounds;
    [SerializeField] private float spawnInterval = 3f;

    private void Start()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(false);

        InvokeRepeating(nameof(SpawnRune), spawnInterval, spawnInterval);
    }

    private void OnValidate()
    {
        if (transform.childCount == 0) return;
        Transform visualizer = transform.GetChild(0);
        visualizer.localPosition = spawnBounds.center;
        visualizer.localScale = spawnBounds.size;
    }

    private void SpawnRune()
    {
        Vector3 position = new(
            Random.Range(spawnBounds.min.x, spawnBounds.max.x),
            Random.Range(spawnBounds.min.y, spawnBounds.max.y),
            0f
        );

        GameObject rune = Instantiate(runePrefab, position, Quaternion.identity);
        rune.GetComponent<RuneController>().Init();
    }
}

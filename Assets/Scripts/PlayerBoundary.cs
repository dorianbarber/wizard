using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerWorldBoundary : MonoBehaviour {
    private Bounds worldBounds;
    private bool boundsInitialized = false;

    void Start() {
        Tilemap[] maps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        if (maps.Length == 0) return;

        // Initialize bounds using the first map's world coordinates
        worldBounds = GetWorldBounds(maps[0]);

        // Expand to include all other maps
        for (int i = 1; i < maps.Length; i++) {
            worldBounds.Encapsulate(GetWorldBounds(maps[i]));
        }
        boundsInitialized = true;
    }

    // Helper to convert local tilemap bounds to world space correctly
    Bounds GetWorldBounds(Tilemap map) {
        Vector3 min = map.localBounds.min;
        Vector3 max = map.localBounds.max;

        // Convert the corners to world space to account for Grid position/scale
        Vector3 worldMin = map.transform.TransformPoint(min);
        Vector3 worldMax = map.transform.TransformPoint(max);

        return new Bounds((worldMin + worldMax) / 2, worldMax - worldMin);
    }

    void LateUpdate() {
        if (!boundsInitialized) return;

        // Clamp position on both X and Y
        float x = Mathf.Clamp(transform.position.x, worldBounds.min.x, worldBounds.max.x);
        float y = Mathf.Clamp(transform.position.y, worldBounds.min.y, worldBounds.max.y);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    // This draws a yellow box in the Scene View so you can debug the boundary!
    void OnDrawGizmos() {
        if (boundsInitialized) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(worldBounds.center, worldBounds.size);
        }
    }
}

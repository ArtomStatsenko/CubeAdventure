using UnityEngine;

public sealed class PlayerSpawner
{
    private GameObject _prefab;
    private Transform _spawnPoint;

    public PlayerSpawner(GameObject prefab, Transform spawnPoint)
    {
        _prefab = prefab;
        _spawnPoint = spawnPoint;
    }

    public PlayerView Spawn()
    {
        GameObject player = Object.Instantiate(_prefab);
        player.transform.position = _spawnPoint.position;
        player.transform.rotation = _spawnPoint.rotation;

        if (player.TryGetComponent(out PlayerView view))
        {
            return view;
        }
        else
        {
            Debug.Log("PlayerView script is missing...");
            return null;
        }
    }
}
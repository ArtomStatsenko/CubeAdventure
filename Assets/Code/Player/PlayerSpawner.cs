using UnityEngine;

public sealed class PlayerSpawner
{
    private GameObject _prefab;
    private Vector3 _spawnPosition;

    public PlayerSpawner(GameObject prefab, Vector3 spawnPosition)
    {
        _prefab = prefab;
        _spawnPosition = spawnPosition;
    }

    public PlayerView Spawn()
    {
        GameObject player = Object.Instantiate(_prefab);
        player.transform.position = _spawnPosition;
        player.transform.rotation = Quaternion.identity;
        var view = player.GetOrAddComponent<PlayerView>();
        return view;
    }
}
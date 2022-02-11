using UnityEngine;

public static class Extentions
{
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        if (!gameObject.TryGetComponent(out T result))
        {
            result = gameObject.AddComponent<T>();
        }

        return result;
    }

    public static Vector3 GetRandomPoint(this Vector3[,] grid)
    {
        var x = Random.Range(0, grid.GetLength(0));
        var y = Random.Range(0, grid.GetLength(1));
        Vector3 result = grid[x, y];
        return result;
    }
}
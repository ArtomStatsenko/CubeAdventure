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
}
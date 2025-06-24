using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    void Awake()
    {
        int count = transform.childCount;
        points = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            points[i] = transform.GetChild(i);
            Debug.Log($"Waypoint {i}: {points[i].position}");
        }
    }


}

using UnityEngine;

public class Example : MonoBehaviour
{
    public GameObject prefab;
    public float space = 1.5f;

    void Start()
    {
        for (var x  = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                for(var z = 0; z < 3; z++)
                { 
                    Vector3[] positions = { new Vector3(x * space, y * space, z * space) };
                    foreach (var pos in positions)
                    {
                        Instantiate(prefab, pos, Quaternion.identity);  
                    }
                }
            }
        }
    }
}

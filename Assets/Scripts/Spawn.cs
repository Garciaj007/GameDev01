using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject waypoints;
    public GameObject[] prefabs;

    Transform[] transforms;

    // Use this for initialization
    void Start () {
        transforms = waypoints.GetComponentsInChildren<Transform>();

        if (transforms[1] != null)
        {
            for (int i = 1; i < transforms.Length; i++)
            {
                int rand = Random.Range(0, prefabs.Length);
                GameObject spawnedObject = Instantiate(prefabs[rand]);
                spawnedObject.transform.position = transforms[i].position;
            }
        }
	}
	
}

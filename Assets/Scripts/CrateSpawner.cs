using UnityEngine;
using System.Collections;

public class CrateSpawner : MonoBehaviour {

	public GameObject terrain;
	private float time;
	private float min, max;

	public GameObject[] crates;
	// Use this for initialization
	void Start () {
		time = 0.0f;
		min = -245.0f;
		max = 245.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= 30) {
			float x = Random.Range(min, max);
			float z = Random.Range(min, max);
			float y = 100;

			Vector3 pos = new Vector3(x,y,z);

			RaycastHit hit;
			// note that the ray starts at 100 units
			Ray ray = new Ray (pos, Vector3.down);
			
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {        
				if (hit.collider != null && hit.collider.gameObject == terrain) {
					Vector3 spawnPos = new Vector3(x, hit.point.y + 1.0f, z);
					int crateNum = Random.Range(0, crates.Length);
					try {
						Network.Instantiate(crates[crateNum], spawnPos, crates[crateNum].rigidbody.rotation, 0);
					} catch (UnityException ex)  {
                        Debug.Log(ex.ToString());
					}
					time = 0.0f;
				}
			}
		}
	}
}

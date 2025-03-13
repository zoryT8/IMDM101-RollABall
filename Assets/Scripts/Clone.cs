using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Clone : MonoBehaviour {
    
    private int numPickUps = 10;
    private float yPos = 0.5F;
    private float minXZPos = -9.5F;
    private float maxXZPos = 9.5F;
    GameObject mother; // the GO to clone

    GameObject[] clones; // array of GOs to be cloned

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        mother = GameObject.Find("PickUp");
        clones = new GameObject[numPickUps];

        for (int i = 1; i < clones.Length; i++) {
            // Instantiate (clone) the GO
            clones[i] = GameObject.Instantiate(mother);

            Collider[] hitColliders;

            do { // keep changing position until it does not overlap with obstacle
                clones[i].transform.position = new Vector3(Random.Range(minXZPos, maxXZPos), yPos, Random.Range(minXZPos, maxXZPos));

                hitColliders = Physics.OverlapSphere(clones[i].transform.position, 0.25F); // check for collision with obstacle
            } while (hitColliders.Length > 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableBehavior : MonoBehaviour {

    public int numberOfPieces = 100;
    public float sizeOfPieces = 0.1f;
    public float impulseNeeded = 1.0f;

    private void Start()
    {
        this.GetComponent<rigidBodyTimeTracker>().permanent = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.GetComponent<rigidBodyTimeTracker>().state == timeTracker.State.PLAY && collision.impulse.magnitude >= impulseNeeded)
        {
            breakIntoPieces(numberOfPieces);
        }
    }

    void breakIntoPieces(int numPieces)
    {
        for (int i = 0; i < numPieces; i++)
        {
            generatePiece(sizeOfPieces, this.transform.position, this.transform.localScale.x / 2f, 0f, this.GetComponent<Renderer>().material);
        }
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Collider>().enabled = false;
    }

    void generatePiece(float size, Vector3 pos, float maxDisplacement, float maxIrregularity, Material mat)
    {
        GameObject piece = new GameObject();
        piece.AddComponent<Rigidbody>();
        piece.transform.localScale = new Vector3(size, size, size);
        piece.transform.position = pos + Random.insideUnitSphere * maxDisplacement;

        Mesh mesh = new Mesh();
        Vector3[] vertices = {
            Random.insideUnitSphere * 1f,
            Random.insideUnitSphere * 1f,
            Random.insideUnitSphere * 1f,
            Random.insideUnitSphere * 1f,
            Random.insideUnitSphere * 1f,
            Random.insideUnitSphere * 1f,
            Random.insideUnitSphere * 1f,
            Random.insideUnitSphere * 1f
        };

        int[] triangles = {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        MeshFilter mF = piece.AddComponent<MeshFilter>();
        mF.mesh = mesh;
        piece.AddComponent<MeshRenderer>();

        piece.AddComponent<BoxCollider>();
        piece.GetComponent<Renderer>().material = mat;
        piece.AddComponent<rigidBodyTimeTracker>();
        FindObjectOfType<timeManager>().subscribeToTime(piece.GetComponent<rigidBodyTimeTracker>());
        piece.GetComponent<rigidBodyTimeTracker>().permanent = false;
        piece.GetComponent<rigidBodyTimeTracker>().destroyAtZero = true;
    }
}

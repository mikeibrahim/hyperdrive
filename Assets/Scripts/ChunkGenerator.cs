using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {
	[SerializeField] private Chunk[] chunks = null;
	// [SerializeField] private GameObject volume = null;

    public Chunk GetChunk() { return chunks[Random.Range(0, chunks.Length)]; }
    // public GameObject GetVolume() { return volume; }
}

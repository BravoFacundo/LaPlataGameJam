using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnYValue;
    public float respawnYOffset;
    public float safeGroundHeightDetection;
    [SerializeField] private Vector3 lastSafeGroundPosition;
    private int objLayer;
    private int playerLayer;

    private void Start()
    {
        objLayer = LayerMask.NameToLayer("PuzzleObject");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, safeGroundHeightDetection))
        {
            if (hit.collider.CompareTag("SafeGround")) lastSafeGroundPosition = hit.collider.transform.position;
        }

        if (transform.position.y < respawnYValue) RespawnPlayer();
    }

    private void RespawnPlayer()
    {
        if (lastSafeGroundPosition != Vector3.zero)
        {
            Vector3 respawnPosition = lastSafeGroundPosition + new Vector3(0f, lastSafeGroundPosition.y + respawnYOffset, 0f);
            transform.position = respawnPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Physics.IgnoreLayerCollision(objLayer, playerLayer, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeBall : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    private GameObject playerObj;

    private void Start()
    {
        playerObj = transform.GetChild(0).gameObject;
        ChangeBallObject("Basket");
    }

    public void ChangeBallObject(string name)
    {
        foreach (GameObject prefab in prefabList)
        {
            if (prefab.name[5..] == name)
            {
                GameObject newPrefab = Instantiate(prefab, playerObj.transform.position, playerObj.transform.rotation);
                newPrefab.transform.parent = transform;
                
                TransferComponents(playerObj, newPrefab);

                Destroy(playerObj);
                playerObj = newPrefab;
                break;
            }
        }
    }

    private void TransferComponents(GameObject oldObject, GameObject newObject)
    {
        newObject.transform.localScale = oldObject.transform.localScale;
        newObject.transform.SetPositionAndRotation(oldObject.transform.position, oldObject.transform.rotation);
    }
}

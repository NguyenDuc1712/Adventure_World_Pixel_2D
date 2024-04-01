using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArrow : MonoBehaviour
{
    public Transform spawnerPoint;
    public GameObject arrowPrefab;
    public void SpawnerArrowPoimt()

    {
        GameObject arrow = Instantiate(arrowPrefab, spawnerPoint.position,arrowPrefab.transform.rotation);
        Vector3 originalScale = arrow.transform.localScale;
        arrow.transform.localScale = new Vector3(
            originalScale.x* transform.localScale.x > 0 ?1 : -1,
            originalScale.y,
            originalScale.z
            );
    }
}

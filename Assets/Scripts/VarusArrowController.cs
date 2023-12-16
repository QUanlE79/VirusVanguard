using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarusArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectilePrefab;
    public void Launch()
    {
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarusArrowController : MonoBehaviour
{
    public Transform LaunchPonit;
    // Start is called before the first frame update
    public GameObject projectilePrefab;
    public void Launch()
    {
        GameObject projectie = Instantiate(projectilePrefab, LaunchPonit.position, projectilePrefab.transform.rotation);
        Vector3 orgin = projectie.transform.localScale;
        int direction = transform.localScale.x > 0 ? 1 : -1;
        projectie.transform.localScale = new Vector3(
            orgin.x * direction,
            orgin.y,
            orgin.z);
    }
}

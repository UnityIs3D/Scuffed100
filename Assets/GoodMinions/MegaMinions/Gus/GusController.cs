using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GusController : MonoBehaviour
{
    public Gus gusFlyScript;
    public SpinAttackGus gusSpinScript;
    public GameObject yellowBubble;


    private void Start()
    {
        StartCoroutine(BothScriptsDuration());
    }




    IEnumerator BothScriptsDuration()
    {
        while (true)
        {
            int randomNumber = Random.Range(7, 14);

            gusFlyScript.enabled = true;
            yield return new WaitForSeconds(randomNumber);

            gusFlyScript.enabled = false;
            gusSpinScript.enabled = true;
            yellowBubble.SetActive(true);
            yield return new WaitForSeconds(randomNumber);
            yellowBubble.SetActive(false);
            gusSpinScript.enabled = false;
        }




    }



}

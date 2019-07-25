using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElement : MonoBehaviour
{
    public float dissolveAmount = 0.5f;
    private Material objectMaterial;
    private bool isHighlighting;
    public float dissolveIncrement = -0.01f;

    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
        isHighlighting = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float inc = dissolveIncrement;
        if (isHighlighting && dissolveAmount >= 0.0f)
        {
            dissolveAmount += inc;
            Mathf.Clamp(dissolveAmount, 0.0f, 1.0f);

            objectMaterial.SetFloat("_pbrGraph_Test_dissolveAmount", dissolveAmount);
            
        }
        else if (!isHighlighting && dissolveAmount <= 1.0f)
        {
            inc = -inc;
            dissolveAmount += inc;
            Mathf.Clamp(dissolveAmount, 0.0f, 1.0f);

            objectMaterial.SetFloat("_pbrGraph_Test_dissolveAmount", dissolveAmount);
        }


        /*if (!(dissolveAmount <= 0.2f) && !(dissolveAmount >= 1.0f))
        {
            float inc = dissolveIncrement;
            if (!isHighlighting)
            {
                inc = -inc;
            }

            dissolveAmount += inc;

            objectMaterial.SetFloat("_pbrGraph_Test_dissolveAmount", dissolveAmount);
        }*/
        
    }

    /// <summary>
    /// When the user hovers over this interactive element, let's highlight it for them.
    /// Also, we should set the Character Controller Script's Selected Item
    /// </summary>
    private void OnMouseEnter()
    {
        isHighlighting = true;
        //objectMaterial.SetFloat("_pbrGraph_Test_dissolveAmount", dissolveAmount);
    }

    private void OnMouseExit()
    {
        isHighlighting = false;
    }

    /*IEnumerator Dissolve()
    {
        float dissolveIncrement = 0.1f;
        
        for (float i = dissolveAmount; i <= 0.0f || i >= 1.0f; i += dissolveIncrement)
        {

        }
    }*/
}

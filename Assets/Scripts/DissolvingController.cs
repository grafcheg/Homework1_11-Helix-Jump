using System.Collections;
using UnityEngine;

public class DissolvingController : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public float dissolveRate = 0.02f;
    public float refreshRate = 0.05f;
    public float dieDelay = 0.2f;

    // private Material[] dissolveMaterials;
    private Material DissolveMaterial;

    private void Start()
    {
        Renderer sectorRenderer = GetComponent<Renderer>();
        DissolveMaterial = sectorRenderer.material;
    }

    public void StartDissolve()
    {
        StartCoroutine(Dissolve());
    }
    
    IEnumerator Dissolve()
    {
        yield return new WaitForSeconds(dieDelay);

        float counter = 0;

        if (DissolveMaterial)
        {
            while (DissolveMaterial.GetFloat("DissolveAmount_") < 1)
            {
                counter += dissolveRate;
                
                DissolveMaterial.SetFloat("DissolveAmount_", counter);

                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}

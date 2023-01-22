using System.Collections;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public bool IsGood = true;
    public bool IsBreakable = false;
    public Material GoodMaterial;
    public Material BadMaterial;
    public Material BreakableMaterial;
    public AudioSource AudioSource;
    public GameObject BrokenPlatformEffect;

    private DissolvingController dissolvingController;
    private Collider Collider;

    private void Awake()
    {
        UpdateMaterial();
        dissolvingController = GetComponent<DissolvingController>();
        Collider = GetComponent<Collider>();
    }

    private void UpdateMaterial()
    {
        Renderer sectorRenderer = GetComponent<Renderer>();
        // sectorRenderer.sharedMaterial = IsGood ? GoodMaterial : BadMaterial;
        if (IsGood && IsBreakable)
            // sectorRenderer.sharedMaterial = BreakableMaterial;
            sectorRenderer.material = BreakableMaterial;
        else if (IsGood)
            sectorRenderer.sharedMaterial = GoodMaterial;
        else
            sectorRenderer.sharedMaterial = BadMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.TryGetComponent(out Player player)) return;
        
        Vector3 normal = -collision.contacts[0].normal.normalized;
        float dot = Vector3.Dot(normal, Vector3.up);
        
        if (!(dot >= 0.5)) return;

        if (IsGood && IsBreakable)
        {
            AudioSource.Play();
            GameObject brokenPlatformEffect = Instantiate(BrokenPlatformEffect, transform.position, Quaternion.identity);
            brokenPlatformEffect.GetComponent<ParticleSystem>().Play();
            StartCoroutine(DestroyCoroutine());
        }
        else if (IsGood)
            player.Bounce();
        else
            player.Die();
    }

    private void OnValidate()
    {
        UpdateMaterial();
    }

    IEnumerator DestroyCoroutine()
    {
        Collider.enabled = false;
        dissolvingController.StartDissolve();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

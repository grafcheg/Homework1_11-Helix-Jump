using UnityEngine;

public class FinishEffect : MonoBehaviour
{
    public Player Player;
    public GameObject FinishEffectPS;

    public void PlayEffect()
    {
        GameObject finish = Instantiate(FinishEffectPS, Player.transform.position, Quaternion.identity);
        finish.GetComponent<ParticleSystem>().Play();
    }
}

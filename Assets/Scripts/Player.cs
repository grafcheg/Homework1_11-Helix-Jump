using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BounceSpeed = 1f;
    public Rigidbody Rigidbody;
    public Platform CurrentPlatform;
    public Game Game;
    public AudioSource AudioSource;
    public GameObject SplashEffect;

    public void Bounce()
    {
        Rigidbody.velocity = new Vector3(0, BounceSpeed, 0);
        AudioSource.Play();
        GameObject splash = Instantiate(SplashEffect, transform.position, Quaternion.identity);
        splash.GetComponent<ParticleSystem>().Play();
    }

    public void Die()
    {
        Game.OnPlayerDied();
        Rigidbody.velocity = Vector3.zero;
    }

    public void ReachFinish()
    {
        Game.OnPlayerReachedFinish();
        Rigidbody.velocity = Vector3.zero;
    }
}

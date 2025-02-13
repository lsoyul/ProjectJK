﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectile : MonoBehaviour
{
    public GameObject spriteImage;
    private float speed = 0f;
    private GameStatics.PROJECTILE_TYPE projectileType;

    private bool isCollide = false;

    void Start()
    {
        if (this.gameObject.activeInHierarchy == false) this.gameObject.SetActive(true);       
    }

    public void Fire(float speed, GameStatics.PROJECTILE_TYPE projectileType)
    {
        this.speed = speed;
        this.projectileType = projectileType;
        float fireAngle = this.transform.localRotation.z + 45;
        spriteImage.transform.localRotation = Quaternion.Euler(0, 0, fireAngle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCollide == false)
        {
            this.transform.Translate(this.transform.up * speed * Time.fixedDeltaTime, Space.World);
        }
    }

    public void HitSomething()
    {
        // Play Hit Effect
        // And Destroy this gameobject
        EffectManager.GetInstance().playEffect(this.transform.position, GameStatics.EFFECT.Explosion1, Vector2.zero);

        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (isCollide == false)
            {
                switch (collision.tag)
                {
                    case "Platform":
                        {
                            isCollide = true;
                            HitSomething();
                        }
                        break;


                    default:
                        break;
                }
            }
        }
    }
}

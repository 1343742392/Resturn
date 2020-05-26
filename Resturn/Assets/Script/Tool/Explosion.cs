using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Explosion : MonoBehaviour 
{
    public float Radius;// explosion radius
    public float Force;// explosion forse
    public List<String> IgnoreName = new List<string>();
    private void Start()
    {
        IgnoreName.Add("Terrain");


    }
    bool once = false;
    void Update () 
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);// create explosion
        for(int i=0; i<hitColliders.Length; i++)
        {              
            if(!IgnoreName.Contains(hitColliders[i].name))// if tag CanBeRigidbody
            {
                var gameobj = hitColliders[i].gameObject;
                var et = gameobj.GetComponent<ExplosionTarget>();
                if(et!=null)
                {
                    et.Blast(this);
                }
                
                if (!hitColliders[i].GetComponent<Rigidbody>())
                {
                    gameobj.AddComponent<Rigidbody>();
                }
                hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, Radius, 0.0F); // push game object
            }
			
        }
        Destroy(gameObject);// destroy explosion
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,Radius);
    }
}
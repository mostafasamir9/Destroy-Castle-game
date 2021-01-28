using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    static public Projectile S0;
   
    static public Rigidbody Pro;
    static public float M;

    // Start is called before the first frame update

     void Awake()
    {
        Pro = this.GetComponent<Rigidbody>();
        M = 10;
    }

    public void Mass10()
    {
        M = 10;
        Pro = this.GetComponent<Rigidbody>();
        Vector3 a = new Vector3(3, 3, 3);
        this.transform.localScale = a;
        Pro.mass = 10;
    }
    public void Mass5()
    {
        M = 5;
        Pro = this.GetComponent<Rigidbody>();
        Vector3 a = new Vector3(2, 2, 2);
        this.transform.localScale = a;
        Pro.mass = 5;
    }
    public void Mass11()
    {
        M = 1;
        Pro = this.GetComponent<Rigidbody>();
        Vector3 a = new Vector3 (1, 1, 1);
        this.transform.localScale = a;
        Pro.mass = 1;
    }
}

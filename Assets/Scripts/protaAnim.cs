using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protaAnim : MonoBehaviour
{

    public bool comeco = false;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void comecar()
    {
        comeco = true;
        anim.SetBool("comecar", true);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialModeObj : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MakeSpecialModeHappen()
    {
        anim.SetBool("SpecialMode", true);

    }
    
    public void MakeSpecialModeFalse()
    {
        anim.SetBool("SpecialMode", false);
    }

}

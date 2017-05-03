using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachToBone : MonoBehaviour {

    public HumanBodyBones selectedBone = HumanBodyBones.Spine;
	void Start () {
        this.transform.parent = this.GetComponentInParent<Animator>().GetBoneTransform(selectedBone);
    }
}

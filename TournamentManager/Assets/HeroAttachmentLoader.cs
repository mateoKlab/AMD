using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;
public class HeroAttachmentLoader : MonoBehaviour {

	public enum SetType {
		White, Red, Green, Blue
	}
	public static HeroAttachmentLoader instance;
	public List<string> redSet = new List<string>();
	public List<string> blueSet = new List<string>();
	public List<string> greenSet = new List<string>();
	public List<string> whiteSet = new List<string>();

	public SetType setType;
	private SkeletonRenderer _skeletonRenderer;

	void Start() {
		instance = this;
		_skeletonRenderer = GetComponent<SkeletonRenderer>();
//		GetComponent<SkeletonRenderer>().skeleton.FindSlot("hand1_red").Attachment = null;
//		GetComponent<SkeletonRenderer>().skeleton.FindSlot("hand1_red").SetToSetupPose();

		for(int i = 0; i < _skeletonRenderer.skeleton.slots.Count; i++) {
			if(_skeletonRenderer.skeleton.Slots.Items[i].ToString().Contains("red")) {
				redSet.Add(_skeletonRenderer.skeleton.Slots.Items[i].ToString());
			} else 	if(_skeletonRenderer.skeleton.Slots.Items[i].ToString().Contains("green")) {
				greenSet.Add(_skeletonRenderer.skeleton.Slots.Items[i].ToString());
			} else 	if(_skeletonRenderer.skeleton.Slots.Items[i].ToString().Contains("blue")) {
				blueSet.Add(_skeletonRenderer.skeleton.Slots.Items[i].ToString());
			} else if(_skeletonRenderer.skeleton.Slots.Items[i].ToString().Contains("white")) {
				whiteSet.Add(_skeletonRenderer.skeleton.Slots.Items[i].ToString());
			}

		}

		ChangeSet();

	
	}

	public void ChangeSet() {

		for(int i = 0; i < greenSet.Count; i++) {
			_skeletonRenderer.skeleton.FindSlot(greenSet[i]).Attachment = null;
		}
		for(int i = 0; i < blueSet.Count; i++) {
			_skeletonRenderer.skeleton.FindSlot(blueSet[i]).Attachment = null;
		}
		for(int i = 0; i < redSet.Count; i++) {
			_skeletonRenderer.skeleton.FindSlot(redSet[i]).Attachment = null;
		}
		for(int i = 0; i < whiteSet.Count; i++) {
			_skeletonRenderer.skeleton.FindSlot(whiteSet[i]).Attachment = null;
		}

		if(setType == SetType.Blue) {
			for(int i = 0; i < blueSet.Count; i++) {
				_skeletonRenderer.skeleton.FindSlot(blueSet[i]).SetToSetupPose();
			}
		} else if(setType == SetType.Green) {
			for(int i = 0; i < greenSet.Count; i++) {
				_skeletonRenderer.skeleton.FindSlot(greenSet[i]).SetToSetupPose();
			}
		} else if(setType == SetType.Red) {
			for(int i = 0; i < redSet.Count; i++) {
				_skeletonRenderer.skeleton.FindSlot(redSet[i]).SetToSetupPose();
			}
		} else if(setType == SetType.White) {
			for(int i = 0; i < whiteSet.Count; i++) {
				_skeletonRenderer.skeleton.FindSlot(whiteSet[i]).SetToSetupPose();
			}
		}
	}
	

}

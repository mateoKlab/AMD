using UnityEngine;
using System.Collections;

public class DrawGizmo : MonoBehaviour {

	public Color gizmoColor;
	public float x;
	public float y;

	public float xPos;
	public float yPos;

	void OnDrawGizmos()
	{
		Gizmos.color = new Color (gizmoColor.r, gizmoColor.g, gizmoColor.b);

		Vector3 gizmoCenter = new Vector3 (transform.localPosition.x + xPos, transform.localPosition.y + yPos, transform.localPosition.z);
		Gizmos.DrawWireCube(gizmoCenter, new Vector3(x, y, 1f));
	}
}

#pragma strict
var newPos : Vector3;
var posX : float;
var rotX : float;

function Update () {
	if(Input.GetMouseButtonDown(0)) {
		newPos = Input.mousePosition;
	}
	if(Input.GetMouseButton(0)) {
		if(newPos != Input.mousePosition) {
			posX = newPos.x - Input.mousePosition.x / Screen.width;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotX, transform.rotation.y + posX * -360, transform.rotation.z), Time.deltaTime * 1000);
			newPos = Input.mousePosition;
		}
	}
}
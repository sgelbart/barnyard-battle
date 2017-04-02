#pragma strict
var focusPoint : Transform;
var camSpot : Transform; //camera's destination (used for zooming camera in and out)
var zoomLimit : int = 0; //used to limit distance you can zoom in and out
var canScroll = true;

function Update () {
	//If player mouse-scrolls foward
	if(Input.GetAxis("Mouse ScrollWheel") > 0) {
		if(canScroll == true) {
			//can only zoom in four intervals from camSpot's starting pos
			if(zoomLimit < 1) {
				//zoom camSpot in
				camSpot.transform.position = camSpot.transform.position + 0.25 * camSpot.transform.forward;
				zoomLimit += 1;
			}
		}
	}
	//If player mouse-scrolls backward
	if(Input.GetAxis("Mouse ScrollWheel") < 0) {
		if(canScroll == true) {
			//can only zoom out four intervals from camSpot's starting pos
			if(zoomLimit > -2) {
				//zoom camSpot out
				camSpot.transform.position = camSpot.transform.position - 0.25 * camSpot.transform.forward;
				zoomLimit -= 1;
			}
		}
	}
	//Lerp camera to camSpot
	transform.position = Vector3.MoveTowards(transform.position, camSpot.transform.position, 5 * Time.deltaTime);
}
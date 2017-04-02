#pragma strict
var animal : GameObject;
var skin : GameObject;
var mat1 : Material;
var mat2 : Material;
var changedTexture = false;

function LoadNewScene(scene : String) {
	Application.LoadLevel(scene);
}
function PlayAnimation(anim : String) {
	animal.GetComponent.<Animation>().CrossFade(anim);
	animal.GetComponent.<Animation>()[anim].speed = 1;
	if(anim == "Walk" || anim == "Idle" || anim == "Idle_Sit" || anim == "Swim" || anim == "Idle_Swim") {
		animal.GetComponent.<Animation>()[anim].wrapMode = WrapMode.Loop;
	}
	else {
		animal.GetComponent.<Animation>()[anim].wrapMode = WrapMode.Once;
	}
}
function SwapMaterials() {
	if(changedTexture == false) {
		skin.GetComponent.<Renderer>().material = mat2;
		changedTexture = true;
	}
	else {
		skin.GetComponent.<Renderer>().material = mat1;
		changedTexture = false;
	}
}

#pragma strict
//WANDER SCRIPT - ANIMALS WILL WALK AROUND RANDOMLY IN A GENERAL LOCATION
var wander = true; //wheather or not you want the animal to wander around, or stand/sit in one place | The Animation on the Animation Component will be played, and looped continuously
var gravity : float = 9.8f; //gravity, force directed downward, on the object
private var controller : CharacterController; //character conroller component attached to the animal
private var moveDirection : Vector3 = Vector3.zero;
private var forward : Vector3; //forward direction / direction animal is facing
var speed : float = 0.5; //movement Speed;
private var hit : RaycastHit;
private var raycastPoint : Vector3; //point of reference, which we will use to detect obstacles in our path | auto-calculated based on Character Controller size and animal position
private var rayDist : float; //how far ahead of the animal we will check for collisions

var animal : Transform; //the object with the animations or 'Animation' Component (in this case, the child gameObject)
private var startPos : Vector3; //the starting position | This is determined on Start()
var maxDistance : float = 3; //how far away the animal can walk from its starting position | True maxRange is maxDistance + 1 (A cousion is added)

var minIdle : float = 1; //the minimum time length the animal will stand idle before walking again | must be 0 or greater
var maxIdle : float = 10; //the maximum time length the animal will stand idle before walking again | must be greater than the min

var minWalk : float = 2; //the minimum time length the animal will walk before standing still again | must be 0 or greater
var maxWalk : float = 5; //the maximum time length the animal will walk before standing still again | must be greater than the min

private var idle = false; //if the animal is walking or idle | used to determine animations and movement, you do not need to check or uncheck this box
private var waitTime : float = 0; //random time calculated using min and max idle or walk ^ | this makes movements more dynamic, and greatly reduces the chance of sycronized movements among multiple of the same animal
private var timer : float = 0; //timer | used to count time spent idle and time spent moving

private var newDirection : float; //new random direction for animal to walk | as a float because we only need the y axis
private var returningHome = false; //used to tell wheather or not the animal has wandered too far from maxDistance and is now returning to startPos
private var distanceBetween : float; //distance animal is from startPos
var moveAnimation : String[]; //name of walk (or movement) animation | Most animals movement animation's are named "Walk", but for instance, if the duck was in water, you can change this name to "Swim", instead.
var idleAnimation : String[]; //assortment of 'idle' animation names to be played at random | This can be, standing, eating, looking around, etc. Provide the name of the animation, as a string(text), not the animationClip.

function Awake() {
	//if animal is not assigned, assume it is the first child of this gameObject
	if(animal == null) {
		animal = transform.GetChild(0).transform;
	}
	
	//if wander is false
	if(wander == false) {
		//Play Animation
		animal.GetComponent.<Animation>().Play();
		//get the name of the animation clip attached to the Animation Component
		var animName : String = animal.GetComponent.<Animation>().clip.name;
		animal.GetComponent.<Animation>()[animName].speed = 1;
		animal.GetComponent.<Animation>()[animName].wrapMode = WrapMode.Loop;
		
		//disable script
		enabled = false;
	}
}
function Start () {
	rayDist = GetComponent(CharacterController).radius * 2; //set the rayDist for this animal | the rayDist is equal to twice the radius of the animal's CharacterController
	waitTime = 0; //make sure waitTime starts at 0
	timer = 0; //make sure timer starts at 0
	
	//get characterController
	controller = transform.GetComponent(CharacterController);
	
	//get animal's starting position
	startPos = transform.position;
	
	//face a random direction at start
	var randomRot = Random.Range(0, 360); //get a random rotation
	transform.rotation = Quaternion.Euler(0, randomRot, 0); //face direction
	
	//randomly decide wheather to stand idle, or to walk, at start
	var randomIdle = Random.Range(1, 3);
	if(randomIdle == 1) {
		idle = false;
	}
	else {
		idle = true;
	}
	
	//Walk or stand Idle
	NewDecision();
}

function FixedUpdate() {
	//**INCREASE TIMER**
	timer += 1 * Time.deltaTime; //timer increases by 1 every second
	
	//**APPLY GRAVITY**
	if(moveDirection.y > gravity * -1) {
		moveDirection.y -= gravity * Time.deltaTime;
	}
	controller.Move(moveDirection * Time.deltaTime);
}
function Update() {
	//**FORWARD DIRECTION**
	//get the direction the animal is facing
	forward = transform.TransformDirection(Vector3.forward);
	
	//**DISTANCE BETWEEN**
	//calculate distanceBetween animal's current position, and its starting position
	distanceBetween = Vector3.Distance(transform.position, startPos); //can put this line in FixedUpdate() instead, but it won't be as accurrate in keeping the animal within maxDistance bounds
	
	//**IDLE**
	if(idle == true) {
		//while timer is less than idle time
		if(timer < waitTime) {
			//if idle animation is over, play another one
			if(!animal.GetComponent.<Animation>().isPlaying) {
				//Call RandomIdle() function, to detertime next idle animation
				RandomIdle();
			}
		}
		else {
			//when idle time is up
			//if idle animation is over, play walk
			if(!animal.GetComponent.<Animation>().isPlaying) {
				NewDecision();
			}
		}
	}
	//**WALK**
	//while timer is less than walking time, keep walking
	else {
		if(timer < waitTime) {
			//Face new direction, over time
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, newDirection, 0), Time.deltaTime * 25);
			
			//make sure animal does not walk too far out of maxDistance
			if(distanceBetween < maxDistance || returningHome == true) { //if within maxDistance, or returingHome
				//get our raycast point
				raycastPoint = Vector3(transform.position.x, GetComponent(CharacterController).height / 2, transform.position.z);
				//check if an obstacle is in our way
				if(Physics.Raycast(raycastPoint, transform.forward, hit, rayDist)) {
					//something is in our way, let's stand idle, and move a new direction next time
					timer = waitTime;
				}
				else {
					controller.SimpleMove(transform.forward * speed); //move forward, times speed
				}
			}
			else {
				//if animal exceeds maxDistance, walking time is ended. Animal will stand idle for a while, then head towards startPos when it begins walking again
				timer = waitTime;
			}
		}
		else {
			//when walking time is up, play idle
			NewDecision();
		}
	}

}
function NewDecision() {
	//reset timer to 0
	timer = 0;
	//reset returningHome
	returningHome = false;
	
	//if currently walking, stand idle
	if(idle == false) {
		idle = true; //animal is now idle
		waitTime = Random.Range(minIdle, maxIdle); //get new waitTime
		RandomIdle(); //play randomIdle
	}
	//else if idle, start walking
	else {
		//face a random direction
		//make sure the animal is within maxDistance from startPos
		if(distanceBetween < maxDistance) {
			var randomRot = Random.Range(0, 360); //get a random rotation
			newDirection = randomRot;
			
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, newDirection, 0), 1);
		}
		//else, face startPos, and walk back
		else {
			//face startPos
			var neededRotation = Quaternion.LookRotation(startPos - transform.position);
			var slerpRotation = Quaternion.Slerp(transform.rotation, neededRotation, 1);
			newDirection = slerpRotation.eulerAngles.y;
			
			//returningHome is true
			returningHome = true;
		}
		//Play walk animation
		var randomMove = Random.Range(0, moveAnimation.Length);
		animal.GetComponent.<Animation>().CrossFade(moveAnimation[randomMove]); //play walk animation
		animal.GetComponent.<Animation>()[moveAnimation[randomMove]].speed = 1; //set walk animation speed to 1
		animal.GetComponent.<Animation>()[moveAnimation[randomMove]].wrapMode = WrapMode.Loop; //loop the walk animation
		
		waitTime = Random.Range(minWalk, maxWalk); //get new waitTime
		idle = false; //animal is now walking
	}
}
function RandomIdle() {
	//**RANDOM IDLE ANIMATION**
	//make sure there are idle animations to pick from
	if(idleAnimation.length > 0) {
		//Get random idle animation
		var randomIdle = Random.Range(0, idleAnimation.length); //choose a random idle animation
		
		//Play idle animation
		animal.GetComponent.<Animation>().CrossFade(idleAnimation[randomIdle]); //play Idle animation
		animal.GetComponent.<Animation>()[idleAnimation[randomIdle]].time = 0;
		animal.GetComponent.<Animation>()[idleAnimation[randomIdle]].speed = 1; //set Idle animation speed to 1
		animal.GetComponent.<Animation>()[idleAnimation[randomIdle]].wrapMode = WrapMode.Once; //play the Idle animation once
	}
	else {
		//if no idle animation(s) have been provided, animal will walk continuously
		NewDecision();
	}
}







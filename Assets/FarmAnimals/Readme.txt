README
/**
Farm Animals
Lylek Games

Thank you for purchasing this asset!

To place an animal into your scene, simply drag and drop the animal you want from the Prefabs folder. Each
animal prefab is ready to go when placed in the scene. The WanderScript, attached to each prefab,
is documented for your preview and/or editability.

The WanderScript is made to have your animals wander in a general location. You can edit properties of the script, 
specific to each animal, in the Editor. 

The MaxDistance variable will limit the distance the animal can wander away from its starting position. You can also
set the minimum and maximum walking and idle times, per animal. This will limit how long the animal will walk until
resting idle, and how long the animal will stand idle, until walking again.

Weather the animal stands idle, or begins walking on start is chosen randomly in the script, on lines 64-70.

If you do not wish for the animal to wander at all, simply uncheck the Wander variable, in the Editor.
The animal will instead loop the animation currently in the clip of the Animation Component. For each animal prefab,
the Idle_Sit animation is already loaded into this clip.

If, instead, you do not want the animal to ever stop walking, set the IdleAnimations size to 0, in the Editor. The
IdleAnimations variable is an array of idle animations to be played at random, when the animal is at rest.

If you need any help, please contact:
support@lylekgames.com

I will be glad to assist you! =)

Feel free to leave a rating and review!
Thank you.

*******************************************************************************************

Website
http://www.lylekgames.com/

Email
support@lylekgames.com

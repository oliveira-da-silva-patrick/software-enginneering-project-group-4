# Creator's  Guide

Throughout the development of this project each member will be *developing* and *implementing new features*. This document was created with the intent of helping
the team keep track of all the features and all it's implementations.

Although a large part of the project will be developing scripts, there will still be much work done directly on _Unity_, therefore it will be useful to document
how everything works in case any other member wants to replicate any feature. _(Or just in case the original creator forgets what they did)_

This document will be divided into **sections** and will be continuously updated for the duration of the project.

### Indice:

* [Scripts](#Scripts)
* [Player](#Player)
* [Camera](#Camera)
* [Enemies](#Enemies)
* [Boundaries](#Boundaries)
* [Tags](#Tags)
* [Projectiles](#Projectiles)
* [Joystick](#Joystick)
* [Animations](#Animations)

## Scripts
  
  Each script contains a description of it's own purpose and instructions on how/where it should be used.
  
  Additionally scripts will contain explanations on eventual problems the original creator ran into. They can sometimes have the following format:
  
    Q: Are you having trouble using this script?
    A: Just read the instruction manual!
  
## Player

  The main character is controlled by the [joystick](#Joystick). When creating a main player do not forget to give it a **_Player_** [tag](#Tags).

  Used scripts:
  
  * PlayerMovement.cs
  
## Camera
  
  The camera is set to follow the main character around the map by updating its position to be equal to the player's.
  
  Used scripts:
  
  * CameraMovement.cs
  
## Enemies

  There are different types of enemies:
  
  - ***Standart***: These are the most common and weaker kind of enemy that usually appears in groups. They can be found in most rooms and sometimes on the main hall.
  - ***Mini-Bosses***: These are stronger, more challenging and give greater reward once beaten. They usually appear alone _(Unless you are really unlucky...)_.
 
 Both of these above will be implemented using ***Circlecast***, which means that these will only follow the player if he's within their _field of view_.
  - ***Bosses***: This is the final and hardest challenge the player will face. This enemy will be implemented using ***Pathfinding***, which means that it will be
 able to detect the player indenpendently of it's position in the room. _(You can **run** but you can't **hide**...)_ 
 
 ### CircleCast
 
 Circlecast is a detecting mechanism for entities. It consists of a circle area around the entity that detects when other entities are in range. We use a 360 degree
 angle so that the enemy can detect the player in every direction.
 Furthermore this mechanism also has the ability to detect obstacles, which consequentialy prevents the detection of entities if they are behind said obstacles.
 
 Our `FieldOfView.cs` script contains a variable `CanAtack` that may be checked by other scripts in the same game object. This way we can also add an attack type
 script to the enemy that is only activated once the `CanAtack` is activated.
 
   #### Attack types
   
   * Shooting
 

  Used scripts:
  
  * Enemy.cs
  
## Boundaries

  A boundary defines the limit of the map and is most commonly a wall but it can be used for another object that has the same properties. All boundaries should have
  the ***Boundary*** tag. As explained in the [tags](#Tags) section, it is necessary to identify 
  certain Game Objects. For example, [projectiles](#Projectiles) self-destruct when they come in contact with boundaries, however this only happens if the boundary
  is tagged, otherwise the projectile just phases through the boundary.
  
  Furthermore, boundaries should be given a ***Kinematic*** body type. This allows interaction with Game Objects, such as collision detection but makes it unnaffected
  by forces, such as gravity.
  
## Tags

  Tags have an important role in the game behind the scenes. They allow to identify and determine interactions between _Game Objects_.
  
  Tags used:
  
  * ***Player*** - Given to the main player
  * ***Boundary*** - Given to any Game Object that is a map limit or has the same properties.
  
## Projectiles

  Projectiles are custom game objects thrown by the enemy to damage the player. There can (and will) exist different types of projectiles, the following steps 
  provide guidance on how to create a **custom game object**:
  
  - Create or drag into the scene a game object. (You can use the default objects on Unity, _Ex: Circle, Square, etc._ or your own objects)
  - Modify the properties of the objects such as color, size, layer, etc.
  - Add the desired components to the object. In the case of a projectile we want to add the following:
      * _Sprite Renderer_ (present by default)
      * _Collider 2D_
      * _Projectile.cs_
  - Now drag this game object into one of your folders and you can delete it from the scene.
  - That's it! Your Custom Game Object is created. In the case of the projectile object you can now drag it into the enemy's projectile slot.
  
  Note: Keep in mind that when you attach a game object to a slot in a script, you are ***not*** creating a _copy_. Think of it as a link or pointer, 
  so any changes that you make to the game object in your folder will be applied to the the projectile thrown by the enemy. _(Which is handy otherwise you 
  would have to reattach the game object every time you modified it)_

  Used scripts:
  
   * Projectile.cs
  
## JoyStick

  For the joystick, we downloaded [this](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631) free pack
  from the [Unity asset store](https://assetstore.unity.com/).
  
  To place the joystick on the scene you first need to create a canvas. You can do so by `Right clicking on Hierarchy` --> `UI` --> `Canvas` and finally drag the 
  joystick inside the canvas.
  
  We chose to use the ***Floating*** joystick, which allows the player to touch anywhere on the screen to move the main character.
  
## Animations

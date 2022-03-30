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
 
 Both of these above will be implemented using ***Raycast***, which means that these will only follow the player if he's within their _field of view_.
  - ***Bosses***: This is the final and hardest challenge the player will face. This enemy will be implemented using ***Pathfinding***, which means that it will be
 able to detect the player indenpendently of it's position in the room.

  Used scripts:
  
  * Enemy.cs
  
## Boundaries

  A boundary defines the limit of the map. All boundaries should have the ***Boundary*** tag. As explained in the [tags](#Tags) section, it is necessary to identify 
  certain Game Objects. For example, [projectiles](#Projectiles) self-destruct when they come in contact with boundaries, however this only happens if the boundary
  is tagged, otherwise the projectile just phases through the boundary.
  
## Tags

  Tags have an important role in the game behind the scenes. They allow to identify and determine interactions between _Game Objects_.
  
## Projectiles

  Used scripts:
  
   * Projectile.cs
  
## JoyStick
## Animations

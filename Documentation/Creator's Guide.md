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
* [AI-Enemies](#AI-Enemies)
* [Boundaries](#Boundaries)
* [Tags](#Tags)
* [Projectiles](#Projectiles)
* [Collectibles](#Collectibles)
* [Joystick](#Joystick)
* [Animations](#Animations)
* [UI-Design](#UI-Design)

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
   * Direct hitters


  Used scripts:
  
  * FieldOfView.cs
  * Shooting.cs

## AI-Enemies

  Due to their complexity AI enemies have their own dedicated section. We used a package from [this](https://arongranberg.com/astar/) website to implement these
  enemies.
  
  We used ***Pathfinding*** to control the entities' movement. This mechanism enables enemies to reach the player wherever he is within a defined area, all while 
  avoiding obstacles. Contrary to normal enemies, these AI enemies can go _around_ obstacles. To make them more worthy of their title,
  they are also smart enough to know _when_ to shoot, meaning, they only do so when their target is in sight, which is achieved using ***CircleCast***.
  
  The scripts `AI_FieldOFView` and `AI_Shooting` are quite similar to the `FieldOfView` and `Shooting` respectively, with only a few modifications to make the 
  behaviours descrived above work correctly.
  
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

## Collectibles

  A collectible is any item the player can pick up from the ground.
  
  - Coin - These can be found on the floor or inside _Chests_.
  - Diamond - These can be found on the floor or inside _Chests_.
  
  The collectibles above are affected by the player's magnet. Within a certain range, they are  attracted to the player. This range can be adjusted by simply changing
  the '_Circle Collider 2D_' radius or by creating a var in the script that does the same.
  If you want to add a new object that should also be attracted to the player then you'll have to expand the `Magnet.cs` script.
  
  ### Chests
  
  Chests are an interactable object that can be found around the map. It contains a defined amount of coins and/ or diamonds that can be adjusted in Unity. Once 
  the player clicks on a chest, it will open (change sprites) and spawn items. These items will come out in an explosion effect. To adjust the explosion effect
  you'll have to change the velocity value of the instantiated object and its linear drag in the corresponding '_RigidBody 2D_' component.
  
## JoyStick

  For the joystick, we downloaded [this](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631) free pack
  from the [Unity asset store](https://assetstore.unity.com/).
  
  To place the joystick on the scene you first need to create a canvas. You can do so by `Right clicking on Hierarchy` --> `UI` --> `Canvas` and finally drag the 
  joystick inside the canvas.
  
  We chose to use the ***Floating*** joystick, which allows the player to touch anywhere on the screen to move the main character.

## Sound

  In the scene we have an '_AudioManager_' which as name suggests manages the different sounds of multiple objects. This Audio Manager contains multiple 
  '_Audio Sources_', each one with their own '_Audio Clip_'.

## Animations

  _Insert explanation on how to create animations._

## UI-Design

  How to create a menu with buttons?
  
  First of all, we need a new scene. To this scene, you can add a background if you want. I simply created a 2D square and gave it a color.
  A main menu needs the title and some buttons to interact with. 
  Unity has already predefined objects for each. For the title, you can use the 'Text' object or even better the 'Text - TextMeshPro' object. If the second
  is not to be found, you may need to download the TextMeshPro plugin from the asset store. It was already pre-installed on my machine.
  Now, for the buttons it is best to use the 'Button' object. Here again the TextMeshPro alternative is better. TextMeshPro allows more freedom on how the text is
  edited. You may notice that not only a button has been created, but also a parent canvas and a text child. To edit the content of the button, modify the values 
  on the inspector when the button or its text object is selected. It is also better to group the buttons in the canvas under one same empty element.
  Finally, you should have a nice menu with a title and one or some buttons. Here's how your hierarchy could be looking like:
    
    -MainMenu
      -EventSystem
      -Canvas
        -Main Camera
        -Title (Text - TMP)
        -Background (Square)
        -Buttons (Empty element)
          -Button1 (Button - TMP)
            -Text (Text - TMP)
          -Button2 (Button - TMP)
            -Text (Text - TMP)
  
  How to give actions to the buttons?
  
  For the buttons to have an on click event, we need to write some code. Let's create a new script called 'MenuButtons'.
  First of all, we need to use the 'UnityEngine.SceneManagement' library. This can be implemented with this line: 'using UnityEngine.SceneManagement;'.
  In this script, we can delete the predefined methods and we will create a method for each button available.
  I created a play button, so i want to have a method which will move the fame from the menu scene to the in-game scene. So, I create a method called 'Play'.
  In this method, I add one simple line: 'SceneManager.LoadScene("x");'.
  This line will load the scene x and quit the current scene (here the main menu scene).
  
  x can be either the name given to the scene or the index given by the build to the scene. This index number can be found under 'File -> Build Settings'.
  
  Remarks:
  
  It may be that now when you start your game, the scene doesn't load or the text does not appear. 
  First, you need to add the scene to the build. Go to 'File -> Build Settings' and drag and drop your scene into that window. 
  Second, go to the inspector of the canvas which contains the text to be displayed and change its render mode to 'World Space'.
  This should fix those issues.
  
  
  

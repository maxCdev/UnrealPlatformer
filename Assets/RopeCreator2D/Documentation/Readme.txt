Rope Creator 2D


What it does:



You can create 2D ropes between two points. (rope: a chain of circle colliders and Unity Physics2D joints)
Fast and easy creation in editor using a custom editor window.


How to install/setup:



Copy/import the "RopeCreator2D" folder to your project's "Assets" folder.

That's it! But if you want to do it manually:

Place the "Rope2DEditorWindow" script in the "Editor" folder in you project's "Assets" folder.
Place the all the Rope2D prefabs in the "Resources/Prefabs/" folder in you project's "Assets" folder.
Place the all the Materials for Line Renderer rope in the "Resources/Materials/" folder in you project's "Assets" folder.


You can use custom folders for prefabs and materials, change the "Rope2DEditorWindow" script. (you can easly do it by comment/uncomment two rows, line 279 & 487) 

If you make your own prefabs for the ropes:

- Add a CircleCollider2D component with the correct size compare to the sprite.
- Add a child object with a Sprite Renderer (rope prefab graphics), that way it can be rotated independently of the parent



How to use it:



Open the custom editor window for the Rope Creator 2D!
Editor - Tabs - Window - "Rope Creator 2D"

First you need two Transforms (gameobjects) in the scene for the rope's first and last point. (you can use the "Rope2D-PointA-Gizmo/Rope2D-PointB-Gizmo" from the "RopeCreator2D/Resource/Prefabs/" folder, but you can use any gameobject, because you only need the Transform component.)
Drag them to the "Rope Creator 2D" window's "Point A" and "Point B" labels.

Drag a Rope2D-RopeBit prefab form the "Resources/Prefabs/" folder to the "Rope Creator 2D" window's "Rope Prefab" label, and set its scale.

Set the fixings, mass, layer(optional) and gravity scale and the custom endings (optional) for the rope. (custom ending: it adds a prefab with a sprite for the rope ending)

If you are ready, click on the "Generate!" button!


Important!

If you can't drag anything into the "Point A"/"Point B" labels, press the clear button!

Limitation:

When you create the ropes, do not use to many segments for the ropes because the Physics2D joints can't hold the segments together. (recommened: max 20 segments)



Creating Line Renderer rope:

You don't need prefabs, just a material for the Line Renderer.

Drag a material to "Rope Creator 2D" window's "Line Material" label from the "Resources/Materials/" folder.
Set the segments number and the width and the other parameters.

If you are ready, click on the "Generate!" button!



Included:



-Main scripts: "Rope2DEditorWindow" and "Rope2DLineRendSetPoints"
-Prefabs for ropes
-Test scene
-Support scripts
-Documentation



Support scripts:



"Rope2DMouseCutter" script - if you attach it to the ropeBit prefabs, you can cut the rope with the mouse. (click / click & hold the mouse button on the rope)

"Rope2DTear" script - if you attach it to the created rope's folder, the rope will be able to tear

"Rope2DMouseDrag" script - if you attach it to a ropeBit prefabs and you can drag them using the mouse


Notes:

The "Rope Creator 2D" supports the Edit - Undo function.
Custom error/warnings messages will help you determine the problems.

If you liked the product, please rate it!
If you have questions: brundle28@gmail.com


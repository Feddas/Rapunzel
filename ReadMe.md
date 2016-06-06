# requirements to build

Include the 2D standard assets: "Assets" menu => "Import Package..." => "2D"


### How Inventory Works
Each item has its own inventory, Inventory.cs
Each item in the inventory has a number.
positive numbers mean collided gets that item.
negative numbers mean collided requires that item.

### How sounds work
Each item has it's own AudioSounds list, PlaysAudio.cs
Which audio plays depends on.
- self inventory
- collided with inventory
NOT A GOOD IDEA: Variable "isOneShot" destroys this object after audio plays

IDEA: InventoryEvent (replaces PlaysAudio)
One UnityEvent occurs depending on the inventory of two collided items.
Event is the one which has the most matching inventory items.
InventoryEvent Needs to run before inventory script so unmodified values are used.
List of Event types:
- play audio
- destroy gameobject
- LoadScene
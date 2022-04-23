# CippSharpCoreContainers
Containers are intended to be serializable classes that holds a reference to 'data' / 'something'. 
As abstract concept.

### Purpose:
That data or something can be another class / struct that is not a container. As this repository contains scripts.
Maybe it's not the best example but it gives an idea: _it contains the containers ( lol :D )_
(see also the 'samples' inside of this repository for more details)

### Contents:
This repository comes with different approaches to containers. From Serializable classes to ScriptableObjects (Assets).
From basic containers to serialized dictionaries examples or big arrays of data readable in inspector.

### How to Install:
- Option 1 (readonly) now it supports Unity Package Manager so you can download by copy/paste the git url in 'Package Manager Window + Install From Git'.
  As said this is a readonly solution so you cannot access all files this way.
- Option 2 (classic) download this repository as .zip; Extract the files; Drag 'n' drop the extracted folder in your unity project (where you prefer).
- Option 3 (alternative) add this as submodule / separate repo in your project by copy/paste the git url
  

### Code:
* The process is really similar on how to create your custom unity events. Inherit from wanted Container class
and ensure to make it serializable.
* From code you can simply access your data with methods, property and with delegates. 
_your stored data are at your commands, sir!_
* Alternatively if you need containers only to draw and populate an inspector, 
you can simply cache the data in your class during initialization at runtime
* Array / List Drawers will also support ReorderableAttribute from [Here](https://github.com/Cippman/Unity-Reorderable-List.git)
This will give you a mimic of the UnityEditor.ReorderableList applied through an attribute directly to your arrays


### History: 
* For basics containers I confess I started to create a workflow / workaround to find a solution on:
    * how can I draw multiple property drawers of the same serialized property?
    * how if I apply the [ReadOnlyAttribute](https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html) to an array / list property the size is still editable?
    * without overthink on how to adjust this from 'Editor' and Drawers side (that sometimes it may be really tricky)
     I found out that 'Containers' worked really well for me.
* Late and experiencing from that I also find them useful specially on ScriptableObjects (DataAssets): to have
the same datas shared as class reference between MonoBehaviours. To have different configurations of the same
 DataAssets to switch during runtime or to 'resolve' stuffs.
    * Resolve assets are a peculiar case. I created them to share a 'method' between behaviours that 
    it should be run with different local parameters. _I used them once to create a small AI method that needed to change parameters during runtime_
* Not only this. I also applied the concept of containers to draw:
    * SerializedDictionaries to draw dictionaries in inspector (that can be cached in your class during runtime).
    * PagedArrayContainers to draw ReadOnly big amount of data without affecting editor performance. 
    Just imagine to have an array of 500.000 length... _eh!_... normally the inspector would beg for mercy, but not this time. 

### Links:
- [repo](https://github.com/ZiosTheCloudburster/CippSharpCoreContainers.git)

### Support:
- [tip jar](https://www.amazon.it/photos/share/Gbg3FN0k6pjG6F5Ln3dqQEmwO0u4nSkNIButm3EGtit)

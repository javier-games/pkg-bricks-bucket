# Installation Guide

This article will guide throw the different ways to
install a *Bricks Bucket Package*.

## Git Package Method

Unity can fetch a Git repository using the [Package
Manager](https://docs.unity3d.com/Manual/Packages.html).
To add a *Bricks Bucket Package* first make sure
you have installed [Git](https://git-scm.com) in your
machine. Then get the link of the package from its
repository you want to install.

Taking as example the Bricks Bucket Core Package, you can
install it by adding the following code to your package JSON
located at `../<MyUnityProject>/Packages/manifest.json`.

    {
        "dependencies": {
            "games.monogum.bricksbucket.core":
            "https://bitbucket.org/monogum/bricks-bucket-core-package.git"
        }
    }

Or by adding the package using the editor in the 
`Package Manager/+/Add package from git URL` option.
Read more about this method in the official [Unity
Manual](https://docs.unity3d.com/Manual/upm-git.html).

## Unity Package Import

If you need the source code into your project you also can
download the `.unitypackage` file from the repository under
the download section of bitbucket.

* [BricksBucket.Core](https://bitbucket.org/monogum/bricks-bucket-core-package/downloads/)

To install it just open you project and double-click the
downloaded package.

## Download Different Versions

Under the download section you can also find a tab named
*Tags* or *Branches*, from here you can download de source
files, un-zip the drag and drop the files inside your
*Assets* or *Packages* folder.

## Advanced Options

Taking advantage of the repository you can install the
package as a submodule and change between branches. You can
read more about this option in the [Git Documentation](
https://git-scm.com/book/en/v2/Git-Tools-Submodules).



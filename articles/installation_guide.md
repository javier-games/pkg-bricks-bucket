# Installation Guide

There are several ways to install **BricksBucket** into your Unity project. Choose the method that best fits your workflow. For easy updates and access to future releases, we recommend using npm.

## Installation via npm

To install BricksBucket from an npm registry, follow these steps:

1. Open your Unity project and go to `Edit > Project Settings > Package Manager`.
2. In the `Scoped Registries` section, click the `+` button to add a new registry.
3. Fill in the following fields:
    - **Name**: `Javier Games`
    - **URL**: `https://registry.npmjs.org`
    - **Scope(s)**: `games.javier`
4. Click `Save`, then close the Project Settings window.
5. Open the Package Manager by navigating to `Window > Package Manager`.
6. In the Package Manager, select `Packages: My Registries` from the dropdown.
7. You should see the `games.javier.upm.bricksbucket.core` package listed. Click `Install` to add it to your project.

Alternatively, you can directly modify your `Packages/manifest.json` file:

1. Add the following entry to the `scopedRegistries` section of your `manifest.json`:
```json
{
  "scopedRegistries": [
    {
      "name": "Javier Games",
      "url": "https://registry.npmjs.org",
      "scopes": ["games.javier"]
    }
  ]
}
```
2. Add `games.javier.upm.bricksbucket.core` to the `dependencies` section:
```json
{
  "dependencies": {
    "games.javier.upm.bricksbucket.core": "1.0.0"
  }
}
```
Replace `1.0.0` with the desired version of BricksBucket. Save the `manifest.json` file after editing.

## Installation via OpenUPM

You can also install BricksBucket via the OpenUPM CLI. Make sure you have Node.js v16 or higher and the [openupm-cli](https://openupm.com/docs/getting-started-cli.html) installed.

Run the following command:

```bash
openupm add games.javier.upm.bricksbucket.core
```

This will configure your Unity project to use the OpenUPM registry.

## Installation via Cloning

To install BricksBucket by cloning the repository:

1. Clone the repository to your local machine:
```bash
git clone https://github.com/javier-games/pkg-bricks-bucket.git
```

2. Open Unity and go to `Window > Package Manager`.
3. Click the `+` button in the top-left corner, then select `Add package from disk...`.
4. In the file explorer, navigate to the cloned repository and select the `package.json` file.
5. Click `Open` to install the package.

Unity will recognize the package, and it will appear in the Package Manager.

## Install from a Tarball

Download the tarball (`games.javier.upm.bricksbucket.core-{version}.tgz`) from the [Releases section](https://github.com/javier-games/pkg-bricks-bucket/tags) or [itch.io](https://javier-games.itch.io/bricks-bucket).

1. In Unity, go to `Window > Package Manager`.
2. Click the `+` button and select `Add package from tarball...`.
3. Locate the downloaded tarball and click `Open`.

## Install from Git URL

If you prefer, you can directly install the package from the Git repository:

1. In Unity, go to `Window > Package Manager`.
2. Click the `+` button and select `Add package from git URL...`.
3. Enter the repository URL:
```
https://github.com/javier-games/pkg-bricks-bucket.git
```


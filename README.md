# Keyboard Utilities

> Disclaimer: Almost all of the code in this project comes from the [Unity Explorer](https://github.com/sinai-dev/UnityExplorer) project by [Sinai](https://github.com/sinai-dev).

This is a utility mod for *The Long Dark* that enables other mods to more easily use Keyboard input.

## Installation

1. If you haven't done so already, install MelonLoader by downloading and running [MelonLoader.Installer.exe](https://github.com/HerpDerpinstine/MelonLoader/releases/latest/download/MelonLoader.Installer.exe).
2. Download the latest version of `KeyboardUtilities.dll` from the [releases page](https://github.com/ds5678/KeyboardUtilities/releases).
3. Move `KeyboardUtilities.dll` into the Mods folder in your TLD install directory.

## Example Use

```
public static void SomeMethod() 
{
    if(KeyboardUtilities.InputManager.GetKey(KeyCode.T))
    {
        DoSomething();
    }
}
```
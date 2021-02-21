# Keyboard Utilities

This is a utility mod for *The Long Dark* that enables other mods to more easily use Keyboard input.

## Special Thanks

I greatly appreciate the work [Sinai](https://github.com/sinai-dev) has done. Most of the code in this project comes from his [Unity Explorer](https://github.com/sinai-dev/UnityExplorer) project. I adapted the code for use with modding The Long Dark and added my own additional funcionality, but none of it would have been possible without [Unity Explorer](https://github.com/sinai-dev/UnityExplorer).

## [Patreon](https://www.patreon.com/ds5678)

I know many people might skip over this, but I hope you don't. You are so special, and I would appreciate your support. Modding takes lots of time, and I have expenses like food, internet, and rent. If you feel that I have improved your playing experience, please consider supporting me on my [Patreon](https://www.patreon.com/ds5678). Your support helps to ensure that I can continue making mods for you at the pace I am :)

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
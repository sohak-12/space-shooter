# Space Shooter
 
![Platform](https://img.shields.io/badge/Platform-Windows-blue?logo=windows&logoColor=white)
![Language](https://img.shields.io/badge/Language-C%23-purple?logo=csharp&logoColor=white)
![Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blueviolet?logo=dotnet&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green)

A classic 2D arcade space shooter, shoot down alien waves, dodge bullets, and survive as long as you can. Built from scratch using C# and Windows Forms

</div>

---

## About the Developer

Designed and developed by **Soha Muzammil** built entirely from scratch using C# and Windows Forms, no external game engine or library used.

---

## Gameplay

- Move your spaceship freely across the screen in all 4 directions
- Destroy waves of alien enemies before they overwhelm you
- Enemies shoot back and increase in number every wave
- You have 3 lives, taking a hit grants brief invincibility
- Game ends when all lives are lost, press **R** to restart

---

## Controls

| Key | Action |
|-----|--------|
| Arrow Keys / W A S D | Move ship in all directions |
| Space | Shoot |
| R | Restart after Game Over |

---

## Features

- Smooth 60 FPS game loop with double-buffered rendering
- Infinite wave system, difficulty scales with each wave
- Enemy AI that bounces, drifts downward, and shoots at random intervals
- Invincibility frames after taking damage with a flashing effect
- HUD displaying Score, Lives, and current Wave
- Starfield background for space atmosphere
- Instant restart without relaunching the game

---

## Project Structure

```
space-shooter/
├── Program.cs          # Entry point
├── Game.cs             # Main form: game loop, input handling, rendering
├── GameObject.cs       # Abstract base class for all game objects
├── Player.cs           # Player ship: movement, shooting, lives, invincibility
├── Enemy.cs            # Enemy ships: AI movement, random shooting
├── Bullet.cs           # Bullet logic for both player and enemies
└── SpaceShooter.csproj
```

---

## Requirements

- Windows OS (Windows 10 / 11 recommended)
- [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) pre-installed on Windows 10/11

---

## Build & Run

**Option 1 - Run the pre-built executable**

```
bin\Release\SpaceShooter.exe
```

**Option 2 - Build from source using MSBuild**

```cmd
cd "C:\Windows\Microsoft.NET\Framework64\v4.0.30319"
MSBuild.exe "path\to\space-shooter\SpaceShooter.csproj" /p:Configuration=Release
```

Then run:

```
bin\Release\SpaceShooter.exe
```
---

## Screenshots
<img width="990" height="840" alt="image" src="https://github.com/user-attachments/assets/bc716ad2-43b2-450b-a52e-b4713a6bbd54" />


<img width="982" height="842" alt="image" src="https://github.com/user-attachments/assets/3d815357-8759-4100-9672-a4ecf9716478" />




---
## License

This project is licensed under the [MIT License](LICENSE).  
Feel free to use, modify, and distribute with attribution.

---

<div align="center">
Made by <strong>Soha Muzammil</strong>
</div>

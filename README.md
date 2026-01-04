# Sokoban

**A grid-based puzzle game inspired by the classic Sokoban mechanic.**  
Players navigate a tile-based environment and push boxes onto target positions, solving spatial logic puzzles that require careful planning and step-by-step reasoning.

**Playable Demo**  
https://betapbo.itch.io/sokoban  
*(The itch.io page includes gameplay footage and screenshots.)*

**Created during:**  
**Aalto Game Design Summer School (July 2025)**

## About This Project
*Sokoban* is a **solo-developed puzzle game** created during the Aalto Game Design Summer School.  
All aspects of the game’s **design, programming, level setup, and implementation** were completed independently.

Free and publicly available assets were used for visuals, animations, and audio, while **all gameplay systems, logic, and scene construction were developed from scratch**.

## About This Repository
This repository contains the **Unity source code** for *Sokoban*, with a focus on **gameplay programming and system logic**.  
To keep the project clear and review-friendly, the repository emphasizes code structure and mechanics rather than asset completeness.

## Repository Scope
This repository is intentionally scoped to highlight **core gameplay systems and technical design**.  
For clarity and review purposes, only essential scripts, scenes, and project settings are included.  
The fully playable version is available on itch.io.

## My Contributions (Solo Development)
- Designed and implemented the complete Sokoban-style gameplay loop:
  - Grid-based player movement
  - Box pushing logic with collision checks and rule validation
  - Target detection and level completion conditions
- Implemented core systems:
  - Level state management
  - Reset and restart functionality
  - Input handling for discrete grid-based movement
- Constructed all levels and scene layouts
- Integrated free assets (visuals, animations, audio) into the gameplay systems
- Managed project structure and rapid iteration within a short-term intensive course

## Technical Highlights
- Developed in **Unity** using **C#**
- Fully solo implementation of:
  - Grid-based movement logic
  - Deterministic puzzle behavior
  - Clear separation of game states and rules
- Core systems located in `Assets/Scripts/`
- Project dependencies managed via `Packages/manifest.json`

## Project Structure
- Assets/Scripts/  # Core gameplay logic (movement, boxes, level rules)
- Assets/Scenes/ # Game scenes and level setups
- ProjectSettings/ # Unity project settings
- Packages/manifest.json # Package dependencies
- .gitignore # Ignores generated and large files

## How to Open (Optional – for Reviewers)
1. Clone this repository  
2. Open the project in Unity Hub  
3. Use Unity 6000.1.7f1 (recommended for full compatibility)
4. Open the main scene from `Assets/Scenes/`  
5. Press Play in the Unity Editor  

> **Note:** Visual and audio assets are minimal or omitted to keep the repository lightweight and focused on programming.  
> The complete playable experience is available via the itch.io demo.

## Design Notes
This project was created to explore:
- Puzzle mechanic implementation
- Grid-based logic and rule constraints
- Solo development workflow under time constraints
- Translating a classic game mechanic into a clean, maintainable codebase

## Contact & More
- GitHub: https://github.com/RuiqingCHEN  
- itch.io: https://betapbo.itch.io  

Thanks for checking out the project!  
Feel free to explore the `Scripts` folder to see how the puzzle mechanics and movement systems are implemented.


# Void Jumper

```
  +--------------------------------------------------+
  |           V O I D   J U M P E R                  |
  |         A turn-based dungeon explorer            |
  +--------------------------------------------------+
```

A turn-based console dungeon explorer written in C# (.NET 10).  
Navigate 5 levels, defeat enemies, and escape the Void.

---

## How to Run

### From Release (recommended)

1. Go to the [Releases](../../releases) page
2. Download `VoidJumper-win-x64.zip` (Windows) or `VoidJumper-linux-x64.zip` (Linux)
3. Unzip the archive
4. Run `VoidJumper.exe` (Windows) or `./VoidJumper` (Linux)

No .NET installation required — the runtime is bundled.

### From Source

```
dotnet run --project VoidJumper/VoidJumper.csproj
```

---

## Controls

| Key | Action |
|-----|--------|
| W / A / S / D | Move up / left / down / right |
| Space | Attack nearest enemy |
| H | Heal (+20 HP) |
| Z | Undo last action |
| Esc | Pause / Resume |
| F5 | Save game |
| F9 | Load game |

---

## Gameplay

- The game is **turn-based**: every key press is one action
- Enemies move and attack in response to your actions
- **Patrol** enemies (`P`) move 1 step per turn, 60 HP, 8 damage
- **Jumper** enemies (`J`) move 2 steps per turn, 40 HP, 15 damage
- Enemies only attack when adjacent to you (distance ≤ 1)
- When an enemy's HP drops below 20%, it switches to fleeing
- Clear all 5 levels to win

---

## Screenshots

```
  Level 1  |  Level 1/5

  ############################################################
  #..........................................................#
  #..........................................................#
  #..........................................................#
  #..........................................................#
  #..........................................................#
  #...................####............................####...#
  #..........................................................#
  #..........................................................#
  #..........................................................#
  #..@.....................................P...........J.....#
  #..........................................................#
  #..........................................................#
  ############################################################

  HP [####################] 100/100   Score: 0   Enemies: 2

  W/A/S/D move  |  Space attack  |  H heal  |  Z undo  |  Esc pause  |  F5 save  |  F9 load
```

---

## Architecture

The project uses 9 design patterns:

### Creational

| Pattern | Classes | Why |
|---------|---------|-----|
| **Singleton** | `GameManager` | Single source of truth for global settings (map size, difficulty). Prevents accidental multiple instances. |
| **Builder** | `LevelBuilder` | Level construction has many optional parameters. Builder prevents a 5-argument constructor and makes the intent readable. |
| **Factory Method** | `EnemyFactory`, `PatrolEnemyFactory`, `JumpEnemyFactory` | Decouples enemy creation from the game logic. Adding a new enemy type only requires a new factory — nothing else changes. |

### Structural

| Pattern | Classes | Why |
|---------|---------|-----|
| **Decorator** | `WeaponDecorator`, `FireDecorator`, `SharpDecorator` | Weapon effects can be stacked at runtime without subclassing every combination. `new SharpDecorator(new FireDecorator(new BaseSword()))` adds both effects transparently. |
| **Facade** | `BattleFacade` | Hides the complexity of wiring up LevelBuilder + EnemyFactory + WeaponDecorator behind a single call. |

### Behavioral

| Pattern | Classes | Why |
|---------|---------|-----|
| **Strategy** | `ICombatStrategy`, `MeleeAttack`, `RangedAttack`, `FleeBehavior` | Enemy behavior changes at runtime (melee → flee when HP < 20%). Strategy lets us swap behavior without touching the Enemy class. |
| **Observer** | `Player.OnHealthChanged`, `ConsoleHUD` | HUD reacts to HP changes without Player knowing about the HUD. Loose coupling — any number of observers can subscribe. |
| **State** | `GameState`, `MenuState`, `PlayingState`, `PauseState`, `GameOverState`, `VictoryState` | Each game screen is a self-contained object. Transitions are clean and each state only handles its own input. |
| **Command** | `ICommand`, `MoveCommand`, `AttackCommand`, `HealCommand`, `InputManager` | Every player action is an object. This enables Undo (`Z` key) — each command stores the state before execution and can reverse itself. |

---

## Project Structure

```
VoidJumper/
  Core/        — Game loop, level system, save/load
  Entities/    — Player, enemies, base Entity
  Items/       — Item base class
  States/      — State machine (Menu, Playing, Pause, GameOver, Victory)
  Systems/     — Weapons, strategies, commands, HUD
VoidJumper.Tests/
  EntityTests, StrategyTests, ObserverTests,
  CommandTests, StateTests, SaveTests
```

---

## Building a Release

```
# Windows
dotnet publish VoidJumper/VoidJumper.csproj -c Release -r win-x64 --self-contained -p:PublishSingleFile=true -o publish/win-x64

# Linux
dotnet publish VoidJumper/VoidJumper.csproj -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true -o publish/linux-x64
```

---

## Authors

| Name | Contributions |
|------|---------------|
| Fleynqq | Architecture, Factory, Strategy, State machine, Save system, Map rendering |
| shinichi234-work | Initial structure, Game manager, Builder, Decorator, Facade, Observer, Commands |

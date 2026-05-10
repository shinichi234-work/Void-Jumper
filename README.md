# Void Jumper

A console-based platformer game written in C#.

The player navigates through dangerous levels, jumping over platforms and avoiding enemies to reach the exit.

## Architecture

The project follows an object-oriented design and applies the following patterns:

| Pattern | Class(es) |
|---|---|
| Singleton | `GameManager` — single game instance with global settings |
| Factory Method | `EnemyFactory`, `PatrolEnemyFactory`, `JumpEnemyFactory` — enemy creation |
| Builder | `LevelBuilder` — fluent level construction |
| Decorator | `WeaponDecorator`, `FireDecorator`, `SharpDecorator` — weapon enchantments |
| Facade | `BattleFacade` — hides level setup, enemy spawning and weapon configuration behind a single call |

## Structure

```
VoidJumper/
  Core/       — game loop, manager, level, builder, facade
  Entities/   — Entity, Player, Enemy and factory classes
  Items/      — Item
  Systems/    — weapon interface and decorators
VoidJumper.Tests/ — xUnit unit tests
docs/             — UML diagram
```

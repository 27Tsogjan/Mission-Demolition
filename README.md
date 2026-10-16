# Mission Demolition

## Start and victory screens

To make the game easier to enter and give players a clear ending, we added a
start screen and a "You Win" screen as separate Unity scenes. The start screen
is the first scene in the build and its button calls `MainMenu.PlayAgain()` to
load the gameplay scene.

When the player completes every level, `MissionDemolition` calls
`GameManager.WinGame()`. The game manager switches to the victory scene, where
the win screen's Play Again button calls `WinScreen.PlayAgain()` and starts the
gameplay scene again. This gives players a clear success message and a direct
way to play another round.
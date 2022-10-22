# tetryds Tools

> Here you can find helpful stuff when coding Unity games

## Installation

Grab the latest release .dll file and drop anywhere on your Assets folder.
If you want documentation grab the .xml file as well.
On your own assembly definition, enable `Override References` and add it to the `Assembly References` list.
If you need help with that just call me.

While I don't add a more decent README, the automatic tests should give a pretty good view of how some of this stuff works.
For the others, just try it out, you can trust that I have thoroughly tested it manually if no unit tests are present.


## Physics
### Special Casts
#### ConeCastNonAlloc
Much appreciated non-allocating cone cast, that is actually a spherecast with extra steps. Useful for radars and stuff like that.

## Debugging
### ExtraGizmos
#### DrawCone
Draws a gizmo cone for debugging. Helpful when using alongside ConeCastNonAlloc

## Timing
### SyncTimer
Neat sync timer that you manually tick and invokes a callback when it expires.

## StateMachine
Event state machine with state triggers and objects. This state machine is meant to keep track of the current state of a game system, it only attempts to switch state when an event is raised.
You can build neat event graphs and have bullet-proof state handling without hassle!
There is a version with a generic behavior state, and another one without, to better suit your needs.

--------------------
++++++++++++++++++++
--------------------
Mars Rover Challenge
--------------------
////////////////////
--------------------



As specified in “Mars Rover Challenge.pdf”; Rovers have been sent to Mars to survey the terrain and I have been charged with creating their navigation system.
Mars’s surface has been divided into zones and each zone can be modelled as a two dimensional cartesian grid. The zones have been very carefully surveyed ahead of time and are deemed safe for exploration within the zone’s bounds, as represented by a single Cartesian coordinate. E.g.: (5, 5)
The rover understands the cardinal points and can face either East (E), West (W), North (N) or South (S)
The rover understands three commands are as follows:
-	M: Move one space forward in the direction it is facing
-	R: Rotate 90 degrees to the right
-	L: Rotate 90 degrees to the left

Due to the transmission delay in communicating with the rover on Mars, you are only able to send the rover a list of commands. These commands will be executed by the rover and its resulting location sent back to HQ. 

This is an example of the list of commands sent to the rover:
8 8
1 2 E
MMLMRMMRRMML

This is how the rover will interpret the commands:
-	The first line describes how big the current zone is. This zone’s boundary is at the Cartesian coordinate of 8,8 and the zone comprises 64 blocks.
-	The second line describes the rover’s staring location and orientation. This rover would start at position 1 on the horizontal axis, position 2 on the vertical axis and would be facing East(E)
-	The third line is the list of commands (movements and rotations) to be executed by the rover. As a result of following these commands, a rover staring at 1 2 E in this zone would land up at 3 3 S.
==============================================================================================================================================================================================================================================================================================================================================================================================

Usage Instructions

The program which illustrates the above task is a console application coded in C# and compiled into an EXE. It takes instructions from a text file which you can configure a path to in the app.config file.

Setup Instructions

1-	Navigate to directory “MarsRoverChallenge\MarsRoverChallenge\bin\Release”.
2-	Edit “MarsRoverChallenge.exe.config\appSettings\CommandFile” to point to your desired command file name and path. E.g. "C:\rover\Commands.txt" and save.
3-	Create a text file at a location specified in “MarsRoverChallenge.exe.config\appSettings\CommandFile” above.
4-	Open the new Command Text file and enter the following instructions from our example above and save (Keep the Commands file open):
8 8
1 2 E
MMLMRMMRRMML

5-	Execute “MarsRoverChallenge\MarsRoverChallenge\bin\Release\ MarsRoverChallenge.exe” a console window will appear and the program will execute all the commands as listed in your command file. The resulting rover location should be “3 3 S” 
6-	The program initiates a File watcher on the “Commands.txt (Or whatever you decided to name it)” file when it launches, so you can go to the Commands file and issue more commands. Try replacing all the text with: RRMMRMMLM and then save. The rover’s location should move to “5 6 N”. You can move the rover around as you wish, just remember to save after altering the commands.
7-	Before you re-launch the application. Ensure you get the file back to its original format so it knows how to size the grid and where to deploy the rover as specified in the instructions above.
================================================================================================================================================================================================================================================================================================================================================================================================

Program Design

I decided to follow a Test Driven Design approach while designing the program, this meant employing an Object Oriented design as it would make unit testing easier and more logical.

The “Main(string[] args)” method in Program.cs is where it all begins. After some basic console loging and ensuring the Commands file exists, the “CommandFileLoader” class is initialized with the command file path. The class initializes everything, based on each command line it initializes the Grid and Rover then places the rover at its initial location.

After initialization the program checks for any movement commands and maneuvers the rover accordingly. After executing all movement commands, a file watcher is set on the commands file in order to maintain control from HQ.

I decided to go with this particular design because it makes the addition of more rovers simpler in future while making it easier to debug.

I made use of unit tests to ensure the program outputs are correct as anticipated.

I agree this isn’t as structured and well commented as a real life product should be, but I believe it brings out the logic behind the design.

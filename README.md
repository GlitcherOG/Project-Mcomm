# SSX3-Server

## Bounty
There is currently a 500 dollar bounty on getting working server messages to get in game. 

## Todo
- Fix Bug where users are blocked when logging in
- Add Remove Buddy
- Send Online Details before displaying Search
- Patch Search so it gets offline personas
- fix room messages so user can create room
- Find out Stat Struct
- Get Challanges working
- Fix Sending Player List Twice on startup
- Fix Duplicate Persona not getting back possible names
- Add Edit user and lost user
- Patch Console Manager so it is used instead of console.writeline
- Discord Bot Fix Kick Messages
- Fix Disconnecting not working correctly
- Discord bot able to send messages in game and to rooms
- Fix Chal not getting user details properly
- Fix Highscore not listing database scores
- Add check to see if user is already logged in, if so disconnect and allow new connection
 
## To Connect to server
1. Using DNAS Patcher 21 bypass the DNAS Checks using mode 1 and 3 to generate an iso to bypass dnas check
2. In Emulator go to network settings swap DNS1 Address to Internal
3. Go to Internal DNS
4. Add a new entry for URL ps2ssx04.ea.com to go to the ip address of the server and ensure its enabled
5. Start server to generate config file
6. Edit config files IP Address to be the ip adress of the server
7. Restart Server

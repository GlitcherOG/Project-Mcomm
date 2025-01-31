# Mcomm Project Server (SSX3 Online Server)

## Bounty
There is currently a 500 dollar bounty on getting working server messages to get in game. 

## Status
General
• Challanaging Player: 
• Quick Play: 

Accounts 

• Authentication & Account Creation: ✅
• Persona creation & Persona deletion: ✅
• Account Management: 
• Username & Password recovery:  

Rooms

• Create & join rooms ✅
• Text Chat: ✅
• Voice Chat: 
• Kick player: 
• Password-locked rooms:

Buddies

• Searching for buddies: ✅ 
• Add as Buddy: ✅ (Show as blocked when leaving and rejoining)
• Removing buddies:  
• Blocking buddies:  
• Online Status: ✅
• Send & receive messages: ✅ 
• View profile: ✅
• Pressence info:

Statistics

• Leaderboards: 
• Rider Stats: 
• Ranked Play: 

News

• News: ✅

## Todo
- Direct Connect
- Fix Bug where users are blocked on buddy list when logging in
- Add Remove Buddy
- Send Online Details before displaying Search
- Patch Search so it gets offline personas
- Find out Stat Struct
- Get Challanges working
- Fix Sending Player List Twice on startup
- Add Edit user and lost user
- Patch Console Manager so it is used instead of console.writeline
- Fix Disconnecting not working correctly
- Fix Chal not getting user details/stats properly
- Fix Highscore not listing database scores
- Add check to see if user is already logged in, if so disconnect and allow new connection
- Fix Challanger Messages to Create Session
- Fix Quick Match So it Works
- Persona Generator for Duplicate Personas
- Fix Having to DQUE to leave room
- Mcomm Buddy for server commands

# Todo Discord Bot
- Discord bot able to send messages in game and to rooms
- Discord Bot Fix Kick Messages
- Link Ingame Account To Discord Account

## To Connect to server
1. Using DNAS Patcher 21 bypass the DNAS Checks using mode 1 and 3 to generate an iso to bypass dnas check
2. In Emulator go to network settings swap DNS1 Address to Internal
3. Go to Internal DNS
4. Add a new entry for URL ps2ssx04.ea.com to go to the ip address of the server and ensure its enabled
5. Start server to generate config file
6. Edit config files IP Address to be the ip adress of the server
7. Restart Server

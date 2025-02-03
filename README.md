# Mcomm Project Server (SSX3 Online Server)

## Status
General  
• Challanaging Player: ✅  
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
• Add as Buddy: ✅ (Disabled currently, Show as blocked when leaving and rejoining)  
• Removing buddies:  
• Blocking buddies:  
• Online Status: ✅  
• Send & receive messages: ✅  
• View profile: ✅  
• Pressence info: ✅

Statistics  

• Leaderboards:  
• Rider Stats:  
• Ranked Play:  

News  

• News: ✅

## Todo
- Fix Bug where users are blocked on buddy list when logging in
- Add Remove Buddy
- Send Online Details before displaying Search
- Patch Search so it gets offline personas
- Find out Stat Struct
- Add Edit user and lost user
- Fix Chal not getting user details/stats properly sometimes
- Fix Highscore not listing database scores
- Add check to see if user is already logged in, if so disconnect and allow new connection
- Fix Quick Match So it Works
- Persona Generator for Duplicate Personas
- Fix Having to DQUE to leave room (Probably due to an issue with vc)
- Mcomm Buddy for server commands
- Fix Passwords
- Last 5 messages queue for rooms
- Fix Title on rooms as it doesnt seem to be appearing
- Ensure user cant message,block,challange,report self
- Tunneling Service (Temp Hide IPs as well)

# Todo Discord Bot
- Discord bot able to send messages in game and to rooms
- Set Rooms to Global
- Discord Bot Fix Kick Messages
- Link Ingame Account To Discord Account

## Setting Up Server
1. Run Server to generate config file and nessasary files
2. Port forward all three ports found in the config file to your server ListenerPort (11000), GamePort (10901), BuddyPort (13505)
3. Edit the config file to contain the IP Address for your server

## Setting Up Client PCSX2
1. Using DNAS Patcher 21 bypass the DNAS Checks using mode 1 and 3 to generate an iso to bypass dnas check
2. In Emulator go to network settings swap DNS1 Address to Internal
3. Go to Internal DNS
4. Add a new entry for URL ps2ssx04.ea.com to go to the ip address of the server and ensure its enabled
5. Configure the Emulator to Use PCAP Switched or PCAP Bridged (See Below)
6. Port Forward UDP 3658-3659 for Gameplay, UDP 6000-6001 for Voice Chat to the emulator PS2 Address you set for PCAP

## PCAP Switched (Ethernet) / PCAP Bridged (WiFi)
Download NPCAP
Note: PCAP Switched is the most reliable option for connecting with others.
Note: PCAP options treat PCSX2 as its own device on your network. So, we need to create an address for it (PS2 Address)
1. In PCSX2, open Settings / Network & HDD.
2. Under Ethernet Device Type, choose the PCAP option that fits your connection type.
3. Enable Intercept DHCP & find the PS2 Address Box.
4. Enter the first 3 sections of your default gateway address (XXX.XXX.X.)
5. For the final section, enter any number 100 - 250
Note: Choosing a high number will give you a better chance at avoiding conflict with existing devices.

## Prevent Windows from messing with your connection (PCSX2 Only)  
__Firewall Exception for PCSX2__  
Control Panel -> System and Security -> Windows Defender Firewall  
Allow an app or feature through Windows Defender Firewall  
Allow another app... -> Browse... -> PCSX2 PS2 Emulator.exe -> Add  

__Inbound Rules__  
Control Panel -> System and Security -> Windows Defender Firewall  
Advanced Settings -> Inbound Rules -> New Rule  
Rule Type: Custom -> Program: This Program Path -> Browse... -> PCSX2 PS2 Emulator.exe  
Protocol and Ports -> Protocol Type: UDP  
Name: PCSX2 UDP -> Finish  
Repeat but for TCP (select Protocol Type: TCP)  